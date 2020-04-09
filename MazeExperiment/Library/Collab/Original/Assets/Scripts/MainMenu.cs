using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public List<int> listOfMazes;

    public void QuitGame(){
    	Application.Quit();
    	Debug.Log("QUIT!");
    }

    public void Start()
    {
        CreateMazeList();
    }

    // Randomize a list of 1-15 and order them based on condition
    public void CreateMazeList()
    {

        // Create list of ints and randomize them
        List<int> list15 = new List<int>(new int[] { 1, 2, 3, 4, 5 });
        List<int> list610 = new List<int>(new int[] { 6, 7, 8, 9, 10 });
        List<int> list1115 = new List<int>(new int[] { 11, 12, 13, 14, 15 });
        list15 = list15.OrderBy(x => Random.value).ToList();
        list610 = list610.OrderBy(x => Random.value).ToList();
        list1115 = list1115.OrderBy(x => Random.value).ToList();


        // Create final list of mazes based on condition
        string condition = PlayerPrefs.GetString("Condition");
        if (condition == "A") {
            listOfMazes.AddRange(list15);
            listOfMazes.AddRange(list610);
            listOfMazes.AddRange(list1115);
        }   
        if (condition == "B")
        {
            listOfMazes.AddRange(list1115);
            listOfMazes.AddRange(list15);
            listOfMazes.AddRange(list610);
            
        }
        if (condition == "C")
        {
            listOfMazes.AddRange(list610);
            listOfMazes.AddRange(list1115);
            listOfMazes.AddRange(list15);

        }


    }

    // Check player prefs to determine what scene to change to
    public void ChangeMaze()
    {
        // TODO: Assume always starting on ego, so if both, change to allo
        // 0 main // 1 ego // 2 alo 

        // Boolean condiitions represented end maze status 
        bool attemptsReached = PlayerPrefs.GetString("currentAttempts") == PlayerPrefs.GetString("NumAttempts");
        bool timeoutReached = PlayerPrefs.GetString("timeOut") == "Yes";
        bool perfectsReached = PlayerPrefs.GetString("currentPerfects") == PlayerPrefs.GetString("NumSuccessfulAttempts");
        bool endReached = PlayerPrefs.GetString("endReached") == "Yes";

        // Go back to beginning of maze if no other coniditions are met 
        if (endReached && (!attemptsReached  && !timeoutReached && !perfectsReached) )
        {
            SceneManager.LoadScene(PersistentManager.Instance.CurrentScene);
        }

        // Check if any of the maze ending requirments are met
        if (attemptsReached || timeoutReached || perfectsReached)
        {
            // Get current maze from list
            int currentIndex = PlayerPrefs.GetInt("currentMazeIndex");

            // Check for maze type then change se
            if (PlayerPrefs.GetString("setMazeType") == "Allocentric")
            {

                // Move to next maze index 
                currentIndex++;
                PlayerPrefs.SetInt("currentMazeIndex", currentIndex);

                // Set next maze to load
                PlayerPrefs.SetInt("CurrentMaze", listOfMazes[currentIndex]);

                // Reload scene
                SceneManager.LoadScene(2);


            }
            else if (PlayerPrefs.GetString("setMazeType") == "Egocentric")
            {

                // Move to next maze index 
                currentIndex++;
                PlayerPrefs.SetInt("currentMazeIndex", currentIndex);

                // Set next maze to load
                PlayerPrefs.SetInt("CurrentMaze", listOfMazes[currentIndex]);

                // Reload scene
                SceneManager.LoadScene(1);

            }
            else if (PlayerPrefs.GetString("setMazeType") == "Both")
            {
                // If on ego switch to allo, else move to next maze
                if (PlayerPrefs.GetString("currentMazeType") == "Egocentric")
                {

                    // Reload scene
                    SceneManager.LoadScene(2);

                }
                else
                {
                    // Move to next maze index 
                    currentIndex++;
                    PlayerPrefs.SetInt("currentMazeIndex", currentIndex);

                    // Set next maze to load
                    PlayerPrefs.SetInt("CurrentMaze", listOfMazes[currentIndex]);

                    // Reload scene
                    SceneManager.LoadScene(1);
                }
            }
        }


    }
}
