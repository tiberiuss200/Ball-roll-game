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
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private Vector3 playerStartPos;

    // Start is called before the first frame update
    void Start()
    {
        playerStartPos = transform.position;
        speed = 10;
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

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
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
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
            transform.position = playerStartPos;
        }
    }
}
