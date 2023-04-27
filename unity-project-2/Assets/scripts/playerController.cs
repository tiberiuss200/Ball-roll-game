/************************************************************
 * HOW TO USE:
 * 
 * All cheese pieces must have the "pickup" tag
 * The end hole must be a physical object with the "hole" tag
 * The cat must have the "Enemy" tag
 * 
 * ----------------------------------------------------------
 * 
 * For the script to work properly, I need Tiberius to add
 * the script for changing scenes and for the player's
 * position reset when they touch the cat
 * 
 * Speed will need to be edited within the script iself; I
 * did that for consistency's sake, so the speed isn't
 * different across different levels
 * 
 ************************************************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerController : MonoBehaviour
{
    public float speed;
    public int cheeseGoal;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject playerCamera;
    private Rigidbody rb;
    public int count;
    public float sideInput;
    public float forwardInput;
    private Vector3 playerStartPos;
    public AudioSource sound1;
    public AudioSource sound2;

    // Start is called before the first frame update
    void Start()
    {
        playerStartPos = transform.position;
        speed = 15;
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        sideInput = movementVector.x;
        forwardInput = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= cheeseGoal)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        float angleToVector;
        Vector3 cameraRotation = playerCamera.gameObject.transform.forward;
        Vector2 inputVector = new Vector2(sideInput, forwardInput);
        if(cameraRotation.x > 0) //Finds the angle the player is currently facing and stores it in the angleToVector float
        {
            angleToVector = Vector2.Angle(new Vector2(cameraRotation.x, cameraRotation.z), new Vector2(0, 1));
        }
        else
        {
            angleToVector = Vector2.Angle(new Vector2(cameraRotation.x, cameraRotation.z), new Vector2(0, -1)) + 180f;
        }
        Debug.Log(angleToVector);
        
        Vector3 finalMovementVector = Quaternion.AngleAxis(angleToVector, Vector3.up) * new Vector3(sideInput, 0, forwardInput);
        
        rb.AddForce(finalMovementVector * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            sound1.Play();
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("hole") && count >= cheeseGoal) //If the player touches the hole after getting all of the cheese
        {
            //TIBERIUS, THE END LEVEL SCRIPT GOES HERE*************************************************************************************************************************************
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            sound2.Play();
            transform.position = playerStartPos;
        }
    }
}
