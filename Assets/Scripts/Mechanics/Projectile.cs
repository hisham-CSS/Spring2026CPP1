using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType projectileType = ProjectileType.PlayerProjectile;
    [SerializeField, Range(0.5f, 10f)] private float lifetime = 10f;
    [SerializeField] private int damage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (projectileType == ProjectileType.PlayerProjectile)
        {
            BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();
            
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (projectileType == ProjectileType.EnemyProjectile)
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Barrier") || collision.CompareTag("Squish"))
                return;

            // Prevent friendly fire between enemies
            if (collision.CompareTag("Player"))
            {
                GameManager.Instance.lives--;
            }
            Destroy(gameObject);
        }
    }

    //add some collision detection functions for the projectile to interact with the environment and other objects, such as damaging enemies or being destroyed on impact with walls
}

public enum ProjectileType
{
    PlayerProjectile,
    EnemyProjectile
}
