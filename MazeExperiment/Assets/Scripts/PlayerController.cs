using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    
    public int MoveSpeed = 2;
    public float TurnSpeed = 0.2f;
    public SceneManagerScript sceneManager;

    private Vector3 toPosition;
    private Vector3 offset = new Vector3(0, 3, -7);
    private bool allowInput = true;
    private bool movingToTarget = false;
    private static int mazeSize;
    private string[,] mazeArray;
    private int[] playerPosition = { 0, 0 };

    private void Start()
    {
        //initialize position to move to, to the current position
        toPosition = transform.position;

        // Read maze into array from file in PlayerPrefs and set start
        mazeArray = ReadMazeFile();
        SetStartingPosition();
    }

    // Update is called once per frame
    void Update()
    {

        // Check if the player is allowed to press another key
        if (allowInput) {

            // Rotate left
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
            {
                // Stop taking player input until coroutine ends
                allowInput = false;

                StartCoroutine(Rotate90(Vector3.up * -90, TurnSpeed));
            }
            // Rotate right
            else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
            {
                // Stop taking player input until coroutine ends
                allowInput = false;

                StartCoroutine(Rotate90(Vector3.up * 90, TurnSpeed));
            }
            // Move forward
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Stop taking player input until coroutine ends
                allowInput = false;
               
                StartCoroutine(MovetoIntersection((moveToVector) => {

                    // Set new vector to move to
                    toPosition = moveToVector;

                    // If new vector isn't the same as current, a valid move was made
                    if (!toPosition.Equals(player.transform.position))
                    {
                        // Set moving to true
                        movingToTarget = true;
                    }
                    else
                    {
                        // New vector is the same, no movement needed so allow input again
                        allowInput = true;
                    }
                }));
            }
        }

        // A valid move vector has been found and the player needs to move to it.
        if (movingToTarget == true)
        {
            // Get direction of player
            int direction = (int)transform.eulerAngles.y;
            int down = 90;
            int up = 270;
            int left = 180;
            int right = 0;

            if (direction == up)
            {
                // Move to new vector
                transform.position = Vector3.MoveTowards(transform.position, toPosition, MoveSpeed * Time.deltaTime);
                // Player reaches new vector
                if (NearlyEqual(transform.position.x, toPosition.x, 0.1f))
                {
                    CheckFinish();
                }
            }
            else if (direction == down)
            {
                // Move to new vector
                transform.position = Vector3.MoveTowards(transform.position, toPosition, MoveSpeed * Time.deltaTime);
                // Player reaches new vector
                if (NearlyEqual(transform.position.x, toPosition.x, 0.1f))
                {
                    CheckFinish();
                }
            }
            else if (direction == right)
            {
                // Move to new vector
                transform.position = Vector3.MoveTowards(transform.position, toPosition, MoveSpeed * Time.deltaTime);
                // Player reaches new vector
                if (NearlyEqual(transform.position.z, toPosition.z, 0.1f))
                {
                    CheckFinish();
                }
            }
            else if (direction == left)
            {
                // Move to new vector
                transform.position = Vector3.MoveTowards(transform.position, toPosition, MoveSpeed * Time.deltaTime);
                // Player reaches new vector
                if (NearlyEqual(transform.position.z, toPosition.z, 0.1f))
                {
                    CheckFinish();
                }
            }
            else
            {

            }


        }
        // Not moving, round and set current position (keep player in grid)
        else
        {
            Vector3 move;
            move.x = (float)Math.Round(transform.position.x);
            move.y = (float)Math.Round(transform.position.y);
            move.z = (float)Math.Round(transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, move, MoveSpeed * Time.deltaTime);
        }
    }

    private void CheckFinish()
    {
        if (mazeArray[playerPosition[0], playerPosition[1]] == "0F")
        {
            sceneManager.SwitchScene();
        }

        // Allow input again
        movingToTarget = false;
        allowInput = true;
    }

    // Rotation coroutine
    //This method was taken from the Unity help forums - user Rullaghn.
    private IEnumerator Rotate90(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        transform.rotation = toAngle;
        allowInput = true;
    }

    // Movement coroutine - returns vector to move to
    IEnumerator MovetoIntersection(System.Action<Vector3> callback)
    {
        // Get player direction
        int direction = (int)transform.eulerAngles.y;
        int down = 90;
        int up = 270;
        int left = 180;
        int right = 0;
        Vector3 moveVector = transform.position;

        // Player is trying to move up
        if (direction == up)
        {
            // Check above player position
            for (int row = playerPosition[0] - 1; row < mazeArray.GetLength(0); row--)
            {
                // Wall
                if (mazeArray[row, playerPosition[1]] == "1N")
                {
                    break;
                }
                // Intersection, End, Start
                else if (mazeArray[row, playerPosition[1]] == "0I" || mazeArray[row, playerPosition[1]] == "0F" || mazeArray[row, playerPosition[1]] == "0S")
                {
                    moveVector.x -= 1;
                    playerPosition[0] = row;
                    break;
                }
                // Empty - increment movement but don't break out
                else if (mazeArray[row, playerPosition[1]] == "0N")
                {
                    moveVector.x -= 1;
                    playerPosition[0] = row;
                }

            }
        }
        // Player is trying to move down
        else if (direction == down)
        {
            // Check below player position
            for (int row = playerPosition[0] + 1; row < mazeArray.GetLength(0); row++)
            {
                // Wall
                if (mazeArray[row, playerPosition[1]] == "1N")
                {
                    break;
                }
                // Intersection, End, Start
                else if (mazeArray[row, playerPosition[1]] == "0I" || mazeArray[row, playerPosition[1]] == "0F" || mazeArray[row, playerPosition[1]] == "0S")
                {
                    moveVector.x += 1;
                    playerPosition[0] = row;
                    break;
                }
                // Empty - increment movement but don't break out
                else if (mazeArray[row, playerPosition[1]] == "0N")
                {
                    moveVector.x += 1;
                    playerPosition[0] = row;
                }

            }

        }
        // Player is trying to move left
        else if (direction == left)
        {
            // Check to the left of player position
            for (int col = playerPosition[1] - 1; col < mazeArray.GetLength(1); col--)
            {
                // Wall
                if (mazeArray[playerPosition[0], col] == "1N")
                {
                    break;
                }
                // Intersection, End, Start
                else if (mazeArray[playerPosition[0], col] == "0I" || mazeArray[playerPosition[0], col] == "0F" || mazeArray[playerPosition[0], col] == "0S")
                {
                    moveVector.z -= 1;
                    playerPosition[1] = col;
                    break;
                }
                // Empty - increment movement but don't break out
                else if (mazeArray[playerPosition[0], col] == "0N")
                {
                    moveVector.z -= 1;
                    playerPosition[1] = col;
                }

            }

        }
        // Player is trying to move right
        else if (direction == right)
        {
            // Check to the right of player position
            for (int col = playerPosition[1] + 1; col < mazeArray.GetLength(1); col++)
            {
                // Wall
                if (mazeArray[playerPosition[0], col] == "1N")
                {
                    break;
                }
                // Intersection, End, Start
                else if (mazeArray[playerPosition[0], col] == "0I" || mazeArray[playerPosition[0], col] == "0F" || mazeArray[playerPosition[0], col] == "0S")
                {
                    moveVector.z += 1;
                    playerPosition[1] = col;
                    break;
                }
                // Empty - increment movement but don't break out
                else if (mazeArray[playerPosition[0], col] == "0N")
                {
                    moveVector.z += 1;
                    playerPosition[1] = col;
                }

            }

            
        }
        // Shouldn't happen (player isn't facing a 90 degree angle)
        else
        {
            yield return player.transform.position;
            callback(player.transform.position);

        }

        //print("MoveVector: " + moveVector.x + ", " + moveVector.y + ", " + moveVector.z);

        // Call to return vector from coroutine
        callback(moveVector);
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
    public static bool NearlyEqual(float a, float b, float epsilon)
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

    // Create 2D array based on maze file
    string[,] ReadMazeFile()
    {
        // TODO: Remove later, for testing purposes only. 
        PlayerPrefs.SetInt("mazeSize", 15);
        PlayerPrefs.SetString("file", "Maze-1.txt");

        // Set maze size from PlayerPrefs 
        mazeSize = PlayerPrefs.GetInt("mazeSize");

        // Create mazeArray with maze size 
        string[,] mazeArray = new string[mazeSize, mazeSize];

        // Get filename from PlayerPrefs
        string file = PlayerPrefs.GetString("file");

        // TODO: Add command line checks for which OS is running, then modify PATH from there to load file in
        //var currentOS = Environment.OSVersion.Platform();

        // Read file into mazeArray
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

        return mazeArray;
    }
}
