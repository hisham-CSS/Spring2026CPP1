using UnityEngine;

public class TurretEnemy : BaseEnemy
{
    [SerializeField] private float fireRate = 2f;
    private float timeSinceLastShot = 0f;

    Shoot shoot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        shoot = GetComponent<Shoot>();

        if (fireRate <= 0f)
        {
            Debug.LogWarning("Fire rate must be greater than 0. Setting to default value of 2 seconds.");
            fireRate = 2f;
        }

        shoot.OnShotFired += () => timeSinceLastShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
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
}
