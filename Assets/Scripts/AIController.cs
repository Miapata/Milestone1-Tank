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

    //max fleeing distance
    public float fleeingDistance;

    //max sight
    public float maxSightRadius;

    //max hearing
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

    //Enum for our states
    public enum States
    {
        //Move to waypoints
        Patrolling,

        //Chase the player
        Chase,

        //Flee from the player
        Flee
    }

    public static States state;

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

    //hearing distance
    private float hearingDistance;

    private void Start()
    {
        //State is patrolling on start
        state = States.Patrolling;
    }

    // Update is called once per frame
    void Update()
    {
        // sends message to Fire the missile
        SendMessage("FireMissile");

        // Distance between the target and current transform
        distance = Vector3.Distance(transform.position, waypoints[wayPointIndex].transform.position);

        //Run through the states of the tank
        State();

        //Check if the player is in the sight of our AI tank
        FOV();

        //Do hearing emthod
        Hearing();
    }

    //Check state
    void State()
    {
        //switch through the differnet states 
        switch (state)
        {
            //If state is Patrolling
            case States.Patrolling:

                //Patrol through waypoints
                Patrol();

                break;

            case States.Chase:
                //Chase the player
                Chase();
                break;

            case States.Flee:
                //Flee form the player
                Flee();
                break;

            default:
                break;
        }

    }


    //Chase the player
    void Chase()
    {
        //distnace float
        float distance;

        //The distance from our player's position to our postition
        distance = Vector2.Distance(tank.transform.position, transform.position);

        //Rotate the tank towards the player
        Rotate(tank.transform.position);

        //Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, tank.transform.position, Time.deltaTime * tankData.moveSpeed);
    }


    // Move Method
    void Patrol()
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

            //Rotate towards the waypoint
            Rotate(waypoints[wayPointIndex].transform.position);
        }
    }

    //Rotate Method
    void Rotate(Vector3 position)
    {

        // set Direction to current position - waypoint position
        directionWaypoint = transform.position - position;

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


    //FOV to tell if the player is in sight
    void FOV()
    {

        //Left angle to draw
        leftAngle = Quaternion.AngleAxis(maxAngle / 2, transform.up) * -transform.forward * maxSightRadius;

        //right angle
        rightAngle = Quaternion.AngleAxis(-maxAngle / 2, transform.up) * -transform.forward * maxSightRadius;

        //Draw a ray for the left angle
        Debug.DrawRay(transform.position, leftAngle, Color.red);
        //Draw a ray for the right angle
        Debug.DrawRay(transform.position, rightAngle, Color.red);

        //direction 
        direction = (tank.transform.position - transform.position);

        //angle
        angle = Vector3.Angle(direction, -transform.forward);

        if (angle < maxAngle * 0.5f)
        {
            //Draw a line to the player's tank
            Debug.DrawLine(transform.position, tank.transform.position);

            print("Player in FOV");

            //change state to chase
            state = States.Chase;
        }
    }

    //Is the player heard
    void Hearing()
    {
        //hearing distance to check
        hearingDistance = Vector3.Distance(transform.position, tank.transform.position);

        //if the hearing distance is less than our max hearing distance
        if (hearingDistance < maxHearingDistance)
        {
            print("Player is heard");

            //Start chasing the player
            state = States.Chase;
        }

    }

    // Sendmessage to change the state
    void StartFleeing()
    {
        //Change state to Flee
        state = States.Flee;
    }

    //Flee method
    void Flee()
    {
        //distnace 
        float distance = Vector3.Distance(tank.transform.position, transform.position);

        //if the distance is less than fleeingDistance
        if (distance < fleeingDistance)
        {
            //Move away from the target
            transform.position = Vector3.MoveTowards(transform.position, tank.transform.position, -Time.deltaTime * tankData.moveSpeed);
        }
        else
        {
            //state is equal to patrolling
            state = States.Patrolling;
        }
    }
}