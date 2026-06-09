using UnityEngine;

public class TurretEnemy : BaseEnemy
{
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float detectionRange = 3f;
    [SerializeField] private AudioClip fireSound;
    private float timeSinceLastShot = 0f;

    Shoot shoot;
    AudioSource audioSource;

    private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        shoot = GetComponent<Shoot>();
        audioSource = GetComponent<AudioSource>();

        if (fireRate <= 0f)
        {
            Debug.LogWarning("Fire rate must be greater than 0. Setting to default value of 2 seconds.");
            fireRate = 2f;
        }

        //Subscriptions to events from the Shoot component and GameManager
        shoot.OnShotFired += () =>
        {
            timeSinceLastShot = Time.time;
            audioSource.PlayOneShot(fireSound);
        };
        GameManager.Instance.OnPlayerSpawned += (player) => target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) return;
        sr.flipX = (target.position.x < transform.position.x);

        if (!CheckDistance())
        {
            sr.color = Color.white;
            return;
        }

        sr.color = Color.red;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Idle"))
        {
            if (Time.time >= timeSinceLastShot + fireRate)
            {
                anim.SetTrigger("Fire");
                //timeSinceLastShot = Time.time;
            }
        }
    }

    bool CheckDistance()
    {
        float distanceToPlayer = Mathf.Abs(target.position.x - transform.position.x);
        return distanceToPlayer <= detectionRange;
    }
}
