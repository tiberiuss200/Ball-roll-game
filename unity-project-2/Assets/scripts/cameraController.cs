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
    private Vector3 offset;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        cameraDistance = 4f;
        turnSpd = 5;
        cameraTilt = 0.4f;
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerMovementDirection = rb.velocity;
        if(rb.velocity.magnitude > 1)
        {
            Quaternion rotation = Quaternion.LookRotation(-Vector3.up + cameraTilt * playerMovementDirection.normalized, playerMovementDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnSpd);
        }

        transform.position = player.transform.position + cameraDistance*Vector3.up;
    }
}
