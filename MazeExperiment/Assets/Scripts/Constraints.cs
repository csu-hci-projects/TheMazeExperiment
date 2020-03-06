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

    public void Start()
    {
        //Initialize constraints
        PlayerPrefs.SetString("Participant", "");
        PlayerPrefs.SetString("Egocentric", "false");
        PlayerPrefs.SetString("Allocentric", "false");
        PlayerPrefs.SetString("Condition", "A");
        PlayerPrefs.SetString("TimeOut", "");
        PlayerPrefs.SetString("NumAttempts", "");
        PlayerPrefs.SetString("NumSuccessfulAttempts", "");
    }

    public void FillParticipantInput(string number)
    {
        PlayerPrefs.SetString("Participant", number);
    }

    public void FillEgocentricInput(bool ego)
    {

        if (ego)
        {
            PlayerPrefs.SetString("Egocentric", "true");
        }
        else
        {
            PlayerPrefs.SetString("Egocentric", "false");
        }
    }

    public void FillAllocentricInput(bool allo)
    {
        if (allo)
        {
            PlayerPrefs.SetString("Allocentric", "true");
        }
        else
        {
            PlayerPrefs.SetString("Allocentric", "false");
        }
    }

    public void FillConditionInput(int con)
    {
        if(con == 0)
        {
            PlayerPrefs.SetString("Condition", "A");
        } else if (con == 1)
        {
            PlayerPrefs.SetString("Condition", "B");
        } else
        {
            PlayerPrefs.SetString("Condition", "C");
        }

    }

    public void FillTimeOutInput(string time)
    {
        PlayerPrefs.SetString("TimeOut", time);
    }

    public void FillNumAttemptsInput(string attempt)
    {
        PlayerPrefs.SetString("NumAttempts", attempt);
    }

    public void FillNumSuccessfulAttemptsInput(string attempt)
    {
        PlayerPrefs.SetString("NumSuccessfulAttempts", attempt);
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

        //Clear saved input
        PlayerPrefs.SetString("Participant", "");
        PlayerPrefs.SetString("Egocentric", "false");
        PlayerPrefs.SetString("Allocentric", "false");
        PlayerPrefs.SetString("Condition", "A");
        PlayerPrefs.SetString("TimeOut", "");
        PlayerPrefs.SetString("NumAttempts", "");
        PlayerPrefs.SetString("NumSuccessfulAttempts", "");
    }

    private void Update()
    {
        //Check if all fields have been filled
        if (!PlayerPrefs.GetString("Participant").Equals("") && (PlayerPrefs.GetString("Egocentric") == "true" || PlayerPrefs.GetString("Allocentric") == "true")
            && !PlayerPrefs.GetString("TimeOut").Equals("") && !PlayerPrefs.GetString("NumAttempts").Equals("") && !PlayerPrefs.GetString("NumSuccessfulAttempts").Equals(""))
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
