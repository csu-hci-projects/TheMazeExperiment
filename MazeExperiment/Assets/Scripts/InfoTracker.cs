using System;
using System.Collections.Generic;
using UnityEngine;

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
            print("In timeout: endtime - " + PersistentManager.Instance.endTime + "    now - " + now);
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
            if (PersistentManager.Instance.totalErrors == 0)
            {
                PersistentManager.Instance.perfectRuns++;

            }

            //Clear visitedList and reset errors made to 0
            visitedList.Clear();
            PersistentManager.Instance.totalErrors = 0;

            // Increment current attempts
            int currentAttempts = PersistentManager.Instance.currentAttempts;
            currentAttempts++;
            PersistentManager.Instance.currentAttempts = currentAttempts;

            // Set player prefs to show end
            PersistentManager.Instance.atEnd = true;

            // Change from maze scene
            GameObject.Find("SceneManager").GetComponent<SceneManagerScript>().ChangeFromMazeScene();


        }

    }

    public void Visted()
    {
        for (int i = 0; i < visitedList.Count; i++)
        {
            if ((visitedList[i][0] == PersistentManager.Instance.currentLocation[0]) && (visitedList[i][1] == PersistentManager.Instance.currentLocation[1]))
            {
                PersistentManager.Instance.totalErrors++;
            }
        }

        visitedList.Add(PersistentManager.Instance.currentLocation);
    }

    public void SetDirection(string direction, string mazeType, string audioAnswer = "")
    {

        //file.WriteLine("ParticipantID, DataType, MazeType, MazeNumber, AttemptNumber, Movement, Error, AudioCue, AudioAnswer, Time, MazeLocation, Gender, VideoGame");

        // Only record data on experimental mazes
        if (!PersistentManager.Instance.isPractice)
        {

            string currentLocation = "(" + PersistentManager.Instance.currentLocation[0].ToString() + " : " + PersistentManager.Instance.currentLocation[1].ToString() + ")";

            //Ego maze update .csv file
            if (mazeType.Equals("Ego"))
            {
                string audioQue = GameObject.Find("Audio Source").GetComponent<EgoAudio>().GetAudioCheck();

                if (PersistentManager.Instance.isTraining)
                {
                    csv.Writer(PersistentManager.Instance.participantNumber.ToString() + ", D, "
                         + mazeType + ", " + PersistentManager.Instance.mazeArrayValue.ToString() + ", " +
                         (PersistentManager.Instance.currentAttempts + 1).ToString() + ", " + direction + ", " + 
                         PersistentManager.Instance.totalErrors.ToString() + ", " + audioQue
                         + ", " + audioAnswer + ", " + DateTime.Now.ToString() + ", " + currentLocation);
                }
                else if (PersistentManager.Instance.isTesting)
                {
                    csv.Writer(PersistentManager.Instance.participantNumber.ToString() + ", T, "
                         + mazeType + ", " + PersistentManager.Instance.mazeArrayValue.ToString() + ", " +
                         (PersistentManager.Instance.currentAttempts + 1).ToString() + ", " + direction + ", " + 
                         PersistentManager.Instance.totalErrors.ToString() + ", " + audioQue
                         + ", " + audioAnswer + ", " + DateTime.Now.ToString() + ", " + currentLocation);
                }
            }

            //Allo maze update .csv file
            else
            {

                string audioQue = GameObject.Find("Audio Source").GetComponent<AlloAudio>().GetAudioCheck();

                if (PersistentManager.Instance.isTraining)
                {
                    csv.Writer(PersistentManager.Instance.participantNumber.ToString() + ", D, "
                         + mazeType + ", " + PersistentManager.Instance.mazeArrayValue.ToString() + ", " +
                         (PersistentManager.Instance.currentAttempts + 1).ToString() + ", " + direction + ", " + 
                         PersistentManager.Instance.totalErrors.ToString() + ", " + audioQue
                         + ", " + audioAnswer + ", " + DateTime.Now.ToString() + ", " + currentLocation);
                }
                else if (PersistentManager.Instance.isTesting)
                {
                    csv.Writer(PersistentManager.Instance.participantNumber.ToString() + ", T, "
                         + mazeType + ", " + PersistentManager.Instance.mazeArrayValue.ToString() + ", " +
                         (PersistentManager.Instance.currentAttempts + 1).ToString() + ", " + direction + ", " + 
                         PersistentManager.Instance.totalErrors.ToString() + ", " + audioQue
                         + ", " + audioAnswer + ", " + DateTime.Now.ToString() + ", " + currentLocation);
                }

            }
        }

    }

}
