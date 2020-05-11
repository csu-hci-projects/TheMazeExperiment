using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorChecker : MonoBehaviour
{
    // List of locations the player has been to
    private List<int[]> visitedList = new List<int[]>();

    // Checks for errors made after every move. Called in each PlayerController class.
    public void CheckErrors()
    {
        for (int i = 0; i < visitedList.Count; i++)
        {
            if ((visitedList[i][0] == PersistentManager.Instance.currentLocation[0]) && (visitedList[i][1] == PersistentManager.Instance.currentLocation[1]))
            {
                PersistentManager.Instance.totalErrors++;
            }
        }

        visitedList.Add(PersistentManager.Instance.currentLocation);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if at the end, change scene wether number of attempts is reached or not
        int currentX = PersistentManager.Instance.currentLocation[0];
        int currentZ = PersistentManager.Instance.currentLocation[1];
        int endX = PersistentManager.Instance.endLocation[0];
        int endZ = PersistentManager.Instance.endLocation[1];

        // Increment attempts if at end
        if (currentX == endX && currentZ == endZ)
        {

            //Clear visitedList and reset errors made to 0
            visitedList.Clear();
            PersistentManager.Instance.totalErrors = 0;

        }
    }
}
