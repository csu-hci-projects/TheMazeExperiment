using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public class CSVWriter : MonoBehaviour

{
    string participant;
    string filePath;

    public bool FristLineCheck() {

        participant = PersistentManager.Instance.participantNumber.ToString();
        filePath = Directory.GetCurrentDirectory()+ "/ExcelResults/Participant_" + participant + ".csv";

        if (!File.Exists(filePath))
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/ExcelResults");

            try
            {
                using (StreamWriter file = new StreamWriter(@filePath, true))
                {

                    file.WriteLine("ParticipantID, DataType, MazeType, MazeNumber, AttemptNumber, Movement, Error, AudioCue, AudioAnswer, Time, MazeLocation, Gender, VideoGame");
                    return true;

                }
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }

        }
        else {
            return true;
        }

        return false;
    }

    public void Writer(string line) {

        try
        {
            using (StreamWriter file = File.AppendText(filePath)) 
            {
                file.WriteLine(line);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }

    }

    public string LastLine() {

        try
        {
            string last = File.ReadLines(filePath).Last();
            return last;

        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            return "";
        }

    }

}
