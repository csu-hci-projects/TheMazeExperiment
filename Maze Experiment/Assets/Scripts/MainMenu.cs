using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField part;
    public GameObject TitleScreen;
    public GameObject ConstraintScreen;
    public GameObject ConsentScreen;
    public List<int> listOfMazes;

    // Called when a quit button is pressed
    public void QuitGame(){
    	Application.Quit();
    }

    // Called when "Begin Experiment" button is pressed. Checks to switch
    // to constraint screen or consent screen based on participant number
    public void ChangeMenuPage()
    {
        // Participant number is greater than 99 (Set defaults and go to Consent screen)
        if (PersistentManager.Instance.participantNumber >= 100)
        {
            //Set default constraints
            if (PersistentManager.Instance.participantNumber % 6 == 5)
            {
                PersistentManager.Instance.egoCentric = true;
                PersistentManager.Instance.timeOut = 2;
                PersistentManager.Instance.condition = "A";
                
            }
            else if (PersistentManager.Instance.participantNumber % 6 == 0)
            {
                PersistentManager.Instance.alloCentric = true;
                PersistentManager.Instance.timeOut = 1;
                PersistentManager.Instance.condition = "A";

            }
            else if (PersistentManager.Instance.participantNumber % 6 == 1)
            {
                PersistentManager.Instance.egoCentric = true;
                PersistentManager.Instance.timeOut = 2;
                PersistentManager.Instance.condition = "B";

            }
            else if (PersistentManager.Instance.participantNumber % 6 == 2)
            {
                PersistentManager.Instance.alloCentric = true;
                PersistentManager.Instance.timeOut = 1;
                PersistentManager.Instance.condition = "B";

            }
            else if (PersistentManager.Instance.participantNumber % 6 == 3)
            {
                PersistentManager.Instance.egoCentric = true;
                PersistentManager.Instance.timeOut = 2;
                PersistentManager.Instance.condition = "C";

            }
            else if (PersistentManager.Instance.participantNumber % 6 == 4)
            {
                PersistentManager.Instance.alloCentric = true;
                PersistentManager.Instance.timeOut = 1;
                PersistentManager.Instance.condition = "C";

            }

            PersistentManager.Instance.numAttempts = 20;
            PersistentManager.Instance.numSuccessfulAttempts = 20;

            TitleScreen.SetActive(false);
            ConsentScreen.SetActive(true);
        }
        // Participant number is less than 100 (Show constraints)
        else
        {
            // Participant ID was not in the specified range
            TitleScreen.SetActive(false);
            ConstraintScreen.SetActive(true);
        }

        // Create maze list
        CreateMazeList();
    }

    // Called from Persistent Manager when the application starts. Attempts to read 
    // and set participant number from participantID.txt
    public void InitializePID()
    {
        string filePath = Directory.GetCurrentDirectory() + "/participantID.txt";
        
        // Check if file exists
        if (File.Exists(filePath))
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        bool canConvert = int.TryParse(line, out PersistentManager.Instance.participantNumber);

                        if (canConvert && PersistentManager.Instance.participantNumber >= 0)
                        {
                            
                        }
                        else
                        {
                            PersistentManager.Instance.participantNumber = -1;
                        }

                    }
                }
            }
            catch (System.Exception e)
            {

            }
        }
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

}
