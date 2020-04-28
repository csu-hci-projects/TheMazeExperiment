using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MazeCreator : MonoBehaviour
{

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

    //Add button event handler
    public void AddButton()
    {
        if (maze15.activeSelf == true)
        {
            FileWriter(maze15Array);
        }
        else
        {
            FileWriter(maze9Array);
        }
    }

    //Switch current maze shown in GUI
    public void SwapMazeGUI(int value)
    {
        if(value == 0)
        {
            maze15.SetActive(true);
            maze9.SetActive(false);
        } else
        {
            maze15.SetActive(false);
            maze9.SetActive(true);
        }
    }

    //takes an index of a 15x15 maze and a value - all comma seperated - as a parameter (string ex. "1,13,0", "0,0,1") and changes the value at that position.
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

        if (maze15.activeSelf == true)
        {
            PrintMaze(maze15Array);
        } else
        {
            PrintMaze(maze9Array);
        }
        

    }

    //write to file
    public void FileWriter(string[,] mazeArray)
    {

        // Make intersections and add it to array.
        if (maze15.activeSelf == true)
        {
            GetIntersections(maze15Array);
        } else
        {
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

        
        string FilePath = Directory.GetCurrentDirectory() + "/Assets/Mazes/";

        // Get next maze name for given condition
        int FileCount = Directory.GetFiles(FilePath).Length / 2;
        string MazeName = "Maze-" + (FileCount + 1) + ".txt";

        FilePath += MazeName;

        // Write mazeArray to file
        System.IO.File.WriteAllLines(FilePath, mazeToWrite);

    }

    //testing purposes
    private void PrintMaze(string[,] mazeArray)
    {
        string str = "";
        for (int row = 0; row < mazeArray.GetLength(0); row++)
        {

            for (int col = 0; col < mazeArray.GetLength(1); col++)
            {
                str += mazeArray[row, col] + " ";
            }
            str += "\n";
        }
        Debug.Log(str);
    }

    //add intersections after maze creation
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
