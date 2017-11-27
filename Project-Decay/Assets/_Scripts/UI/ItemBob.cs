using UnityEngine;
using System.Collections;

public class ItemBob : MonoBehaviour
{
    // Direction to move
    public Vector3 moveDir = Vector3.zero;

    // Speed of the item bob
    public float speed = 0.0f;

    // Distance of travel (Until it moves in other direction)
    public float travelDistance = 0.0f;
    Vector3 vel = Vector3.zero;

    // Transform Variable
    private Transform ThisTransform = null;

    public bool isLootItem;
    public bool isInPosition = false;

    // Coroutine until travels in new direction
    IEnumerator Start()
    {
        {
            //Cache transform
            ThisTransform = transform;

            // Loop forever
            while (true)
            {
                // Change direction
                moveDir = moveDir * -1;

                // Start movement
                yield return StartCoroutine(Travel());
            }
        }
    }

    public void Update()
    {
        // If the item is a lootitem then move to parent pos
        if (isLootItem && !isInPosition)
            {
                transform.position = Vector3.SmoothDamp(transform.position, transform.parent.position, ref vel,  0.3f);
 
            }       
    }




    // Travel full distance in direction, from current posistion
    IEnumerator Travel()
    {
        // Current Travel distance
        float distanceTravelled = 0.0f;

        // Move
        while (distanceTravelled < travelDistance)
        {
            // Gets new position based on speed and current direction
            Vector3 distToTravel = moveDir * speed * Time.deltaTime;

            // Updates new position
            ThisTransform.position += distToTravel;

            // Updates distance travelled so far
            distanceTravelled += distToTravel.magnitude;

            // Wait until next update
            yield return null;
        }

    }
}
