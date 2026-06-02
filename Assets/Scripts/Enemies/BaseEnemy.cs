using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(-10)]
[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public abstract class BaseEnemy : MonoBehaviour
{
    //private: variables that can only be accessed within this class that it is declared in
    //public: variables that can be accessed from other classes
    //protected: variables that can be accessed from this class and any class that inherits from it

    protected SpriteRenderer sr;
    protected Animator anim;
    protected int health;

    [SerializeField] protected int maxHealth = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (maxHealth <= 0)
        {
            maxHealth = 5;
            Debug.LogWarning("BaseEnemy: Max health was not set, defaulting to 5 on " + gameObject.name);
        }

        health = maxHealth;
    }

    public virtual void TakeDamage(int damage, DamageType damageType = DamageType.Default)
    {
        health -= damage;
        
        if (health <= 0)
        {
            anim.SetTrigger("Death");

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject, 0.5f);
            }
            else
                Destroy(gameObject, 0.5f);
        }
    }
}

public enum DamageType
{
    Default,
    JumpedOn
}