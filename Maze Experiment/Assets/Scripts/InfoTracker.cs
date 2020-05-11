using System;
using System.Collections.Generic;
using UnityEngine;

public class InfoTracker : MonoBehaviour
{

    public GameObject player;

    private CSVWriter csv;

    // Start is called before the first frame update
    void Start()
    {
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

    public void SetDirection(string direction, string mazeType, string audioAnswer = "")
    {

        //file.WriteLine("ParticipantID, DataType, MazeType, MazeNumber, AttemptNumber, Movement, Error, AudioType, AudioCue, AudioAnswer, Time, MazeLocation, Gender, VideoGame");

        // Only record data on experimental mazes
        if (!PersistentManager.Instance.isPractice)
        {

            string currentLocation = "(" + PersistentManager.Instance.currentLocation[0].ToString() + " : " + PersistentManager.Instance.currentLocation[1].ToString() + ")";

            string audioQue = "0";
            string audioType = "";

            if (PersistentManager.Instance.mazeArrayIndex < 5)
            {
                audioType = "Ego";
                audioQue = GameObject.Find("Audio Source").GetComponent<EgoAudio>().GetAudioCheck();
            }
            else if (PersistentManager.Instance.mazeArrayIndex >= 5 && PersistentManager.Instance.mazeArrayIndex < 10)
            {
                audioType = "Allo";
                audioQue = GameObject.Find("Audio Source").GetComponent<AlloAudio>().GetAudioCheck();
            }

            //Ego maze update .csv file
            if (mazeType.Equals("Ego"))
            {

                if (PersistentManager.Instance.isTraining)
                {
                    csv.Writer(PersistentManager.Instance.participantNumber.ToString() + ", D, "
                         + mazeType + ", " + PersistentManager.Instance.mazeArrayValue.ToString() + ", " +
                         (PersistentManager.Instance.currentAttempts + 1).ToString() + ", " + direction + ", " +
                         PersistentManager.Instance.totalErrors.ToString() + ", " + audioType + ", " + audioQue
                         + ", " + audioAnswer + ", " + DateTime.Now.ToString() + ", " + currentLocation);
                }
                else if (PersistentManager.Instance.isTesting)
                {
                    csv.Writer(PersistentManager.Instance.participantNumber.ToString() + ", T, "
                         + mazeType + ", " + PersistentManager.Instance.mazeArrayValue.ToString() + ", " +
                         (PersistentManager.Instance.currentAttempts + 1).ToString() + ", " + direction + ", " +
                         PersistentManager.Instance.totalErrors.ToString() + ", " + audioType + ", " + audioQue
                         + ", " + audioAnswer + ", " + DateTime.Now.ToString() + ", " + currentLocation);
                }
            }

            //Allo maze update .csv file
            else
            {

                if (PersistentManager.Instance.isTraining)
                {
                    csv.Writer(PersistentManager.Instance.participantNumber.ToString() + ", D, "
                         + mazeType + ", " + PersistentManager.Instance.mazeArrayValue.ToString() + ", " +
                         (PersistentManager.Instance.currentAttempts + 1).ToString() + ", " + direction + ", " +
                         PersistentManager.Instance.totalErrors.ToString() + ", " + audioType + ", " + audioQue
                         + ", " + audioAnswer + ", " + DateTime.Now.ToString() + ", " + currentLocation);
                }
                else if (PersistentManager.Instance.isTesting)
                {
                    csv.Writer(PersistentManager.Instance.participantNumber.ToString() + ", T, "
                         + mazeType + ", " + PersistentManager.Instance.mazeArrayValue.ToString() + ", " +
                         (PersistentManager.Instance.currentAttempts + 1).ToString() + ", " + direction + ", " +
                         PersistentManager.Instance.totalErrors.ToString() + ", " + audioType + ", " + audioQue
                         + ", " + audioAnswer + ", " + DateTime.Now.ToString() + ", " + currentLocation);
                }

            }
        }

    }

}