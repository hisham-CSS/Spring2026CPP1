using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
    [SerializeField] private GameObject[] pickupPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randomIndex = Random.Range(0, pickupPrefab.Length);

        if (randomIndex == pickupPrefab.Length - 1)
            return;

        Instantiate(pickupPrefab[randomIndex], transform.position, Quaternion.identity);
    }
}
