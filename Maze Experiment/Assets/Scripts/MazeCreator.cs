using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MazeCreator : MonoBehaviour
{

    public GameObject addButton;
    public GameObject errorPanel;
    public TMP_Text errorMessage;
    public TMP_InputField fileNameInput;

    // Arrays which holds the values of what the users selects in the GUI. Gets written to a file on "Add"
    private static string[,] maze15Array = new string[15, 15] {
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"}
    };

    private static string[,] maze9Array = new string[9, 9] {
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"},
        {"1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N", "1N"}
    };

    public GameObject maze15;
    public GameObject maze9;

    // Called dynamically from MazeCreatorScreen > Options > FileName > FileNameField in the MainMenuScene
    public void FillFileNameInput(string filename)
    {
        Regex ne = new Regex(@"^\w+([\s-_]\w+)*$");
        Match match = ne.Match(filename);

        // Checks that the filename is allowed
        if (match.Success){
            // Set filename and turn off error notifications
            PersistentManager.Instance.newMazeFileName = filename;
            errorPanel.SetActive(false);
            errorMessage.enabled = false;
        } else
        {
            // Reset filename and turn on error notifications
            PersistentManager.Instance.newMazeFileName = "";
            errorPanel.SetActive(true);
            errorMessage.enabled = true;
        }
       
    }

    private void Update()
    {
        // Only shows add button when the filename is not blank
        if (PersistentManager.Instance.newMazeFileName == "")
        {
            addButton.SetActive(false);
        } else
        {
            addButton.SetActive(true);
        }
    }

    // Add button event handler
    public void AddButton()
    {
        // Writes the size 15 maze on add
        if (maze15.activeSelf == true)
        {
            FileWriter(maze15Array);
        }
        // Writes the size 9 maze on add
        else
        {
            FileWriter(maze9Array);
        }
    }

    // Switch current maze shown in GUI. Called from MazeSizeDropdown in the MazeCreatorScreen
    public void SwapMazeGUI(int value)
    {
        // Show 15 size maze
        if(value == 0)
        {
            maze15.SetActive(true);
            maze9.SetActive(false);
        }
        // Show 9 size maze
        else
        {
            maze15.SetActive(false);
            maze9.SetActive(true);
        }
    }

    // takes an index of a 15x15 maze and a value - all comma seperated - as a parameter 
    // (string ex. "1,13,0", "0,0,1") and changes to the specified value at that position.
    public void SetIndexValue(string index)
    {
        string[] splitParam = index.Split(',');

        int row = int.Parse(splitParam[0]);
        int col = int.Parse(splitParam[1]);
        int value = int.Parse(splitParam[2]);

        if (value == 0) // Wall
        {
            if(maze15.activeSelf == true)
            {
                maze15Array[row, col] = "1N";
            } else
            {
                maze9Array[row, col] = "1N";
            }
            
        }
        else if (value == 1) // Path
        {
            if (maze15.activeSelf == true)
            {
                maze15Array[row, col] = "0N";
            }
            else
            {
                maze9Array[row, col] = "0N";
            }
        }
        else if (value == 2) // Start
        {
            if (maze15.activeSelf == true)
            {
                maze15Array[row, col] = "0S";
            }
            else
            {
                maze9Array[row, col] = "0S";
            }
        }
        else // Finish
        {
            if (maze15.activeSelf == true)
            {
                maze15Array[row, col] = "0F";
            }
            else
            {
                maze9Array[row, col] = "0F";
            }
        }
        
    }

    // Write maze to file.
    public void FileWriter(string[,] mazeArray)
    {

        // Make intersections and add it to array.
        if (maze15.activeSelf == true)
        {
            // Add intersections before writing
            GetIntersections(maze15Array);
        } else
        {
            // Add intersections before writing
            GetIntersections(maze9Array);
        }

        string[] mazeToWrite = new string[mazeArray.GetLength(0)];

        //iterate mazeArray and build one line strings
        for (int row = 0; row < mazeArray.GetLength(0); row++)
        {
            string str = "";
            string delim = "";
            for (int col = 0; col < mazeArray.GetLength(1); col++)
            {
                str += delim + mazeArray[row, col];
                delim = " ";
            }
            mazeToWrite[row] = str;
        }

        
        string FilePath = Directory.GetCurrentDirectory() + "/Assets/Resources/Mazes/";

        // Get next maze name for given condition
        int FileCount = Directory.GetFiles(FilePath).Length / 2;
        string MazeName;

        // Filename is specified
        if(PersistentManager.Instance.newMazeFileName == "")
        {
            MazeName = "Maze-" + (FileCount) + ".txt";
        } else
        {
            MazeName = PersistentManager.Instance.newMazeFileName + ".txt";
        }
        

        FilePath += MazeName;

        // Write mazeArray to file
        System.IO.File.WriteAllLines(FilePath, mazeToWrite);

    }


    // Add intersections after maze creation
    private void GetIntersections(string[,] mazeArray)
    {

        for (int row = 1; row < mazeArray.GetLength(0) - 1; row++)
        {

            for (int col = 1; col < mazeArray.GetLength(1) - 1; col++)
            {

                int intercount = 0;

                //check to the right
                if ((mazeArray[row, col][0] == '0') && (mazeArray[row, col + 1][0] == '0'))
                {
                    //Make sure it isnt a hallway
                    if ((mazeArray[row, col][0] == '0') && (mazeArray[row, col - 1][0] != '0'))
                    {
                        intercount++;
                    }
                }
                //check to the left
                if ((mazeArray[row, col][0] == '0') && (mazeArray[row, col - 1][0] == '0'))
                {
                    //Make sure it isnt a hallway
                    if ((mazeArray[row, col][0] == '0') && (mazeArray[row, col + 1][0] != '0'))
                    {
                        intercount++;
                    }
                }
                //check up
                if ((mazeArray[row, col][0] == '0') && (mazeArray[row - 1, col][0] == '0'))
                {
                    //Make sure it isnt a hallway
                    if ((mazeArray[row, col][0] == '0') && (mazeArray[row + 1, col][0] != '0'))
                    {
                        intercount++;
                    }
                }
                //check down
                if ((mazeArray[row, col][0] == '0') && (mazeArray[row + 1, col][0] == '0'))
                {
                    //Make sure it isnt a hallway
                    if ((mazeArray[row, col][0] == '0') && (mazeArray[row - 1, col][0] != '0'))
                    {
                        intercount++;
                    }
                }
                //check 4-way
                //First check left and right
                if ((mazeArray[row, col][0] == '0') && (mazeArray[row, col + 1][0] == '0') && (mazeArray[row, col][0] == '0') && (mazeArray[row, col - 1][0] == '0'))
                {
                    //Now check up and down
                    if ((mazeArray[row, col][0] == '0') && (mazeArray[row - 1, col][0] == '0') && (mazeArray[row, col][0] == '0') && (mazeArray[row + 1, col][0] == '0'))
                    {
                        intercount++;
                    }

                }


                if (intercount > 0)
                {
                    mazeArray[row, col] = "0I";
                }
            }
        }

    }


}
