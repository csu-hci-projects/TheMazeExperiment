using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class MazeBuilder : MonoBehaviour
{
    // Player variables
    public GameObject player;
    public GameObject camera;

    // Materials for randomized colors
    public Material red;
    public Material orange;
    public Material yellow;
    public Material blue;
    public Material cyan;
    public Material green;
    public Material purple;
    public Material pink;
    public Material white;

    public Material black;
    public Material grey;

    // Materials for randomized colors
    public Material redLight;
    public Material orangeLight;
    public Material yellowLight;
    public Material blueLight;
    public Material cyanLight;
    public Material greenLight;
    public Material purpleLight;
    public Material pinkLight;
    public Material whiteLight;

    // Maze variables
    private int originalX = 0;
    private int originalY = 0;
    private int originalZ = 0;

    private static int mazeSize;
    private string[,] mazeArray;

    private List<Material> colorList;
    private List<Material> lightColorList;

    // Returns a bool based on if the indexes fits withinn the mazeSize
    bool IsIndexBounded(int mazeSize, int i, int j)
    {
        if (i >= mazeSize || j >= mazeSize)
        {
            return false;
        }
        if (i < 0 || j < 0)
        {
            return false;
        }

        return true;
    }

    // TODO: Make dynamic for number of sectors (currently harcoded to 15x15)
    // Find quadrant based on number of sectors and position
    int FindQuadrant(int numSectors, int i, int j)
    {

        if (mazeSize == 9)
        {
            // 012 345 678
            // Check sector 1
            if ((i >= 0 && i < 3) && (j >= 0 && j < 3))
            {
                return 0;
            }

            // Check sector 2
            if ((i >= 0 && i < 3) && (j > 2 && j < 6))
            {
                return 1;
            }

            // Check sector 3
            if ((i >= 0 && i < 3) && (j > 5 && j < 9))
            {
                return 2;
            }

            // Check sector 4
            if ((i >= 2 && i < 6) && (j >= 0 && j < 3))
            {
                return 3;
            }

            // Check sector 5
            if ((i > 2 && i < 6) && (j > 2 && j < 6))
            {
                return 4;
            }

            // Check sector 6
            if ((i > 2 && i < 6) && (j > 5 && j < 9))
            {
                return 5;
            }

            // Check sector 7
            if ((i > 5 && i < 9) && (j >= 0 && j < 3))
            {
                return 6;
            }

            // Check sector 8
            if ((i > 5 && i < 9) && (j > 2 && j < 6))
            {
                return 7;
            }

            // Check sector 9
            if ((i > 5 && i < 9) && (j > 5 && j < 9))
            {
                return 8;
            }
        }


        if (mazeSize == 15)
        {

            // Check sector 1
            if ((i >= 0 && i < 5) && (j >= 0 && j < 5))
            {
                return 0;
            }

            // Check sector 2
            if ((i >= 0 && i < 5) && (j > 4 && j < 10))
            {
                return 1;
            }

            // Check sector 3
            if ((i >= 0 && i < 5) && (j > 9 && j < 15))
            {
                return 2;
            }

            // Check sector 4
            if ((i > 4 && i < 10) && (j >= 0 && j < 5))
            {
                return 3;
            }

            // Check sector 5
            if ((i > 4 && i < 10) && (j > 4 && j < 10))
            {
                return 4;
            }

            // Check sector 6
            if ((i > 4 && i < 10) && (j > 9 && j < 15))
            {
                return 5;
            }

            // Check sector 7
            if ((i > 9 && i < 15) && (j >= 0 && j < 5))
            {
                return 6;
            }

            // Check sector 8
            if ((i > 9 && i < 15) && (j > 4 && j < 10))
            {
                return 7;
            }

            // Check sector 9
            if ((i > 9 && i < 15) && (j > 9 && j < 15))
            {
                return 8;
            }
        }

        // Error 
        return -1;
    }

    // Creates a list of randomized colors for maze building
    void GenerateSectorColors()
    {

        // Instantiate color list (egocentric)
        colorList = new List<Material>();
        colorList.Add(red);
        colorList.Add(orange);
        colorList.Add(yellow);
        colorList.Add(blue);
        colorList.Add(cyan);
        colorList.Add(green);
        colorList.Add(purple);
        colorList.Add(pink);
        colorList.Add(white);

        // Instantiate light color list (allocentric)
        lightColorList = new List<Material>();
        lightColorList.Add(redLight);
        lightColorList.Add(orangeLight);
        lightColorList.Add(yellowLight);
        lightColorList.Add(blueLight);
        lightColorList.Add(cyanLight);
        lightColorList.Add(greenLight);
        lightColorList.Add(purpleLight);
        lightColorList.Add(pinkLight);
        lightColorList.Add(whiteLight);

        // Temp list of colors for random sorting
        List<Material> tempList = new List<Material>();
        List<Material> tempLightList = new List<Material>();

        // Generate random number for length of color list
        System.Random rand = new System.Random();

        // Randomly pick elements from list to make new randomly ordered array 
        int counter = 0;
        while (counter < colorList.Count)
        {
            int randInt = rand.Next(0, colorList.Count);
            if (!tempList.Contains(colorList[randInt]))
            {
                tempList.Add(colorList[randInt]);
                tempLightList.Add(lightColorList[randInt]);
                counter++;
            }

        }

        // Set tempList to colorList for other method use
        colorList = tempList;
        lightColorList = tempLightList;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Create list of randomly ordered colors for building sector
        GenerateSectorColors();

        // Read maze into array from file in PlayerPrefs
        mazeArray = ReadMazeFile();

        // If allo, set camera above
        if (PersistentManager.Instance.CurrentScene == 2)
        {
            Vector3 middlePosition = new Vector3(mazeSize / 2, mazeSize, mazeSize / 2);
            camera.transform.position = middlePosition;
            camera.transform.Rotate(90, 270, 0);
        }

        
        //Build maze from file 
        BuildMaze(mazeArray);


    }

    // Read maze from PlayerPrefs file 
    public string[,] ReadMazeFile()
    {
        // TODO: Remove later, for testing purposes only. 
        PersistentManager.Instance.mazeSize = 15;

        int index = PersistentManager.Instance.mazeArrayIndex;
        string file = "Maze-" + PersistentManager.Instance.listOfMazes[index].ToString() + ".txt";

        // Set maze size from PlayerPrefs 
        mazeSize = PersistentManager.Instance.mazeSize;

        // Create mazeArray with maze size 
        string[,] mazeArray = new string[mazeSize, mazeSize];

        // TODO: Add command line checks for which OS is running, then modify PATH from there to load file in
        //var currentOS = Environment.OSVersion.Platform();

        // Read file into mazeArray
        string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "/Assets/Mazes/" + file);

        // Read lines into mazeArray 
        for (int i = 0; i < lines.Length; i++)
        {
            string[] splitArray = lines[i].Split(' ');
            for (int j = 0; j < splitArray.Length; j++)
            {
                mazeArray[i, j] = splitArray[j];
            }
        }

        PersistentManager.Instance.mazeArray = mazeArray;

        return mazeArray;
    }

    // Build the maze dynamically based on 2D string array passed from GUI
    void BuildMaze(string[,] mazeArray)
    {

        // Create generic cube for building 
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // Counter variables to be incremented for line by line creation 
        int currentX = 0;
        int currentY = 0;
        int currentZ = 0;

        // Build floor for maze 1 by 1
        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {
                // Build floor color based on ego or allo

                cube.GetComponent<Renderer>().material = black;

                //Set cube transform to current position
                cube.transform.position = new Vector3(currentX, 0, currentZ);

                // Set name to index
                string floorName = "Floor-" + i.ToString() + "-" + j.ToString();
                cube.name = floorName;

                //Instantiate cube of declared size 
                Instantiate(cube);

                // Increment Vector3 variable to move down the row 
                currentZ++;
            }

            // Increment originalX
            currentX += 1;

            // Move back to begining of row then increment by one to start next row
            cube.transform.position = new Vector3(currentX, originalY, originalZ);

            // Set back to begining of row 
            currentZ = originalZ;
        }


        // Set size of cube
        int height = 1;
        cube.transform.localScale = new Vector3(1, height, 1);


        // Counter variables to be incremented for line by line creation 
        currentX = 0;
        currentY = 0;
        currentZ = 0;

        // Build maze 1 by 1 and check for identifiers (S,E,C,N)
        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {

                // Check whether a wall or not
                if (mazeArray[i, j][0] == '1')
                {
                    // Set wall color to black if Allo and random color if Ego
                    if (PersistentManager.Instance.CurrentScene == 2)
                    {
                        cube.GetComponent<Renderer>().material = black;
                    }
                    else
                    {
                        cube.GetComponent<Renderer>().material = colorList[FindQuadrant(mazeSize, i, j)];
                    }

                    //Set cube transform to current position
                    cube.transform.position = new Vector3(currentX, (float)(currentY + (0.5 * height) + 0.5), currentZ);

                    // Set name to index
                    string wallName = "Wall-" + i.ToString() + "-" + j.ToString();
                    cube.name = wallName;

                    //Instantiate cube of declared size 
                    Instantiate(cube);
                    //Debug.Log("Instantiating cube at CurrentX: " + currentX + " CurrentY: " + currentY + " CurrentZ: " + currentZ);

                }

                // Check if begining
                if (mazeArray[i, j][1] == 'S')
                {

                    // Add start position to currentPosition for later tracking
                    PersistentManager.Instance.startLocation = new int[] { i, j};

                    // Set player location to start location
                    player.transform.position = new Vector3(currentX, 1, currentZ);

                    // Set player rotation based on start postition
                    if (i == 0)
                    {
                        player.transform.Rotate(0, 90, 0);

                    }
                    else if (i == (mazeSize - 1))
                    {
                        player.transform.Rotate(0, 270, 0);

                    }
                    else if (j == 0)
                    {
                        player.transform.Rotate(0, 0, 0);

                    }
                    else if (j == (mazeSize - 1))
                    {
                        player.transform.Rotate(0, 180, 0);
                    }
                    else
                    {

                        // Check if bounded then look around for direction to pick

                        // Check up
                        if (IsIndexBounded(mazeSize, i + 1, j))
                        {
                            if (mazeArray[i + 1, j][0] == '0')
                            {
                                player.transform.Rotate(0, 270, 0);
                            }
                        }

                        // Check down
                        else if (IsIndexBounded(mazeSize, i - 1, j))
                        {
                            if (mazeArray[i - 1, j][0] == '0')
                            {
                                player.transform.Rotate(0, 90, 0);
                            }
                        }

                        // Check left
                        else if (IsIndexBounded(mazeSize, i, j - 1))
                        {
                            if (mazeArray[i, j - 1][0] == '0')
                            {
                                player.transform.Rotate(0, 180, 0);
                            }
                        }

                        //Check right
                        else if (IsIndexBounded(mazeSize, i, j + 1))
                        {
                            if (mazeArray[i, j + 1][0] == '0')
                            {
                                player.transform.Rotate(0, 0, 0);
                            }
                        }

                    }


                }
                // Check if end
                if (mazeArray[i, j][1] == 'F')
                {
                    // Set end coordiantes for info tracking
                    PersistentManager.Instance.endLocation = new int[] { i, j };
                }

                // Increment Vector3 variable to move down the row 
                currentZ++;

            }

            // Increment originalX
            currentX += 1;

            // Move back to begining of row then increment by one to start next row
            cube.transform.position = new Vector3(currentX, originalY, originalZ);

            // Set back to begining of row 
            currentZ = originalZ;

        }

    }


    // Reset floor colors to non-light materials
    void ResetLightColors()
    {
        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {
                string cubeName = "Floor-" + i.ToString() + "-" + j.ToString() + "(Clone)";
                GameObject.Find(cubeName).GetComponent<Renderer>().material = grey;
            }
        }
    }

    // Color around a given intersection
    void ColorAround(int i, int j)
    {

        // Loop through area around index, check if bounded and a floor
        for (int x = i - 1; x < i + 2; x++)
        {
            for (int y = j - 1; y < j + 2; y++)
            {
                //print("Looking at " + x + " " + y);
                if (IsIndexBounded(mazeSize, x, y))
                {
                    if (mazeArray[x, y][0] == '0')
                    {
                        // Light up floor
                        string cubeName = "Floor-" + x.ToString() + "-" + y.ToString() + "(Clone)";
                        GameObject.Find(cubeName).GetComponent<Renderer>().material = lightColorList[FindQuadrant(mazeSize, x, y)];
                        //print(cubeName + " colored");
                    }
                }
            }
        }
    }

    // Color hallway between intersections
    void ColorHallway(ref int i, ref int j, string direction)
    {
        // Loop through one direction until intersection and color floor
        bool isIntersection = true;
        while (mazeArray[i, j][1] != 'I' || isIntersection)
        {

            // Get component from name and update color
            string cubeName = "Floor-" + i.ToString() + "-" + j.ToString() + "(Clone)";
            GameObject.Find(cubeName).GetComponent<Renderer>().material = lightColorList[FindQuadrant(mazeSize, i, j)];
            //print(cubeName + " colored");

            // Move in proper direction
            if (direction == "UP")
            {
                i++;
            }
            else if (direction == "DOWN")
            {
                i--;
            }
            else if (direction == "LEFT")
            {
                j++;
            }
            else if (direction == "RIGHT")
            {
                j--;
            }

            // Force proper boolean in while
            isIntersection = false;

            // Break if out of bounds or if wall and set back index if issue
            if (IsIndexBounded(mazeSize, i, j))
            {
                // Reverse move if wall is found 
                if (mazeArray[i, j][0] == '1')
                {
                    if (direction == "UP")
                    {
                        i--;
                    }
                    else if (direction == "DOWN")
                    {
                        i++;
                    }
                    else if (direction == "LEFT")
                    {
                        j--;
                    }
                    else if (direction == "RIGHT")
                    {
                        j++;
                    }

                    break;
                }

            }
            else
            {
                // Reverse move if out of bounds
                if (direction == "UP")
                {
                    i--;
                }
                else if (direction == "DOWN")
                {
                    i++;
                }
                else if (direction == "LEFT")
                {
                    j--;
                }
                else if (direction == "RIGHT")
                {
                    j++;
                }

                break;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PersistentManager.Instance.CurrentScene == 2)
        {

            // Get current position of maze from transposition 
            int cX = PersistentManager.Instance.currentLocation[0];
            int cZ = PersistentManager.Instance.currentLocation[1];

            // Set current direction 
            int i = cX;
            int j = cZ;

            // Light start of maze if necessary
            if (mazeArray[i, j][1] == 'S')
            {
                // Color intersection
                ColorAround(i, j);
            }

            // If player is at intersection light all around them 
            if (mazeArray[i, j][1] == 'I')
            {
                // Light up all blocks around current intersection
                ColorAround(i, j);

                // Reset colors to erase past hallways
                ResetLightColors();

                // Color hallway in each direction and color intersections
                string[] directions = new string[] { "UP", "DOWN", "LEFT", "RIGHT" };
                for (int index = 0; index < 4; index++)
                {

                    // Color hallway
                    i = cX;
                    j = cZ;
                    ColorHallway(ref i, ref j, directions[index]);

                    // Color intersection
                    ColorAround(i, j);


                }

            }
        }

    }
}
