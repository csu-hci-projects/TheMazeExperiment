using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SurveyInput : MonoBehaviour
{
    // Input fields
    public Button NextButton;
    public Toggle MaleToggle;
    public Toggle FemaleToggle;
    public Toggle OtherToggle;
    public Toggle PreferNotToggle;
    public TMP_InputField HoursInput;

    // Error game objects
    public TMP_Text HoursError;
    public GameObject hoursPanel;

    private CSVWriter csv;
    private string lastLine;

    // Called by SurveyScene > Survey > SurveyScreen > Gender ? Boxes > Male
    public void SetMaleToggle(bool value)
    {
        if (value)
        {
            PersistentManager.Instance.Gender = "Male";
        }

    }

    // Called by SurveyScene > Survey > SurveyScreen > Gender ? Boxes > Female
    public void SetFemaleToggle(bool value)
    {
        if (value)
        {
            PersistentManager.Instance.Gender = "Female";
        }
    }

    // Called by SurveyScene > Survey > SurveyScreen > Gender ? Boxes > Other
    public void SetOtherToggle(bool value)
    {
        if (value)
        {
            PersistentManager.Instance.Gender = "Other";
        }
    }

    // Called by SurveyScene > Survey > SurveyScreen > Gender ? Boxes > Prefer not to say
    public void SetPreferNotToggle(bool value)
    {
        if (value)
        {
            PersistentManager.Instance.Gender = "Prefer not to say";
        }
    }

    // Called by SurveyScene > Survey > SurveyScreen > HoursSpent > HoursField
    public void SetHoursInput(string value)
    {
        // Tries to convert user input for set hours 
        bool canConvert = int.TryParse(value, out PersistentManager.Instance.Hours);

        if (canConvert && PersistentManager.Instance.Hours >= 0)
        {
            // Disable error messages
            HoursError.enabled = false;
            hoursPanel.SetActive(false);
        }
        else
        {
            // Enable error messages and reset hours
            PersistentManager.Instance.Hours = -1;
            HoursError.enabled = true;
            hoursPanel.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize gender and CSV writer
        PersistentManager.Instance.Gender = "Male";
        csv = new CSVWriter();
        csv.FristLineCheck();
        lastLine = csv.LastLine();
        lastLine = lastLine.Replace(", T, ", ", S, ");
    }

    // Update is called once per frame
    void Update()
    {
        // Only show next when a valid number is entered into the HoursField
        if (PersistentManager.Instance.Hours != -1)
        {
            NextButton.gameObject.SetActive(true);
        }
        else
        {
            NextButton.gameObject.SetActive(false);
        }
    }

    // Called when next is pressed
    public void WriteLine()
    {
        lastLine += ", " + PersistentManager.Instance.Gender + ", " + PersistentManager.Instance.Hours;
        csv.Writer(lastLine);

    }
}
