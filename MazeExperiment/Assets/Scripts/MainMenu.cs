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

}
