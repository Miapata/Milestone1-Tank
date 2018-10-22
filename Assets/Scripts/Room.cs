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

    // Update is called once per frame
    void Update()
    {

    }


    // Use this for initialization
    void Start()
    {
        // Generate Grid
        GenerateGrid();
        BakeSurfaces();
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


                // Save it to the grid array
                grid[j, i] = tempRoom;
            }
        }
    }

    void BakeSurfaces()
    {

    }

    // Returns a random room
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[Random.Range(0, gridPrefabs.Length)];
    }
}
