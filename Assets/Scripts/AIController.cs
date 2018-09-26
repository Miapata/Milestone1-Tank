using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject tank;
    public GameObject[] waypoints;
    public TankData tankData;
    public Vector3 currentWaypointPosition;
    public int wayPointIndex = 1;

    private float distance;
    // Update is called once per frame
    void Update()
    {
        print(wayPointIndex);
        SendMessage("FireMissile"); // sends message to Fire the missile

        distance = Vector3.Distance(transform.position, waypoints[wayPointIndex].transform.position);
       
        Move();
        Rotate();

    }
    void Move()
    {

        if (distance < 1)
        {

            currentWaypointPosition = waypoints[wayPointIndex].transform.position;
            IncreaseWaypointIndex();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[wayPointIndex].transform.position, Time.deltaTime * tankData.moveSpeed);
        }
    }

    void Rotate()
    {
        Quaternion.RotateTowards(transform.rotation, waypoints[wayPointIndex].transform.rotation, Time.deltaTime * tankData.rotateSpeed);
    }

    void IncreaseWaypointIndex()
    {
        if (wayPointIndex >= waypoints.Length - 1)
        {
            wayPointIndex = 0;
        }
        else
        {
            wayPointIndex++;
        }
    }
}
