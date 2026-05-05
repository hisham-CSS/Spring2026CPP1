using UnityEngine;

//abstract classes cannot be instantiated, but they can be inherited from. This allows us to create a base class for all pickups, and then create specific types of pickups that inherit from this base class. This is useful for code organization and reusability, as we can put common functionality for all pickups in the base class, and then override or add specific functionality in the derived classes.
public abstract class Pickup : MonoBehaviour
{
    abstract public void OnPickup(GameObject player);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPickup(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            OnPickup(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
