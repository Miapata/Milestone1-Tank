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

    //index for the waypoint list
    public int wayPointIndex = 1;

    //Direction
    private Vector3 direction;

    //Distance
    private float distance;

    //Offset
    private Vector3 offset;
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
        direction = transform.position - waypoints[wayPointIndex].transform.position;

        // Set rotation to direction
        Quaternion rotation= Quaternion.LookRotation(direction);

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
}