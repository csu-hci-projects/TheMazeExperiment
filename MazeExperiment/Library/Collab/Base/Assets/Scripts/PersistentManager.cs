using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton class to store the state of the application
public class PersistentManager : MonoBehaviour
{
    // Single instance of the class
    public static PersistentManager Instance { get; private set; }

    // Scene Vars
    public bool isPractice;
    public bool isTraining;
    public bool isTesting;
    public int CurrentScene;
    public int PreviousScene;
    public int MazeType; // 0 is Ego // 1 is Allo // 2 is Both

    // Maze Vars
    public List<int> listOfMazes;
    public string[,] mazeArray;
    public bool egoCentric;
    public bool alloCentric;
    public int mazeSize;
    public bool firstTimeIn;

    public List<Material> colorList;
    public List<Material> lightColorList;

    public int[] startLocation;
    public int[] endLocation;

    public int mazeArrayIndex;
    public int mazeArrayValue;

    public int perfectRuns;

    // Player Vars
    public int[] currentLocation;
    public int[] previousLocation;
    public int currentAttempts;
    public bool timedOut;
    public bool atEnd;

    // Constraints 
    public int participantNumber;
    public string condition;
    public double timeOut;
    public int numAttempts;
    public int numSuccessfulAttempts;

    // Survey Data
    public string Gender;
    public string Hours;

    public DateTime endTime;



    public int currentTestAttempts;
    public int maxTestAttempts;

    public int[] randomStartLocation;
    public Vector3 randomStartRotation;




    // Instantiation of first call
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Scene Vars
            isPractice = true;
            isTraining = false;
            isTesting = false;
            CurrentScene = 0;

            // Maze Vars
            listOfMazes = new List<int>();
            mazeArray = new string[15, 15];
            egoCentric = false;
            alloCentric = false;
            mazeSize = 0;
            firstTimeIn = true;

            colorList = new List<Material>();
            lightColorList = new List<Material>();

            mazeArrayIndex = 0;
            mazeArrayValue = 0;

            perfectRuns = 0;

            // Player Vars
            startLocation = new int[] { 0, 0 };
            currentLocation = new int[] { 0, 0 };
            previousLocation = new int[] { 0, 0 };
            currentAttempts = 0;
            timedOut = false;
            atEnd = false;

            // Constraints
            participantNumber = -1;
            condition = "A";
            timeOut = -1;
            numAttempts = -1;
            numSuccessfulAttempts = -1;

            // Survey Data
            Gender = "";
            Hours = "";

            endTime = DateTime.Now;



            currentTestAttempts = 0;
            maxTestAttempts = 2;

            randomStartLocation = new int[] { 0, 0 };
            randomStartRotation = new Vector3(0, 0, 0);


        }
        else
        {
            // Delete duplicate creations of Instance
            Destroy(gameObject);
        }
    }


}
