using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    readonly float horizontalSpeed = 100f;
    readonly float verticalSpeed = 100f;
    float xRotation = 0.0f;
    float yRotation = 0.0f;
    Transform player;
    void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);
        player.transform.eulerAngles = new Vector3(0, yRotation, 0);

        transform.position = player.position - transform.forward * 5f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Ground"))
        {
            transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        }
    }
}