using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class to switch scenes and update state in persistent manager
public class SceneManagerScript : MonoBehaviour
{
    public string Current;

    public void Start()
    {
        Current = PersistentManager.Instance.CurrentScene;
    }

    public void SwitchScene()
	{
        // Check current scene from state instance
        if (Current == "Menu")
        {
            if (PlayerPrefs.GetString("Egocentric") == "false" && PlayerPrefs.GetString("Allocentric") == "true")
            {
                SceneManager.LoadScene(2);
                PersistentManager.Instance.CurrentScene = "Allo";
            } else
            {
                SceneManager.LoadScene(1);
                PersistentManager.Instance.CurrentScene = "Ego";
            }

        } else if (Current == "Ego")
        {
            if (PlayerPrefs.GetString("Egocentric") == "true" && PlayerPrefs.GetString("Allocentric") == "true")
            {
                SceneManager.LoadScene(2);
                PersistentManager.Instance.CurrentScene = "Allo";
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        } else if (Current == "Allo")
        {
            if (PlayerPrefs.GetString("Egocentric") == "true" && PlayerPrefs.GetString("Allocentric") == "true")
            {
                SceneManager.LoadScene(1);
                PersistentManager.Instance.CurrentScene = "Ego";
            }
            else
            {
                SceneManager.LoadScene(2);
            }
        }


    }

}
