using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Button References")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private Button resumeButton;

    [Header("In Game UI References")]
    [SerializeField] private TMP_Text livesText;

    [Header("Menu References")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenu;


    void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(() => ChangeScene("Game"));

        if (settingsButton != null)
            settingsButton.onClick.AddListener(() => SetMenu(settingsMenu, mainMenu));

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);

        if (backButton != null)
            backButton.onClick.AddListener(() => SetMenu(mainMenu, settingsMenu));

        if (returnToMenuButton != null)
            returnToMenuButton.onClick.AddListener(() => ChangeScene("Title"));

        if (resumeButton != null)
            resumeButton.onClick.AddListener(() => SetMenu(null, pauseMenu));
    }

    void Update()
    {
        if (livesText != null)
            livesText.text = "Lives: " + GameManager.Instance.lives;

        if (!pauseMenu) return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseMenu.activeSelf)
                SetMenu(null, pauseMenu);
            else
                SetMenu(pauseMenu, null);
        }
    }

    void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void SetMenu(GameObject menuToActivate, GameObject menuToDeactivate)
    {
        if (menuToActivate != null) menuToActivate.SetActive(true);
        if (menuToDeactivate != null) menuToDeactivate.SetActive(false);
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
