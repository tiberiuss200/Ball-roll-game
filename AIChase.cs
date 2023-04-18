using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{

    //This is a test to see if I know how to use GIT
    public GameObject player;
    public float speed;
    public float turnSpeed;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        
        if(distance > 0.5 && distance < 10)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

            Vector3 endDirection = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(endDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
        }
        Vector3 test = new Vector3(1f,2f,3f);
        
    }
}
