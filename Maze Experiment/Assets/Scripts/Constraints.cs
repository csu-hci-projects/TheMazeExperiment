using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Constraints : MonoBehaviour
{
    // Input fields
    public Button NextButton;
    public TMP_InputField ParticipantField;
    public Toggle EgocentricToggle;
    public Toggle AllocentricToggle;
    public TMP_Dropdown ConditionDropdown;
    public TMP_InputField TimeOutField;
    public TMP_InputField NumAttemptsField;
    public TMP_InputField NumSuccessfulAttemptsField;
    public Toggle AudioToggle;

    // Error notifications
    public TMP_Text ParticipantError;
    public TMP_Text TimeOutError;
    public TMP_Text NumAttemptsError;
    public TMP_Text NumSuccessError;
    public GameObject partPanel;
    public GameObject timePanel;
    public GameObject attemptsPanel;
    public GameObject successPanel;

    // Called dynamically from the ConstraintSection > Participant > ParticipantField gameObject in the ConstraintsScreen
    public void FillParticipantInput(string number)
    {
        // Tries to convert user input to an integer.
        bool canConvert = int.TryParse(number, out PersistentManager.Instance.participantNumber);

        if (canConvert && PersistentManager.Instance.participantNumber >= 0)
        {
            // Turn off error messages if success
            ParticipantError.enabled = false;
            partPanel.SetActive(false);
        } else
        {
            // Show error messages if fail
            PersistentManager.Instance.participantNumber = -1;
            ParticipantError.enabled = true;
            partPanel.SetActive(true);
        }
    }

    // Called dynamically from the ConstraintSection > MazeType > Egocentric gameObject in the ConstraintsScreen
    public void FillEgocentricInput(bool ego)
    {
        // Sets egocentric in Persistent Manager
        PersistentManager.Instance.egoCentric = ego;
    }

    // Called dynamically from the ConstraintSection > MazeType > Allocentric gameObject in the ConstraintsScreen
    public void FillAllocentricInput(bool allo)
    {
        // Sets allocentric in Persistent Manager
        PersistentManager.Instance.alloCentric = allo;
    }

    // Called dynamically from the ConstraintSection > Condition > ConditionDropdown gameObject in the ConstraintsScreen
    public void FillConditionInput(int con)
    {
        // Sets condition in Persistent Manager
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

    // Called dynamically from the ConstraintSection > TimeOut > TimeOutField gameObject in the ConstraintsScreen
    public void FillTimeOutInput(string time)
    {
        // Tries to convert user input to a double.
        bool canConvert = double.TryParse(time, out PersistentManager.Instance.timeOut);

        if (canConvert && PersistentManager.Instance.timeOut > 0.0)
        {
            // Turn off error messages if success
            TimeOutError.enabled = false;
            timePanel.SetActive(false);
        }
        else
        {
            // Show error messages if fail
            PersistentManager.Instance.timeOut = -1;
            TimeOutError.enabled = true;
            timePanel.SetActive(true);
        }
    }

    // Called dynamically from the ConstraintSection > NumberOfAttempts > AttemptsField gameObject in the ConstraintsScreen
    public void FillNumAttemptsInput(string attempt)
    {
        // Tries to convert user input to an integer.
        bool canConvert = int.TryParse(attempt, out PersistentManager.Instance.numAttempts);

        if (canConvert && PersistentManager.Instance.numAttempts > 0)
        {
            // Turn off error messages if success
            NumAttemptsError.enabled = false;
            attemptsPanel.SetActive(false);
        }
        else
        {
            // Show error messages if fail
            PersistentManager.Instance.numAttempts = -1;
            NumAttemptsError.enabled = true;
            attemptsPanel.SetActive(true);

        }
    }

    // Called dynamically from the ConstraintSection > NumberOfSuccessfulAttempts > SuccessfulAttemptsField gameObject in the ConstraintsScreen
    public void FillNumSuccessfulAttemptsInput(string attempt)
    {
        // Tries to convert user input to an integer.
        bool canConvert = int.TryParse(attempt, out PersistentManager.Instance.numSuccessfulAttempts);

        if (canConvert && PersistentManager.Instance.numSuccessfulAttempts > 0)
        {
            // Turn off error messages if success
            NumSuccessError.enabled = false;
            successPanel.SetActive(false);
        }
        else
        {
            // Show error messages if fail
            PersistentManager.Instance.numSuccessfulAttempts = -1;
            NumSuccessError.enabled = true;
            successPanel.SetActive(true);
        }
    }

    // Called dynamically from the ConstraintSection > Audio > DisableCheckbox gameObject in the ConstraintsScreen
    public void FillAudioInput(bool audio)
    {
        // Sets disableAudio in the persistentManager
        PersistentManager.Instance.disableAudio = audio;
    }

    // Called when the back button is pressed in the constraints screen.
    public void ClearInput()
    {
        //Clear GUI input
        ParticipantField.text = "";
        EgocentricToggle.isOn = false;
        AllocentricToggle.isOn = false;
        ConditionDropdown.value = 0;
        TimeOutField.text = "";
        NumAttemptsField.text = "";
        NumSuccessfulAttemptsField.text = "";
        EgocentricToggle.isOn = false;

    }

    // Constantly checks that all fields have been filled for the next button to appear.
    private void Update()
    {
        //Check if all fields have been filled
        bool participantSet = PersistentManager.Instance.participantNumber != -1;
        bool egoSet = PersistentManager.Instance.egoCentric;
        bool alloSet = PersistentManager.Instance.alloCentric;
        bool timeSet = PersistentManager.Instance.timeOut != -1;
        bool attemptsSet = PersistentManager.Instance.numAttempts != -1;
        bool perfectSet = PersistentManager.Instance.numSuccessfulAttempts != -1;

        if (participantSet && (egoSet || alloSet) && timeSet && attemptsSet && perfectSet)
        {
            // Show next button
            NextButton.gameObject.SetActive(true);
        }
        else
        {
            // Turn off next button
            NextButton.gameObject.SetActive(false);
        }
    }



}
