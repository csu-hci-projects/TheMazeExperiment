using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeTile : MonoBehaviour
{
    public int State = 0;
    public string Index = "0,0";
    public MazeCreator referenceScript;

    private Color Orange = new Color32(255, 176, 0, 255);


    public void OnClick()
    {
        //increment button state
        State++;

        //wrap back to first state
        if (State == 4){State = 0;}

        //get button colors
        var colors = GetComponent<Button>().colors;

        if (State == 0) // Wall
        {
            colors.normalColor = Orange;
            colors.highlightedColor = Orange;
            colors.pressedColor = Orange;
            colors.selectedColor = Orange;
            GetComponent<Button>().GetComponentInChildren<Text>().text = "";
            referenceScript.SetIndexValue(Index+","+State);
        } else if (State == 1) // Path
        {
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.white;
            colors.pressedColor = Color.white;
            colors.selectedColor = Color.white;
            GetComponent<Button>().GetComponentInChildren<Text>().text = "";
            referenceScript.SetIndexValue(Index + "," + State);
        } else if (State == 2) // Start
        {
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.white;
            colors.pressedColor = Color.white;
            colors.selectedColor = Color.white;
            GetComponent<Button>().GetComponentInChildren<Text>().text = "S";
            referenceScript.SetIndexValue(Index + "," + State);
        } else if(State == 3) // End
        {
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.white;
            colors.pressedColor = Color.white;
            colors.selectedColor = Color.white;
            GetComponent<Button>().GetComponentInChildren<Text>().text = "F";
            referenceScript.SetIndexValue(Index + "," + State);
        } else
        {}

        //set button colors
        GetComponent<Button>().colors = colors;

    }
}
