using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIController : MonoBehaviour
{
    // The tank
    public GameObject tank;

    // List of waypoints
    public List<GameObject> waypoints;

    //Our material
    public Material material;

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

    //Personality variable
    public Personalities personality;

    //speed
    public float speed;

    //List of personalities
    public enum Personalities
    {
        //Once in sight, he will constantly pursue you
        Chase,

        //Once in sight, he will flee and once far enough he will fire once and start patrolling
        Flee,

        //Has better range and will fire from afar
        Ranger,

        //Patrols and has vision/sight
        Normal
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

    //Our current state
    public static States state;

    //index for the waypoint list
    public int wayPointIndex = 1;

    //Navmesh agent
    public NavMeshAgent agent;

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
        try
        {
            //If the waypoint is not null
            if (waypoints[wayPointIndex] != null)
            {
                //set float to Vector3.Distance
                distance = Vector3.Distance(transform.position, waypoints[wayPointIndex].transform.position);
            }
        }
        //Catch any possible exception
        catch (System.Exception e)
        {

            print(e.Message);
        }

        //Personlity method
        Personality();
        //Inputcheck
        //InputCheck();
    }

    //Input check for the keyboard
    //void InputCheck()
    //{

    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        personality = Personalities.Normal;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        personality = Personalities.Chase;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        personality = Personalities.Flee;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha4))
    //    {
    //        personality = Personalities.Ranger;
    //    }

    //}

    //Check the personality
    void Personality()
    {
        //Normal
        if (personality == Personalities.Normal)
        {
            //color is grey
            material.color = Color.grey;
            //Run through the states of the tank
            State();

            //Check if the player is in the sight of our AI tank
            FOV();

            //Do hearing emthod
            Hearing();
        }
        //Chase
        else if (personality == Personalities.Chase)
        {
            //green
            material.color = Color.green;
            //Chase the player
            Chase();
        }
        //Flee
        else if (personality == Personalities.Flee)
        {
            //red
            material.color = Color.red;
            //Move away from the player
            MoveAway();
        }

        // ranger
        else if (personality == Personalities.Ranger)
        {
            //blue
            material.color = Color.blue;
            //Rotate to the player
            Rotate(tank.transform.position);
        }
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
        //distance float
        float distanceFromPlayer;

        //The distance from our player's position to our postition
        distanceFromPlayer = Vector3.Distance(GameManager.instance.tankData.transform.position, transform.position);

        if (distanceFromPlayer < 8 && personality == Personalities.Normal)
        {
            //Rotate the tank towards the player
            Rotate(tank.transform.position);

            //Move towards the player
            MoveToPlayer();
        }
        else if (personality == Personalities.Chase)
        {
            //Rotate the tank towards the player
            Rotate(tank.transform.position);

            //Move towards the player
            MoveToPlayer();
        }

        //Set the state to patrolling
        else
        {
            state = States.Patrolling;

        }
    }


    // Move Method
    void Patrol()
    {

        // If distance is less than one
        if (distance < 5)
        {


            try
            {
                if (waypoints[wayPointIndex])
                {
                    // set waypointposition to the index's position
                    currentWaypointPosition = waypoints[wayPointIndex].transform.position;
                }
            }
            catch (System.Exception e)
            {

                print(e.Message);
            }


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
            transform.position = Vector3.MoveTowards(transform.position, waypoints[wayPointIndex].transform.position, speed);

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
        if (wayPointIndex >= waypoints.ToArray().Length - 1)
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

            //Rotate the tank towards the player
            Rotate(tank.transform.position);

            //change state to chase
            state = States.Chase;
        }
    }

    //Is the player heard
    void Hearing()
    {
        //hearing distance to check
        hearingDistance = Vector3.Distance(transform.position, GameManager.instance.transform.position);

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
        //distance
        float distance = Vector3.Distance(tank.transform.position, transform.position);

        //if the distance is less than fleeingDistance
        if (distance < fleeingDistance)
        {
            //Move away from the target
            MoveAway();
        }
        else
        {
            //state is equal to patrolling
            state = States.Patrolling;
        }
    }

    //Moves the tank to the player
    void MoveToPlayer()
    {
        //set the destination to the tank
        transform.position = Vector3.MoveTowards(transform.position, GameManager.instance.tankData.transform.position, Time.deltaTime * tankData.moveSpeed);
    }
    //Moves away from the player
    void MoveAway()
    {
        //move away from the player
        transform.position = Vector3.MoveTowards(transform.position, GameManager.instance.tankData.transform.position, -Time.deltaTime * tankData.moveSpeed);
    }
}