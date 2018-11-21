using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Room : MonoBehaviour
{
    //These are the columns and rows
    public int rows;
    public int cols;

    //room width
    private float roomWidth = 10.0f;
    //room height
    private float roomHeight = 10.0f;
    //Grid 
    private Room[,] grid;
    //gridPrefabs
    public GameObject[] gridPrefabs;
    //use for random rotations
    public float[] rotations;
    //Spawnpoints 
    public List<GameObject> spawnPoints;
    //The length of the spwanPOints
    public int wayPointsLength;

    //Navmesh surface to rebake
    private NavMeshSurface surface;
    //Our waypoint
    private GameObject waypoint;

    // Use this for initialization
    void Start()
    {
        //Set a random seed using DateToint
        UnityEngine.Random.InitState(Seed());

        // Generate Grid
        GenerateGrid();

        //Spawn the tanks
        SpawnTanks();


        //Set random patrol points
        RandomPatrolPoints();
    }

    //Spawn tanks
    public void SpawnTanks()
    {

        //Spawn the tanks and iterate through them
        foreach (MonoBehaviour tank in GameManager.instance.enemyTankData)
        {
            //Set the transform position to a random range
            tank.gameObject.transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.ToArray().Length)].transform.position;
            //Set the gameObject's avctive state to false
            tank.gameObject.SetActive(true);
        }

        //Set the player's position to random
        foreach (TankData item in GameManager.instance.tankData)
        {
            if (item.gameObject.activeInHierarchy)
            {
               
                item.gameObject.transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.ToArray().Length)].transform.position;
            }
        }

    }
    //Genereate grid
    public void GenerateGrid()
    {
        // Clear out the grid
        grid = new Room[rows, cols];

        // For each grid row...
        for (int i = 0; i < rows; i++)
        {
            // for each column in that row
            for (int j = 0; j < cols; j++)
            {
                // Figure out the location. 
                float xPosition = roomWidth * j;
                float zPosition = roomHeight * i;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);

                // Create a new grid at the appropriate location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, new Quaternion(0, rotations[UnityEngine.Random.Range(0, 4)], 0, 0)) as GameObject;

                // Set its parent
                tempRoomObj.transform.parent = this.transform;

                // Give it a meaningful name
                tempRoomObj.name = "Room_" + j + "," + i;

                // Get the room object
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                //Add the spawnpoint to the list from the tile generated
                spawnPoints.Add(tempRoomObj.transform.GetChild(1).gameObject);
                surface = tempRoomObj.GetComponent<NavMeshSurface>();
                surface.BuildNavMesh();
                // Save it to the grid array
                grid[j, i] = tempRoom;
                //Spawn the tanks
                SpawnTanks();
            }
        }
    }

    //RandomPatrolPoints
    public void RandomPatrolPoints()
    {
        //Get the data from the enemy tanks
        foreach (MonoBehaviour data in GameManager.instance.enemyTankData)
        {

            //get the controller
            AIController controller = data.gameObject.GetComponent<AIController>();

            //If i is less than wayPointsLength, iterate through
            for (int i = 0; i < wayPointsLength; i++)
            {
                //Add a waypoint
                controller.waypoints.Add(spawnPoints[UnityEngine.Random.Range(0, spawnPoints.ToArray().Length)]);
            }

        }
    }
    // Returns a random room
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }


    //Get the date and converts it into an integer
    public int Seed()
    {

        //If map of the day is true
        if (GameManager.instance.mapOfTheDay == true)
        {
            //Return today
            return System.DateTime.Now.Year + System.DateTime.Now.Month + System.DateTime.Now.Day;
        }

        //If our InputField was not empty
        else if (!string.IsNullOrEmpty(GameManager.instance.seedText))
        {
            print("String was not null so we are using the text");
            //Iterate through each char
            foreach (char item in GameManager.instance.seedText)
            {
                //this gives us a numeric value to use
                int a = item - 0;
                //add a to our seed
                GameManager.instance.seed += a;
                //return our seed
                print("Seed:" + GameManager.instance.seed);
                return GameManager.instance.seed;

            }

        }
        //Return today, hours, minutes,seconds,milliseconds
        return System.DateTime.Now.Year + System.DateTime.Now.Month + System.DateTime.Now.Day + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + System.DateTime.Now.Millisecond;



    }

}

