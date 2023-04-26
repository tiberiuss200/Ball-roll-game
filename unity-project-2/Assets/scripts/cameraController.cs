using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject player;
    public float cameraDistance;
    public float turnSpd;
    public float cameraTilt;
    private Vector3 playerMovementDirection;
    Rigidbody rb;
    void Start()
    {
        cameraDistance = 4f;
        turnSpd = 5;
        cameraTilt = 1.5f;
        rb = player.GetComponent<Rigidbody>();
    }
    void LateUpdate()
    {
        playerMovementDirection = rb.velocity;
        if(rb.velocity.magnitude > 1)
        {
            Quaternion rotation = Quaternion.LookRotation(-Vector3.up + cameraTilt * playerMovementDirection.normalized, playerMovementDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnSpd);
        }

        transform.position = player.transform.position - transform.forward * 7f;
    }
}
