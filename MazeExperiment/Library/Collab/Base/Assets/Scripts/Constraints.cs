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

    private string Participant = "";
    private bool Egocentric = false;
    private bool Allocentric = false;
    private string Condition = "A";
    private string TimeOut = "";
    private string NumAttempts = "";
    private string NumSuccessfulAttempts = "";

    public void FillParticipantInput(string number)
    {
        Participant = number;
        PlayerPrefs.SetString("Participant", number);
    }

    public void FillEgocentricInput(bool ego)
    {
        if (ego)
        {
            Egocentric = true;
            PlayerPrefs.SetString("Egocentric", "true");
        }
        else
        {
            Egocentric = false;
            PlayerPrefs.SetString("Egocentric", "false");
        }
    }

    public void FillAllocentricInput(bool allo)
    {
        if (allo)
        {
            Allocentric = true;
            PlayerPrefs.SetString("Allocentric", "true");
        }
        else
        {
            Allocentric = false;
            PlayerPrefs.SetString("Allocentric", "false");
        }
    }

    public void FillConditionInput(int con)
    {
        if(con == 0)
        {
            Condition = "A";
            PlayerPrefs.SetString("Condition", "A");
        } else if (con == 1)
        {
            Condition = "B";
            PlayerPrefs.SetString("Condition", "B");
        } else
        {
            Condition = "C";
            PlayerPrefs.SetString("Condition", "C");
        }

    }

    public void FillTimeOutInput(string time)
    {
        TimeOut = time;
        PlayerPrefs.SetString("TimeOut", time);
    }

    public void FillNumAttemptsInput(string attempt)
    {
        NumAttempts = attempt;
        PlayerPrefs.SetString("NumAttempts", attempt);
    }

    public void FillNumSuccessfulAttemptsInput(string attempt)
    {
        NumSuccessfulAttempts = attempt;
        PlayerPrefs.SetString("NumSuccessfulAttempts", attempt);
    }

    public void ClearInput()
    {
        Participant = "";
        Egocentric = false;
        Allocentric = false;
        Condition = "A";
        TimeOut = "";
        NumAttempts = "";
        NumSuccessfulAttempts = "";
        ParticipantField.text = "";
        EgocentricToggle.isOn = false;
        AllocentricToggle.isOn = false;
        ConditionDropdown.value = 0;
        TimeOutField.text = "";
        NumAttemptsField.text = "";
        NumSuccessfulAttemptsField.text = "";
    }

    private void Update()
    {

        if (!Participant.Equals("") && (Egocentric == true || Allocentric == true) && !TimeOut.Equals("") && !NumAttempts.Equals("") && !NumSuccessfulAttempts.Equals(""))
        {
            NextButton.gameObject.SetActive(true);
        } else
        {
            NextButton.gameObject.SetActive(false);
        }
    }



}
