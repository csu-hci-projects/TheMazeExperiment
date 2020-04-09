using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SurveyInput : MonoBehaviour
{

    public Button NextButton;
    public Toggle MaleToggle;
    public Toggle FemaleToggle;
    public Toggle OtherToggle;
    public Toggle PreferNotToggle;
    public TMP_InputField HoursInput;

    public TMP_Text HoursError;
    public GameObject hoursPanel;

    private CSVWriter csv;
    private string lastLine;

    //file.WriteLine("ParticipantID, DataType, MazeType, MazeNumber, AttemptNumber, Movement, Error, AudioCue, Time, MazeLocation, Gender, VideoGame");

    public void SetMaleToggle(bool value)
    {
        if (value)
        {
            PersistentManager.Instance.Gender = "Male";
        }
        
    }

    public void SetFemaleToggle(bool value)
    {
        if (value)
        {
            PersistentManager.Instance.Gender = "Female";
        }
    }

    public void SetOtherToggle(bool value)
    {
        if (value)
        {
            PersistentManager.Instance.Gender = "Other";
        }
    }

    public void SetPreferNotToggle(bool value)
    {
        if (value)
        {
            PersistentManager.Instance.Gender = "Prefer not to say";
        }
    }

    public void SetHoursInput(string value)
    {
        bool canConvert = int.TryParse(value, out PersistentManager.Instance.Hours);

        if (canConvert && PersistentManager.Instance.Hours >= 0)
        {
            HoursError.enabled = false;
            hoursPanel.SetActive(false);
        }
        else
        {
            PersistentManager.Instance.Hours = -1;
            HoursError.enabled = true;
            hoursPanel.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PersistentManager.Instance.Gender = "Male";
        csv = new CSVWriter();
        csv.FristLineCheck();
        lastLine = csv.LastLine();
        lastLine = lastLine.Replace(", T, ", ", S, ");
    }

    // Update is called once per frame
    void Update()
    {
        if (PersistentManager.Instance.Hours != -1)
        {
            NextButton.gameObject.SetActive(true);
        } else
        {
            NextButton.gameObject.SetActive(false);
        }
    }

    public void WriteLine() 
    {
        lastLine += ", " + PersistentManager.Instance.Gender + ", " + PersistentManager.Instance.Hours;
        csv.Writer(lastLine);

    }
}
