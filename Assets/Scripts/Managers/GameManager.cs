using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    #region Singleton Pattern
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    #region State Variables
    [SerializeField] private int maxLives = 9;
    private int _lives = 3;
    public int lives
    {
        get { return _lives; }
        set
        {
            if (value <  0)
            {
                GameOver();
                return;
            }

            if (_lives > value)
            {
                Respawn();
            }

            _lives = value;
            if (_lives > maxLives)
            {
                _lives = maxLives;
            }

            Debug.Log($"Lives have changed to {_lives}");
        }
    }
    #endregion

    //event driven programming is a programming paradigm in which the flow of the program is determined by events, such as user input, rather than a linear sequence of instructions. This allows for more flexibility and responsiveness in the program, as it can react to events as they happen rather than waiting for a specific point in the code to execute. Events are the backbone of the obverver pattern, which is a design pattern in which an object (the subject) maintains a list of its dependents (observers) and notifies them of any state changes, usually by calling one of their methods. This allows for a decoupling of the subject and observers, as the subject does not need to know anything about the observers in order to notify them of changes.
    //delegates are a type that represents references to methods with a specific parameter list and return type. They are used to pass methods as arguments to other methods, and can be used to define callback methods that are called when an event occurs. Delegates are often used in event-driven programming to allow for more flexibility and decoupling between the event source and the event handlers.

    public delegate void PlayerInstanceDelegate(PlayerController player);
    public event PlayerInstanceDelegate OnPlayerSpawned;

    [SerializeField] private PlayerController playerPrefab;
    private PlayerController _playerInstance;
    public PlayerController PlayerInstance => _playerInstance;

    private Vector3 currentCheckpoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        //This is a debug input to allow us to easily switch between the title screen and the game scene, as well as to test our lives system. You can remove this once you have your title screen and game scene set up.
        if (Input.GetKeyDown(KeyCode.Escape)) {
            string currentSceneName = SceneManager.GetActiveScene().name;
            string sceneToLoad = currentSceneName == "Title" ? "Game" : "Title";

            SceneManager.LoadScene(sceneToLoad);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            lives++;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            lives--;
        }
    }

    public void SpawnPlayer (Vector3 spawnPos)
    {
        _playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        UpdateCheckpoint(spawnPos);

        OnPlayerSpawned?.Invoke(_playerInstance);
    }

    public void UpdateCheckpoint(Vector3 newCheckPoint)
    {
        currentCheckpoint = newCheckPoint;
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("Title");
    }

    private void Respawn()
    {
        //_playerInstance.Anim.SetTrigger("Respawn");
        _playerInstance.transform.position = currentCheckpoint;
    } 
}
