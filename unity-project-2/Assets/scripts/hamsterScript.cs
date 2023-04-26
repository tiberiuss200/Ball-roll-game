using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hamsterScript : MonoBehaviour
{
    GameObject player;
    private void Awake()
    {
        player = GameObject.Find("player");
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.6f, player.transform.position.z - 0.15f);
        transform.eulerAngles = new Vector3(0f, Camera.main.transform.rotation.y, 0f);
    }
}
