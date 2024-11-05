using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphereScore : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Vector3 movementDirection = Vector3.forward;

    private void Update()
    {
        // Move the sphere in the specified direction
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
    }
}

