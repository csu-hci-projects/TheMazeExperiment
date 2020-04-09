using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Constraints : MonoBehaviour
{
    public Button NextButton;
    public TMP_InputField ParticipantField;
    public Toggle EgocentricToggle;
    public Toggle AllocentricToggle;
    public TMP_Dropdown ConditionDropdown;
    public TMP_InputField TimeOutField;
    public TMP_InputField NumAttemptsField;
    public TMP_InputField NumSuccessfulAttemptsField;
    public Toggle AudioToggle;

    //Errors
    public TMP_Text ParticipantError;
    public TMP_Text TimeOutError;
    public TMP_Text NumAttemptsError;
    public TMP_Text NumSuccessError;
    public GameObject partPanel;
    public GameObject timePanel;
    public GameObject attemptsPanel;
    public GameObject successPanel;


    public void FillParticipantInput(string number)
    {

        bool canConvert = int.TryParse(number, out PersistentManager.Instance.participantNumber);

        if (canConvert && PersistentManager.Instance.participantNumber >= 0)
        {
            ParticipantError.enabled = false;
            partPanel.SetActive(false);
        } else
        {
            PersistentManager.Instance.participantNumber = -1;
            ParticipantError.enabled = true;
            partPanel.SetActive(true);
        }
    }

    public void FillEgocentricInput(bool ego)
    {
        PersistentManager.Instance.egoCentric = ego;
    }

    public void FillAllocentricInput(bool allo)
    {
        PersistentManager.Instance.alloCentric = allo;
    }

    public void FillConditionInput(int con)
    {
        print(con);
        if(con == 0)
        {
            PersistentManager.Instance.condition = "A";

        } else if (con == 1)
        {
            PersistentManager.Instance.condition = "B";

        } else
        {
            PersistentManager.Instance.condition = "C";
        }

    }

    public void FillTimeOutInput(string time)
    {
        bool canConvert = double.TryParse(time, out PersistentManager.Instance.timeOut);

        if (canConvert && PersistentManager.Instance.timeOut > 0.0)
        {
            TimeOutError.enabled = false;
            timePanel.SetActive(false);
        }
        else
        {
            PersistentManager.Instance.timeOut = -1;
            TimeOutError.enabled = true;
            timePanel.SetActive(true);
        }
    }

    public void FillNumAttemptsInput(string attempt)
    {
        bool canConvert = int.TryParse(attempt, out PersistentManager.Instance.numAttempts);

        if (canConvert && PersistentManager.Instance.numAttempts > 0)
        {
            NumAttemptsError.enabled = false;
            attemptsPanel.SetActive(false);
        }
        else
        {
            PersistentManager.Instance.numAttempts = -1;
            NumAttemptsError.enabled = true;
            attemptsPanel.SetActive(true);

        }
    }

    public void FillNumSuccessfulAttemptsInput(string attempt)
    {
        bool canConvert = int.TryParse(attempt, out PersistentManager.Instance.numSuccessfulAttempts);

        if (canConvert && PersistentManager.Instance.numSuccessfulAttempts > 0)
        {
            NumSuccessError.enabled = false;
            successPanel.SetActive(false);
        }
        else
        {
            PersistentManager.Instance.numSuccessfulAttempts = -1;
            NumSuccessError.enabled = true;
            successPanel.SetActive(true);
        }
    }

    public void FillAudioInput(bool audio)
    {
        PersistentManager.Instance.disableAudio = audio;
    }

    public void ClearInput()
    {

        //Clear GUI
        ParticipantField.text = "";
        EgocentricToggle.isOn = false;
        AllocentricToggle.isOn = false;
        ConditionDropdown.value = 0;
        TimeOutField.text = "";
        NumAttemptsField.text = "";
        NumSuccessfulAttemptsField.text = "";
        EgocentricToggle.isOn = false;

    }

    private void Update()
    {
        //Check if all fields have been filled
        bool participantSet = PersistentManager.Instance.participantNumber != -1;
        bool egoSet = PersistentManager.Instance.egoCentric;
        bool alloSet = PersistentManager.Instance.alloCentric;
        bool timeSet = PersistentManager.Instance.timeOut != -1;
        bool attemptsSet = PersistentManager.Instance.numAttempts != -1;
        bool perfectSet = PersistentManager.Instance.numSuccessfulAttempts != -1;

        //print(participantSet + " " + egoSet + " " + alloSet + " " + timeSet + " " + attemptsSet + " " + perfectSet);

        if (participantSet && (egoSet || alloSet) && timeSet && attemptsSet && perfectSet)
        {
            //Show next button
            NextButton.gameObject.SetActive(true);
        }
        else
        {
            NextButton.gameObject.SetActive(false);
        }
    }



}
