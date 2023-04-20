/*******************************************************************************************************

HOW TO USE:

The cat must be on the "Enemy" layer
Every wall must have a "Walls" tag
The hamster must have the "Player" tag

--------------------------------------------------------------------------------------------------------

Speed will change the speed the cat runs in

See Distance will change the distance at which the cat can start chasing the player

Turn Speed will change how fast the cat turns around

Chase Time will change for how long the cat chases the player after they go around a corner

Make sure the enemy layer is set to a new layer “Enemy”

*********************************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float seeDistance = 100;
    public float turnSpeed;
    public float chaseTime;
    private bool reachedPlayerPos;
    private float distance;
    private float distanceToLastKnownPlayerPos;
    private float lastKnownPlayerX;
    private float lastKnownPlayerZ;
    private float[] weightArray = new float[16];
    private float timer;
    public LayerMask EnemyLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position); //distance = the distance between the cat and player

        distanceToLastKnownPlayerPos = Vector3.Distance(transform.position, new Vector3(lastKnownPlayerX, 0f, lastKnownPlayerZ));
        Array.Clear(weightArray, 0, weightArray.Length);//Clears the weight array

        RaycastHit hitInfo;
        Vector3 vectorToPlayer = player.transform.position - transform.position;//Calculates a vector to the player's position

        Ray ray = new Ray(transform.position, vectorToPlayer);//Sends a ray from the enemy to the player to see if the cat can see the player

        if (distanceToLastKnownPlayerPos <= 3.5f)
        {
            reachedPlayerPos = true;//Detects if the cat has reached the last known player position, and should start wandering the maze
        }

        if (Physics.Raycast(ray, out hitInfo, seeDistance, ~EnemyLayer) && (hitInfo.transform.tag == "Player"))
        { //If the cat can see the player
            lastKnownPlayerX = player.transform.position.x;//Store the last known position of the player
            lastKnownPlayerZ = player.transform.position.z;
            timer = chaseTime;//Reset the timer for how long the cat should chase the player after the player disappears
            reachedPlayerPos = false;//This boolean lets the cat know whether it has already reached the last position it saw the player in, and lets it wander if it has

            checkWalls(ref weightArray);
            addWeightInVector(vectorToPlayer, ref weightArray, 5, 6);
            moveInDirectionOfWeight(ref weightArray, speed, turnSpeed);
        }
        else if (timer >= 0)
        {//Otherwise, if the cat is still chasing the player
            if (!reachedPlayerPos)
            {
                Vector3 vectorToLastKnownPlayerPos = new Vector3(lastKnownPlayerX - transform.position.x, 0, lastKnownPlayerZ - transform.position.z);
                checkWalls(ref weightArray);
                addWeightInVector(vectorToLastKnownPlayerPos, ref weightArray, 5, 6);
                moveInDirectionOfWeight(ref weightArray, speed, turnSpeed);
            }
            else
            {
                Vector3 currentVector = -transform.forward;
                checkWalls(ref weightArray);
                addWeightInVector(currentVector, ref weightArray, 3, 3);
                moveInDirectionOfWeight(ref weightArray, speed, turnSpeed);
            }

            timer -= 1.0f * Time.deltaTime;
        }
        //Vector3 test = new Vector3(1f,2f,3f);

    }

    void checkWalls(ref float[] array) //A function to check for the walls around the cat, and lower the weight for the directions towards the walls and around those directions
    {
        for (int i = 0; i < 16; i++)
        {
            RaycastHit wallCheckInfo;
            Vector3 wallCheckDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * (i * 22.5f)), 0, Mathf.Cos(Mathf.Deg2Rad * (i * 22.5f))); //Effectively uses the array to check for walls around the player, subtracting weight if it hits one
            Ray currentWallChecker = new Ray(transform.position, wallCheckDirection);

            if (Physics.Raycast(currentWallChecker, out wallCheckInfo, 1f, ~EnemyLayer) && (wallCheckInfo.transform.tag == "Walls"))
            { //The wall has to be within 2 units to be detected
                addWeightInVector(wallCheckDirection, ref array, -2, 4);
            }

        }
    }

    void addWeightInVector(Vector3 direction, ref float[] array, int weightToAdd, int range)
    {
        int highestWeightDirection;
        float angleFromNorthToTarget;
        angleFromNorthToTarget = ((Mathf.Atan2(direction.z, direction.x) * 180f) / 3.141593f) / 22.5f + 8; //Gets an angle from north to the target from -180 to 180, then divides it to put it in terms of 16
        if (angleFromNorthToTarget < 15.5)
        {
            highestWeightDirection = (int)Mathf.Round(angleFromNorthToTarget); //Sets the direction to put the highest weight to the closest 
        }
        else//If it goes to a high enough angle, this wraps the direction back to north
        {
            highestWeightDirection = 0;

        }
        array[highestWeightDirection] = weightToAdd;
        for (float i = 1; i < range; i++)
        {
            if (highestWeightDirection + i > 15)
            {
                array[highestWeightDirection + (int)i - 16] += ((0.1f * (weightToAdd / i)) + weightToAdd);
            }
            else
            {
                array[highestWeightDirection + (int)i] += ((0.1f * (weightToAdd / i)) + weightToAdd);
            }
            if (highestWeightDirection - i < 0)
            {
                array[highestWeightDirection - (int)i + 16] += ((0.1f * (weightToAdd / i)) + weightToAdd);
            }
            else
            {
                array[highestWeightDirection - (int)i] += ((0.1f * (weightToAdd / i)) + weightToAdd);
            }
        }
    }

    void moveInDirectionOfWeight(ref float[] array, float spd, float turnSpd)
    {
        float bestDirection = 0;
        float highestWeight = array[0];
        for (int i = 1; i < 16; i++)
        {
            if (array[i] > highestWeight)
            {
                bestDirection = i;
                highestWeight = array[i];
            }
        }
        Vector3 movementSolution = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (bestDirection * 22.5f)), 0, Mathf.Sin(Mathf.Deg2Rad * (bestDirection * 22.5f)));

        Quaternion rotation = Quaternion.LookRotation(movementSolution, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnSpd);
        transform.position = Vector3.MoveTowards(this.transform.position, transform.position - movementSolution, spd * Time.deltaTime);

    }
}



