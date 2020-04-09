using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

abstract class PlayerController : MonoBehaviour
{
    //modifiable variables
    public GameObject player;
    public SceneManagerScript sceneManager;
    private int moveSpeed = 5;
    private float turnSpeed = 0.1f;

    //class variables
    protected string direction;
    protected Vector3 toPosition;
    protected Vector3 offset = new Vector3(0, 3, -7);
    protected bool allowInput = true;
    protected bool movingToTarget = false;
    protected static int mazeSize;
    protected string[,] mazeArray;
    protected int[] playerPosition = { 0, 0 };

    public int MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float TurnSpeed { get => turnSpeed; set => turnSpeed = value; }

    protected void Start()
    {
        // TODO: Check current scene to know ego or allo 
        // Call maze builder to setup mazearray and starting position
        GameObject.Find("Main Camera").GetComponent<MazeBuilder>().Setup();


        //initialize position to move to, to the current position
        //toPosition = transform.position;

        // Read maze into array from file in PlayerPrefs and set start
        //mazeArray = ReadMazeFile();
        mazeArray = PersistentManager.Instance.mazeArray;

        //SetStartingPosition();
        //PersistentManager.Instance.currentLocation = playerPosition;
        playerPosition = new  int[] { PersistentManager.Instance.startLocation[0], PersistentManager.Instance.startLocation[1] };
        PersistentManager.Instance.currentLocation = playerPosition;

    }

    // Update method to be overridden
    protected abstract void Update();

    // Read maze from PlayerPrefs file 
    public string[,] ReadMazeFile()
    {
        // TODO: Remove later, for testing purposes only. 
        PlayerPrefs.SetInt("mazeSize", 15);

        // Get current maze and set to file
        int index = PersistentManager.Instance.mazeArrayIndex;
        string file = "Maze-" + PersistentManager.Instance.listOfMazes[index].ToString() + ".txt";

        if (PersistentManager.Instance.isPractice)
        {
            file = "PracticeMaze.txt";
        }

        // Set maze size from PlayerPrefs 
        mazeSize = PlayerPrefs.GetInt("mazeSize");

        // Create mazeArray with maze size 
        string[,] mazeArray = new string[mazeSize, mazeSize];

        // TODO: Add command line checks for which OS is running, then modify PATH from there to load file in
        //var currentOS = Environment.OSVersion.Platform();

        // Read file into mazeArray
        //print("MazeBuilder: " + file);
        string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "/Assets/Mazes/" + file);

        // Read lines into mazeArray 
        for (int i = 0; i < lines.Length; i++)
        {
            string[] splitArray = lines[i].Split(' ');
            for (int j = 0; j < splitArray.Length; j++)
            {
                mazeArray[i, j] = splitArray[j];
            }
        }

        PersistentManager.Instance.mazeArray = mazeArray;

        return mazeArray;
    }

    // Check if player is on an end tile, then switch scene or allow input again
    protected void CheckFinish()
    {

        // Allow input again
        movingToTarget = false;
        allowInput = true;
    }

    // Set starting index
    void SetStartingPosition()
    {
        for (int row = 0; row < mazeArray.GetLength(0); row++)
        {
            for (int col = 0; col < mazeArray.GetLength(1); col++)
            {

                if (mazeArray[row, col] == "0S")
                {
                    playerPosition[0] = row;
                    playerPosition[1] = col;
                }

            }
        }
    }

    // Compares two floats - used for player position comparisons
    protected bool NearlyEqual(float a, float b, float epsilon)
    {
        float absA = Math.Abs(a);
        float absB = Math.Abs(b);
        float diff = Math.Abs(a - b);

        if (diff < epsilon)
        {
            return true;
        }
        return false;
    }

}
