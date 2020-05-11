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
        // Call maze builder to setup mazearray and starting position
        GameObject.Find("Main Camera").GetComponent<MazeBuilder>().Setup();

        // Get maze from persistent manager
        mazeArray = PersistentManager.Instance.mazeArray;

        playerPosition = new  int[] { PersistentManager.Instance.startLocation[0], PersistentManager.Instance.startLocation[1] };
        PersistentManager.Instance.currentLocation = playerPosition;

    }

    // Update method to be overridden
    protected abstract void Update();


    // Check if player is on an end tile, allow input again
    protected void CheckFinish()
    {
        // Allow input again
        movingToTarget = false;
        allowInput = true;
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
