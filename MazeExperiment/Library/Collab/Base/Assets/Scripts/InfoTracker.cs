using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Linq;

public class InfoTracker : MonoBehaviour
{

    public GameObject player;

    // Keep track of time
    private DateTime endTime;
    private double timeout;

    // For calling change maze scene 
    private MainMenu obj = new MainMenu();

    private CSVWriter csv;

    private List<int[]> visitedList = new List<int[]>();

    private int errorsMade = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Start timer and create timeout 
        //DateTime now = DateTime.Now;
        //timeout = Convert.ToDouble(PersistentManager.Instance.timeOut);
        //DateTime tmp = now.AddMinutes(timeout);
        //endTime = tmp;

        //print("endTime" + endTime.ToString());

        csv = gameObject.GetComponent<CSVWriter>();
        csv.FristLineCheck();
    }

    // Update is called once per frame
    void Update()
    {

        // Check if they timed out
        DateTime now = DateTime.Now;

        // Only during training
        if (now >= PersistentManager.Instance.endTime && PersistentManager.Instance.isTraining)
        {
            // Set player prefs to show timeout
            PersistentManager.Instance.timedOut = true;

            // Change maze scene
            print("In timeout: " + PersistentManager.Instance.endTime + "    " + now);
            GameObject.Find("SceneManager").GetComponent<SceneManagerScript>().ChangeScene(6);
            PersistentManager.Instance.timedOut = false;

        }

        // Check if at the end, change scene wether number of attempts is reached or not 
        int currentX = PersistentManager.Instance.currentLocation[0];
        int currentZ = PersistentManager.Instance.currentLocation[1];
        int endX = PersistentManager.Instance.endLocation[0];
        int endZ = PersistentManager.Instance.endLocation[1];

        // Increment attempts if at end
        if (currentX == endX && currentZ == endZ)
        {

            // Increment perfectRuns if errorsMade is 0
            if (errorsMade == 0) {
                PersistentManager.Instance.perfectRuns++;

            }

            //Clear visitedList and reset errors made to 0
            visitedList.Clear();
            errorsMade = 0;

            // Increment current attempts
            int currentAttempts = PersistentManager.Instance.currentAttempts;
            currentAttempts++;
            PersistentManager.Instance.currentAttempts = currentAttempts;

            // Set player prefs to show end
            PersistentManager.Instance.atEnd = true;

            // Change maze scene depending on ego or allo current
            if (PersistentManager.Instance.CurrentScene == 1)
            {
                GameObject.Find("SceneManager").GetComponent<SceneManagerScript>().ChangeFromEgocentric();
            } else
            {
                GameObject.Find("SceneManager").GetComponent<SceneManagerScript>().ChangeFromAllocentric();
            }

        }

    }

    public void Visted()
    {
        for (int i = 0; i < visitedList.Count; i++)
        {
            if ((visitedList[i][0] == PersistentManager.Instance.currentLocation[0]) && (visitedList[i][1] == PersistentManager.Instance.currentLocation[1]))
            {
                errorsMade++;
                print("Error has been made, total is: " + errorsMade.ToString());
            }
        }

        visitedList.Add(PersistentManager.Instance.currentLocation);
    }

    public void SetDirection(string direction, string mazeType)
    {

        //file.WriteLine("ParticipantID, DataType, MazeType, MazeNumber, AttemptNumber, Movement, Error, AudioCue, Time, MazeLocation, Gender, VideoGame");

        // Only record data on experimental mazes
        if (!PersistentManager.Instance.isPractice)
        {

            string currentLocation = "(" + PersistentManager.Instance.currentLocation[0].ToString() + " : " + PersistentManager.Instance.currentLocation[1].ToString() + ")";

            //Ego maze update .csv file
            if (mazeType.Equals("Ego"))
            {
                string audioQue = GameObject.Find("Audio Source").GetComponent<EgoAudio>().GetAudioCheck();


                csv.Writer(PersistentManager.Instance.participantNumber.ToString() + ", D, "
                     + mazeType + ", " + PersistentManager.Instance.mazeArrayValue.ToString() + ", " +
                     (PersistentManager.Instance.currentAttempts + 1).ToString() + ", " + direction + ", " + errorsMade.ToString() + ", " + audioQue
                     + ", " + DateTime.Now.ToString() + ", " + currentLocation);
            }

            //Allo maze update .csv file
            else
            {

                string audioQue = GameObject.Find("Audio Source").GetComponent<AlloAudio>().GetAudioCheck();

                csv.Writer(PersistentManager.Instance.participantNumber.ToString() + ", D, "
                     + mazeType + ", " + PersistentManager.Instance.mazeArrayValue.ToString() + ", " +
                     (PersistentManager.Instance.currentAttempts + 1).ToString() + ", " + direction + ", " + errorsMade.ToString() + ", " + audioQue
                     + ", " + DateTime.Now.ToString() + ", " + currentLocation);

            }
        }

    }

}
