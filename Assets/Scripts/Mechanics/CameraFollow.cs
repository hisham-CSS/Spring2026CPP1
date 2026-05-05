using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;

    [SerializeField] private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //MAKE YOUR CODE DEFENSIVE AGAINST BAD INPUT
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            if (player == null)
            {
                Debug.LogError("CameraFollow: No target assigned and no GameObject with tag 'Player' found in the scene.");
                return;
            }
                
            target = player.transform;

            PlayerController controller = player.GetComponent<PlayerController>();
            controller.lives++;
        }
    }

    //Inputs are polled in update, Physics are applied in FixedUpdate, and camera movement is best done in LateUpdate, so that we can be sure that the player has moved before we move the camera to follow them.

    // Update is called once per frame
    void LateUpdate()
    {
        //early return - if we don't have a target, we can't follow anything, so we should just exit the method
        if (target == null) return;

        //Store our current position
        Vector3 currentPos = transform.position;

        //update the X position to be  the same as the target's X position, but clamped between our min and max X values
        currentPos.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);

        transform.position = Vector3.MoveTowards(transform.position, currentPos, 10f * Time.deltaTime);
    }
}
