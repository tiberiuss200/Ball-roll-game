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
        turnSpd = 3;
        cameraTilt = 0.6f;
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerMovementDirection = rb.velocity;
        if(player.GetComponent<playerController>().sideInput != 0)
        {
            Quaternion rotation = Quaternion.LookRotation(-Vector3.up + cameraTilt * playerMovementDirection.normalized, playerMovementDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnSpd);
        }
        transform.position = player.transform.position - transform.forward*cameraDistance;
    }
}
