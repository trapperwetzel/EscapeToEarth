using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//written by Julian Van Beusekom
//12/5/24

public class SwordFollower : MonoBehaviour
{
    public Transform handPosition; // Assign the Empty GameObject near the hand
    public Vector3 offset; // Adjust for precise positioning

    void Update()
    {
        if (handPosition != null)
        {
            // Update the sword's position and rotation to match the hand
            transform.position = handPosition.position + offset;
            transform.rotation = handPosition.rotation;
        }
    }
}

