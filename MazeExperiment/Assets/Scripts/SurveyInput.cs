using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SurveyInput : MonoBehaviour
{
    public string Hours = "";

    public Button NextButton;
    public Toggle MaleToggle;
    public Toggle FemaleToggle;
    public Toggle OtherToggle;
    public Toggle PreferNotToggle;
    public TMP_InputField HoursInput;

    public void SetMaleToggle(bool value)
    {
        if (value)
        {
            PlayerPrefs.SetString("Gender", "Male");
        }
        
    }

    public void SetFemaleToggle(bool value)
    {
        if (value)
        {
            PlayerPrefs.SetString("Gender", "Female");
        }
    }

    public void SetOtherToggle(bool value)
    {
        if (value)
        {
            PlayerPrefs.SetString("Gender", "Other");
        }
    }

    public void SetPreferNotToggle(bool value)
    {
        if (value)
        {
            PlayerPrefs.SetString("Gender", "Prefer not to say");
        }
    }

    public void SetHoursInput(string value)
    {
        Hours = value;
        PlayerPrefs.SetString("Video Game Hours", value);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("Gender", "Male");
    }

    // Update is called once per frame
    void Update()
    {
        if (Hours != "")
        {
            NextButton.gameObject.SetActive(true);
        } else
        {
            NextButton.gameObject.SetActive(false);
        }
    }
}
