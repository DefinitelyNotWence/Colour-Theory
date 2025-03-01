using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{


    public GameObject player; // Allows me to add the player as a game object on the camera which allows
                              // me to grab specific parts of the player and use it in the camera
    

    void FixedUpdate()
    {
        Vector3 targetPosition = player.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition + new Vector3(0, 0, -10), Time.deltaTime * 8);
        // Sets the camera position to the same as the player but Lerp makes it ease
        // towards the player instead of being directly on top constantly
    }
}
