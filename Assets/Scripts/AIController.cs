using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // The tank
    public GameObject tank;

    // List of waypoints
    public GameObject[] waypoints;

    // tank data
    public TankData tankData;

    //current wapoint position
    public Vector3 currentWaypointPosition;

    //The max FOV angle
    public float maxAngle;


    public float maxSightRadius;

    public float maxHearingDistance;

    public enum Personalities
    {
        //Once in sight, he will constantly pursue you
        Chase,
        //Once in sight, he will flee and once far enough he will fire once and start patrolling
        Flee,
        //Has better range and will fire from afar
        Ranger,
        //Only fires when he is out of your view
        Sneak
    }

    //index for the waypoint list
    public int wayPointIndex = 1;


    // This is the direction to the player's tanks
    private Vector3 direction;

    //Angle 
    private float angle;

    //Direction
    private Vector3 directionWaypoint;

    //Distance
    private float distance;

    //Offset
    private Vector3 offset;

    // left angle to draw the FOV
    private Vector3 leftAngle;

    // right angle to draw the FOV
    private Vector3 rightAngle;

    private float hearingDistance;



    // Update is called once per frame
    void Update()
    {
        // sends message to Fire the missile
        SendMessage("FireMissile");

        // Distance between the target and current transform
        distance = Vector3.Distance(transform.position, waypoints[wayPointIndex].transform.position);

        //Move
        Move();

        //Rotate
        Rotate();

        //Check if the player is in the sight of our AI tank
        FOV();

        Hearing();
    }

    // Move Method
    void Move()
    {

        // If distance is less than one
        if (distance < 1)
        {

            // set waypointposition to the index's position
            currentWaypointPosition = waypoints[wayPointIndex].transform.position;

            //Increase the waypoint
            IncreaseWaypointIndex();
        }
        else
        {
            // set the x of offset to the wapoint's x 
            offset.x = waypoints[wayPointIndex].transform.position.x;

            // swt the offset's y to our current transform's y position
            offset.y = transform.position.y;

            //// set the x of offset to the waypoint's x 
            offset.z = waypoints[wayPointIndex].transform.position.z;

            // Move the tank towards the waypoint
            transform.position = Vector3.MoveTowards(transform.position, offset, Time.deltaTime * tankData.moveSpeed);
        }
    }

    //Rotate Method
    void Rotate()
    {

        // set Direction to current position - waypoint position
        directionWaypoint = transform.position - waypoints[wayPointIndex].transform.position;

        // Set rotation to direction
        Quaternion rotation = Quaternion.LookRotation(directionWaypoint);

        //Lerp our rotation to the desired rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, tankData.rotateSpeed * Time.deltaTime);
    }

    //Increase Waypoint nMethod
    void IncreaseWaypointIndex()
    {
        //If the index is greater than the length
        if (wayPointIndex >= waypoints.Length - 1)
        {
            // Reset the index
            wayPointIndex = 0;
        }
        else
        {
            // Increase the index by one
            wayPointIndex++;
        }
    }


    //tHE FOLD OF VIEW THAT WE USE FOR THE ai'S SIGHT
    void FOV()
    {

        //Left angle to draw
        leftAngle = Quaternion.AngleAxis(maxAngle / 2, transform.up) * -transform.forward * maxSightRadius;
        rightAngle = Quaternion.AngleAxis(-maxAngle / 2, transform.up) * -transform.forward * maxSightRadius;

        Debug.DrawRay(transform.position, leftAngle, Color.red);
        Debug.DrawRay(transform.position, rightAngle, Color.red);
        direction = (tank.transform.position - transform.position);
        angle = Vector3.Angle(direction, -transform.forward);
        if (angle < maxAngle * 0.5f)
        {
            Debug.DrawLine(transform.position, tank.transform.position);
            print("Player in FOV");
        }
    }

    void Hearing()
    {

        hearingDistance = Vector3.Distance(transform.position, tank.transform.position);

        if (hearingDistance < maxHearingDistance)
        {
            print("Player is heard");
        }
    }
}