using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public List<int> listOfMazes;

    //private MazeBuilder obj = new MazeBuilder(); 

    public void QuitGame(){
    	Application.Quit();
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
        string condition = PersistentManager.Instance.condition;
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

        PersistentManager.Instance.listOfMazes = listOfMazes;

        
    }

    // TODO: Keep this code for now; in case something was overlooked on the refactor

    /*
    // Change scene and update PersistentManager
    public void ChangeScene(int scene)
    {
        // Update PersistentManager vars
        PersistentManager.Instance.previousScene = PersistentManager.Instance.CurrentScene;
        PersistentManager.Instance.CurrentScene = scene;

        // Load scene
        SceneManager.LoadScene(scene);
    }

    // Set timer and PersisistentManager vars for timeout tracking
    public void SetTimer()
    {
        // Check current time and add constraint given timeout onto it 
        System.DateTime now = System.DateTime.Now;
        double timeout = System.Convert.ToDouble(PersistentManager.Instance.timeOut);
        System.DateTime tmp = now.AddMinutes(timeout);

        // Update var for later checking
        PersistentManager.Instance.endTime = tmp;
    }

    // Change scene for Ego Only under end maze conditions
    public void EgoCheck(int currentIndex)
    {
        // Reset current attempts
        PersistentManager.Instance.currentAttempts = 0;

        // Check if in transition screen
        if (PersistentManager.Instance.CurrentScene == 4 || PersistentManager.Instance.CurrentScene == 7)
        {
            // At before experiment screen, set practice to false, dont increment maze in practice
            if (PersistentManager.Instance.isPractice)
            {
                // End of practice
                PersistentManager.Instance.isPractice = false;
            }
            else
            {
                // Move to next maze index 
                currentIndex++;
                PersistentManager.Instance.mazeArrayIndex = currentIndex;
            }

            // Set next maze to load
            PersistentManager.Instance.mazeArrayValue = listOfMazes[currentIndex];

            //Change to ego from transition
            ChangeScene(1);

            // Start timer for next maze
            SetTimer();

        }
        else if (PersistentManager.Instance.CurrentScene == 1)
        {
            //Change to before experiment transition instead if practice
            if (PersistentManager.Instance.isPractice)
            {
                //Change to transition practice scene
                ChangeScene(7);
            }
            else
            {
                //Change to transition sceen
                ChangeScene(4);
            }

            // Update colors
            GameObject.Find("Main Camera").GetComponent<MazeBuilder>().GenerateSectorColors();
        }
    }

    // Change scene for Allo Only under end maze conditions
    public void AlloCheck(int currentIndex)
    {
        // Reset current attempts
        PersistentManager.Instance.currentAttempts = 0;

        // Check if in transition screen
        if (PersistentManager.Instance.CurrentScene == 4 || PersistentManager.Instance.CurrentScene == 7)
        {
            // At before experiment screen, set practice to false, dont increment maze in practice
            if (PersistentManager.Instance.isPractice)
            {
                // End of practice
                PersistentManager.Instance.isPractice = false;
            }
            else
            {

                // Move to next maze index 
                currentIndex++;
                PersistentManager.Instance.mazeArrayIndex = currentIndex;
            }

            // Set next maze to load
            PersistentManager.Instance.mazeArrayValue = listOfMazes[currentIndex];

            //Change to allo from transition
            ChangeScene(2);

            // Start timer for next maze
            SetTimer();


        }
        else if (PersistentManager.Instance.CurrentScene == 2)
        {
            //Change to before experiment transition instead if practice
            if (PersistentManager.Instance.isPractice)
            {
                //Change to practice transition scene
                ChangeScene(7);
            }
            else
            {
                //Change to transition scene
                ChangeScene(4);
            }

            // Update colors
            GameObject.Find("Main Camera").GetComponent<MazeBuilder>().GenerateSectorColors();
        }
    }

    // Change scene for Egoa and Allo under end maze conditions
    public void EgoAlloCheck(int currentIndex)
    {
        // Reset current attempts
        PersistentManager.Instance.currentAttempts = 0;

        // Update colors
        if (PersistentManager.Instance.CurrentScene == 2)
        {
            GameObject.Find("Main Camera").GetComponent<MazeBuilder>().GenerateSectorColors();
        }

        // Practice mazes completed
        if (PersistentManager.Instance.CurrentScene == 2 && PersistentManager.Instance.isPractice)
        {
            //Change to practice transition scene
            ChangeScene(7);
        }
        // If in ego or allo scene then switch to transtition screen
        else if (PersistentManager.Instance.CurrentScene == 1 || PersistentManager.Instance.CurrentScene == 2)
        {

            //Change to transition scene
            ChangeScene(4);
        }
        // Check if in transition screen and then what maze to switch to
        else if (PersistentManager.Instance.CurrentScene == 4 || PersistentManager.Instance.CurrentScene == 7)
        {
            if (PersistentManager.Instance.previousScene == 1)
            {
                //Reload allo scene
                ChangeScene(2);
            }

            if (PersistentManager.Instance.previousScene == 2)
            {
                if (PersistentManager.Instance.isPractice)
                {
                    // End of practice
                    PersistentManager.Instance.isPractice = false;
                }
                else
                {
                    // Move to next maze index 
                    currentIndex++;
                    PersistentManager.Instance.mazeArrayIndex = currentIndex;
                }

                // Set next maze to load
                PersistentManager.Instance.mazeArrayValue = listOfMazes[currentIndex];

                //Change to ego scene
                ChangeScene(1);

                // Start timer for next maze
                SetTimer();
            }
        }

    }

    // Determine what scene to change to
    public void ChangeMaze()
    {
        // TODO: reset vars at end of scene loading calls
        // TODO: Can you change if strutcture to not have to check for maze type since persistnat CurrentScene
        // TODO: Change to completed maze scene before changing to next maze
        // TODO: Increment attempts on timeout and perfects
        // TODO: Current time continues through transition screens, use stopwatch to pause inbetween mazes
        // 0 main // 1 ego // 2 alo // 3 same maze // 4 completed maze // 5 survey // 6 timeout // 7 begin experiment
        //print("Change Maze");

        // Get current listOfMazes
        listOfMazes = PersistentManager.Instance.listOfMazes;

        // Boolean condiitions represented end maze status 
        bool attemptsReached = PersistentManager.Instance.currentAttempts == PersistentManager.Instance.numAttempts;
        bool timeoutReached = PersistentManager.Instance.timedOut;
        bool perfectsReached = PersistentManager.Instance.currentSuccessfulAttempts == PersistentManager.Instance.numSuccessfulAttempts;
        bool endReached = PersistentManager.Instance.atEnd;

        //print("Current attempts: " + PersistentManager.Instance.currentAttempts + " Max attempts: " + PersistentManager.Instance.numAttempts);
        //print("Change scene: Attempts: " + attemptsReached + " Timeout: " + timeoutReached + " Perfects: " + perfectsReached + " End: "  + endReached);

        // Go back to beginning of maze if no conditions are met.
        if ((endReached && (!attemptsReached && !timeoutReached && !perfectsReached)))
        {
            // Change from attempt transition menu to ego or allo
            if (PersistentManager.Instance.CurrentScene == 3 || PersistentManager.Instance.CurrentScene == 6)
            {
                // Check if ego or allo and change scene
                if (PersistentManager.Instance.previousScene == 1)
                {
                    //Change scene and update PersistentManager
                    ChangeScene(1);
                }

                if (PersistentManager.Instance.previousScene == 2)
                {
                    //Change scene and update PersistentManager
                    ChangeScene(2);
                }

                // Set end and timeout back to false
                PersistentManager.Instance.atEnd = false;
                PersistentManager.Instance.timedOut = false;

            }
            // Change to attempt transition screen
            else if (PersistentManager.Instance.CurrentScene == 1 || PersistentManager.Instance.CurrentScene == 2)
            {

                if (timeoutReached)
                {
                    //Change to transition scene
                    ChangeScene(6);
                }
                else
                {
                    //Change to transition scene 
                    ChangeScene(3);
                }


            }

        }


        // Check if any of the maze ending requirments are met
        if (attemptsReached || perfectsReached || timeoutReached || PersistentManager.Instance.CurrentScene == 4 || PersistentManager.Instance.CurrentScene == 7)
        {

            // Get current maze from list
            int currentIndex = PersistentManager.Instance.mazeArrayIndex;

            // All mazes completed
            if (currentIndex == (PersistentManager.Instance.mazeSize - 1))
            {
                // Change to end survey scene
                ChangeScene(5);
            }

            // Allo: Change scene properly based on transition screens and practice mode
            if (PersistentManager.Instance.alloCentric && !PersistentManager.Instance.egoCentric)
            {
                AlloCheck(currentIndex);
            }
            // Ego: Change scene properly based on transition screens and practice mode
            else if (PersistentManager.Instance.egoCentric && !PersistentManager.Instance.alloCentric)
            {
                EgoCheck(currentIndex);
            }
            // Ego and Allo: Change scene properly based on transition screens and practice mode
            else if (PersistentManager.Instance.egoCentric && PersistentManager.Instance.alloCentric)
            {
                EgoAlloCheck(currentIndex);
            }

        }


    }

    */
}
