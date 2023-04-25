using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TargetMovement : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float moveSpeed = 1.0f;

    private Vector3 startPosition;

    void Start()
    {
        // Record the starting position of the target
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position of the target based on the current time
        float newY = startPosition.y + moveDistance * Mathf.Sin(Time.time * moveSpeed);

        // Update the target's position with the new Y position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}