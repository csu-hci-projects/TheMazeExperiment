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
   

    // Maze Vars 
    public int MazeType; // 0 is Ego // 1 is Allo // 2 is Both
    public List<int> listOfMazes;
    public int mazeArrayIndex;
    public int mazeArrayValue;
    public string[,] mazeArray;
    public int mazeSize;
    public bool firstTimeIn;
    // Path coloring
    public List<Material> colorList;
    public List<Material> lightColorList;
    // Maze locations
    public int[] startLocation;
    public int[] endLocation;

    
    
    // Errors
    public int perfectRuns;
    public int totalErrors;
    
    // Audio
    public string audioAnswer;

    // Player Vars
    public int[] currentLocation;
    public int currentAttempts;
    public bool timedOut;
    public bool atEnd;

    // Constraints 
    public int participantNumber;
    public bool egoCentric;
    public bool alloCentric;
    public string condition;
    public double timeOut;
    public int numPracticeAttempts = 2;
    public int numAttempts;
    public int numTestingAttempts = 2;
    public int numSuccessfulAttempts;
    public bool disableAudio;

    // Survey Data
    public string Gender;
    public int Hours;

    // Timeout
    public DateTime endTime;
    public DateTime startSpanTime;

    // Random Start Position
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
            mazeArrayIndex = 0;
            mazeArrayValue = 0;
            mazeArray = new string[15, 15];
            mazeSize = 0;
            firstTimeIn = true;
            // Path Coloring
            colorList = new List<Material>();
            lightColorList = new List<Material>();
            // Maze Locations
            startLocation = new int[] { 0, 0 };

            // Errors
            perfectRuns = 0;
            totalErrors = 0;

            // Audio
            audioAnswer = "";

            // Player Vars
            currentLocation = new int[] { 0, 0 };
            currentAttempts = 0;
            timedOut = false;
            atEnd = false;

            // Constraints
            participantNumber = -1;
            egoCentric = false;
            alloCentric = false;
            condition = "A";
            timeOut = -1;
            numAttempts = -1;
            numSuccessfulAttempts = -1;
            disableAudio = false;

            // Survey Data
            Gender = "Male";
            Hours = -1;

            // Timout
            endTime = DateTime.Now;
            
            // Random Start Position
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
