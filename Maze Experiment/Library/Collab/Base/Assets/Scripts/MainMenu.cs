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

    //private MazeBuilder obj = new MazeBuilder(); 

    public void QuitGame(){
    	Application.Quit();
    }

    public void ChangeMenuPage()
    {
        if (PersistentManager.Instance.participantNumber >= 100)
        {
            //Set default constraints
            // TDOD: Modulo ParticipantID and set different constraints based on that.
            PersistentManager.Instance.alloCentric = true;
            PersistentManager.Instance.timeOut = 5;
            PersistentManager.Instance.numAttempts = 3;
            PersistentManager.Instance.numSuccessfulAttempts = 1;

            TitleScreen.SetActive(false);
            ConsentScreen.SetActive(true);
        }
        else
        {
            // Participant ID was not in the specified range
            TitleScreen.SetActive(false);
            ConstraintScreen.SetActive(true);
        }

        // Create maze list
        CreateMazeList();
    }

    public void InitializePID()
    {
        string filePath = Directory.GetCurrentDirectory() + "/participantID.txt";
        Debug.LogError(filePath);
        if (File.Exists(filePath))
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
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
