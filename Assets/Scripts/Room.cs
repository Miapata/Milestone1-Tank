using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Room : MonoBehaviour
{
    public int rows;
    public int cols;

    private float roomWidth = 10.0f;
    private float roomHeight = 10.0f;
    private Room[,] grid;
    public GameObject[] gridPrefabs;
    public float[] rotations;
    public List<GameObject> spawnPoints;
    public int wayPointsLength;
    private NavMeshSurface surface;
    private GameObject waypoint;
    // Use this for initialization
    void Start()
    {
        //Set the seed
        UnityEngine.Random.InitState(DateToInt(System.DateTime.Now));

        // Generate Grid
        GenerateGrid();

        //Spawn the tanks
        SpawnTanks();

        RandomPatrolPoints();
    }

    public void SpawnTanks()
    {
        Random.InitState(System.DateTime.Now.Second + System.DateTime.Now.Millisecond);
        //Spawn the tanks and iterate through them
        foreach (MonoBehaviour tank in GameManager.instance.enemyTankData)
        {
            tank.gameObject.transform.position = spawnPoints[Random.Range(0, spawnPoints.ToArray().Length)].transform.position;
            tank.gameObject.SetActive(true);
        }

        GameManager.instance.tankData.gameObject.transform.position = spawnPoints[Random.Range(0, spawnPoints.ToArray().Length)].transform.position;


    }
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
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, new Quaternion(0, rotations[Random.Range(0, 4)], 0, 0)) as GameObject;
                print(tempRoomObj.transform.rotation.y);

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
                SpawnTanks();
            }
        }
    }

    public void RandomPatrolPoints()
    {
        foreach (MonoBehaviour data in GameManager.instance.enemyTankData)
        {

            AIController controller = data.gameObject.GetComponent<AIController>();
            for (int i = 0; i < wayPointsLength; i++)
            {
                controller.waypoints.Add(spawnPoints[Random.Range(0, spawnPoints.ToArray().Length)]);
            }

        }
    }
    // Returns a random room
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[Random.Range(0, gridPrefabs.Length)];
    }


    //Get the date and converts it into an integer
    public int DateToInt(System.DateTime dateTime)
    {
        if (GameManager.instance.mapOfTheDay)
        {
            return dateTime.Year + dateTime.Month + dateTime.Day;
        }
        else
        {
            return dateTime.Year + dateTime.Month + dateTime.Day + dateTime.Hour + dateTime.Minute + dateTime.Second + dateTime.Millisecond;
        }


    }

}

