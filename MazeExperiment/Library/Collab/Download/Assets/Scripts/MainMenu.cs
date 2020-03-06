using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string Current = "Main Menu";
    public void ChangeScene(){
        if(PlayerPrefs.GetString("Egocentric") == "true")
        {
            SceneManager.LoadScene(1);
        } else if (PlayerPrefs.GetString("Allocentric") == "true")
        {
            SceneManager.LoadScene(2);
        }
    }

    public void QuitGame(){
    	Application.Quit();
    	Debug.Log("QUIT!");
    }
}
