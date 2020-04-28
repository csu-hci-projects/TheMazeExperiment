using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

class AllocentricPlayerController : PlayerController
{

    // Override Update in abstract PlayerController class
    protected override void Update()
    {

        // Check if the player is allowed to press another key
        if (allowInput)
        {

            // Move up
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                // gameObject.GetComponent<InfoTracker>().SetDirection("Up", "Allo");
                // Stop taking player input until coroutine ends
                allowInput = false;
                direction = "up";

                StartCoroutine(MovetoIntersection((moveToVector) =>
                {
                    // Set new vector to move to
                    toPosition = moveToVector;

                    // If new vector isn't the same as current, a valid move was made
                    if (!toPosition.Equals(player.transform.position))
                    {

                        // Set moving to true
                        movingToTarget = true;
                    }
                    else
                    {

                        // New vector is the same, no movement needed so allow input again
                        allowInput = true;                    
                    }
                }));
            }
            // Move down
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                // gameObject.GetComponent<InfoTracker>().SetDirection("Down", "Allo");
                // Stop taking player input until coroutine ends
                allowInput = false;
                direction = "down";

                StartCoroutine(MovetoIntersection((moveToVector) =>
                {

                    // Set new vector to move to
                    toPosition = moveToVector;

                    // If new vector isn't the same as current, a valid move was made
                    if (!toPosition.Equals(player.transform.position))
                    {
                        // Set moving to true
                        movingToTarget = true;
                    }
                    else
                    {
                        // New vector is the same, no movement needed so allow input again
                        allowInput = true;
                    }
                }));
            }
            // Move left
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // gameObject.GetComponent<InfoTracker>().SetDirection("Left", "Allo");
                // Stop taking player input until coroutine ends
                allowInput = false;
                direction = "left";

                StartCoroutine(MovetoIntersection((moveToVector) =>
                {

                    // Set new vector to move to
                    toPosition = moveToVector;

                    // If new vector isn't the same as current, a valid move was made
                    if (!toPosition.Equals(player.transform.position))
                    {
                        // Set moving to true
                        movingToTarget = true;
                    }
                    else
                    {
                        // New vector is the same, no movement needed so allow input again
                        allowInput = true;
                    }
                }));
            }
            // Move right
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                // gameObject.GetComponent<InfoTracker>().SetDirection("Right", "Allo");
                // Stop taking player input until coroutine ends
                allowInput = false;
                direction = "right";

                StartCoroutine(MovetoIntersection((moveToVector) =>
                {

                    // Set new vector to move to
                    toPosition = moveToVector;

                    // If new vector isn't the same as current, a valid move was made
                    if (!toPosition.Equals(player.transform.position))
                    {
                        // Set moving to true
                        movingToTarget = true;
                    }
                    else
                    {
                        // New vector is the same, no movement needed so allow input again
                        allowInput = true;
                    }
                }));
            }

        }

        // A valid move vector has been found and the player needs to move to it.
        if (movingToTarget == true)
        {
            //Constant updating
            PersistentManager.Instance.currentLocation = new int[] { (int)Math.Round(transform.position.x), (int)Math.Round(transform.position.z) };

            if (direction == "up")
            {
                // Move to new vector
                transform.position = Vector3.MoveTowards(transform.position, toPosition, MoveSpeed * Time.deltaTime);
                // Player reaches new vector
                if (NearlyEqual(transform.position.x, toPosition.x, 0.1f))
                {
                    // Now that player is at new location add it to the VisitedList in InfoTracker
                    gameObject.GetComponent<ErrorChecker>().CheckErrors();
                    gameObject.GetComponent<InfoTracker>().SetDirection("Up", "Allo");

                    // Set new player position to new
                    PersistentManager.Instance.currentLocation = new int[] { (int)Math.Round(toPosition[0]), (int)Math.Round(toPosition[2]) };

                    CheckFinish();
                }
            }
            else if (direction == "down")
            {
                // Move to new vector
                transform.position = Vector3.MoveTowards(transform.position, toPosition, MoveSpeed * Time.deltaTime);
                // Player reaches new vector
                if (NearlyEqual(transform.position.x, toPosition.x, 0.1f))
                {
                    // Now that player is at new location add it to the VisitedList in InfoTracker
                    gameObject.GetComponent<ErrorChecker>().CheckErrors();
                    gameObject.GetComponent<InfoTracker>().SetDirection("Down", "Allo");

                    // Set new player position to new
                    PersistentManager.Instance.currentLocation = new int[] { (int)Math.Round(toPosition[0]), (int)Math.Round(toPosition[2]) };

                    CheckFinish();
                }
            }
            else if (direction == "left")
            {
                // Move to new vector
                transform.position = Vector3.MoveTowards(transform.position, toPosition, MoveSpeed * Time.deltaTime);
                // Player reaches new vector
                if (NearlyEqual(transform.position.z, toPosition.z, 0.1f))
                {
                    // Now that player is at new location add it to the VisitedList in InfoTracker
                    gameObject.GetComponent<ErrorChecker>().CheckErrors();
                    gameObject.GetComponent<InfoTracker>().SetDirection("Left", "Allo");

                    // Set new player position to new
                    PersistentManager.Instance.currentLocation = new int[] { (int)Math.Round(toPosition[0]), (int)Math.Round(toPosition[2]) };

                    CheckFinish();
                }
            }
            else if (direction == "right")
            {
                // Move to new vector
                transform.position = Vector3.MoveTowards(transform.position, toPosition, MoveSpeed * Time.deltaTime);
                // Player reaches new vector
                if (NearlyEqual(transform.position.z, toPosition.z, 0.1f))
                {
                    // Now that player is at new location add it to the VisitedList in InfoTracker
                    gameObject.GetComponent<ErrorChecker>().CheckErrors();
                    gameObject.GetComponent<InfoTracker>().SetDirection("Right", "Allo");

                    // Set new player position to new
                    PersistentManager.Instance.currentLocation = new int[] { (int)Math.Round(toPosition[0]), (int)Math.Round(toPosition[2]) };

                    CheckFinish();
                }
            }


        }
        // Not moving, round and set current position (keep player in grid)
        else
        {
            Vector3 move;
            move.x = (float)Math.Round(transform.position.x);
            move.y = (float)Math.Round(transform.position.y);
            move.z = (float)Math.Round(transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, move, MoveSpeed * Time.deltaTime);
        }

        //PersistentManager.Instance.currentLocation = playerPosition;
    }



    // Movement coroutine - returns vector to move to
    IEnumerator MovetoIntersection(System.Action<Vector3> callback)
    {
        Vector3 moveVector = transform.position;

        // Player is trying to move up
        if (direction == "up")
        {
            // Check above player position
            for (int row = playerPosition[0] - 1; row < mazeArray.GetLength(0); row--)
            {
                // Out of bounds check
                if (row < 0)
                {
                    break;
                }

                // Wall
                if (mazeArray[row, playerPosition[1]] == "1N")
                {
                    break;
                }
                // Intersection, End, Start
                else if (mazeArray[row, playerPosition[1]] == "0I" || mazeArray[row, playerPosition[1]] == "0F" || mazeArray[row, playerPosition[1]] == "0S")
                {
                    if (mazeArray[row, playerPosition[1]] == "0F" && PersistentManager.Instance.audioWaitActive)
                    {
                        break;
                    }
                    else
                    {
                        moveVector.x -= 1;
                        playerPosition[0] = row;
                        break;
                    }
                }
                // Empty - increment movement but don't break out
                else if (mazeArray[row, playerPosition[1]] == "0N")
                {
                    moveVector.x -= 1;
                    playerPosition[0] = row;
                }

            }
        }
        // Player is trying to move down
        else if (direction == "down")
        {
            // Check below player position
            for (int row = playerPosition[0] + 1; row < mazeArray.GetLength(0); row++)
            {
                // Out of bounds check
                if (row > 14)
                {
                    break;
                }

                // Wall
                if (mazeArray[row, playerPosition[1]] == "1N")
                {
                    //print("WALL");
                    break;
                }
                // Intersection, End, Start
                else if (mazeArray[row, playerPosition[1]] == "0I" || mazeArray[row, playerPosition[1]] == "0F" || mazeArray[row, playerPosition[1]] == "0S")
                {
                    if (mazeArray[row, playerPosition[1]] == "0F" && PersistentManager.Instance.audioWaitActive)
                    {
                        break;
                    }
                    else
                    {
                        moveVector.x += 1;
                        playerPosition[0] = row;
                        break;
                    }
                }
                // Empty - increment movement but don't break out
                else if (mazeArray[row, playerPosition[1]] == "0N")
                {
                    moveVector.x += 1;
                    playerPosition[0] = row;
                }

            }

        }
        // Player is trying to move left
        else if (direction == "left")
        {
            // Check to the left of player position
            for (int col = playerPosition[1] - 1; col < mazeArray.GetLength(1); col--)
            {
                // Out of bounds check
                if (col < 0)
                {
                    break;
                }

                // Wall
                if (mazeArray[playerPosition[0], col] == "1N")
                {
                    //print("WALL");
                    break;
                }
                // Intersection, End, Start
                else if (mazeArray[playerPosition[0], col] == "0I" || mazeArray[playerPosition[0], col] == "0F" || mazeArray[playerPosition[0], col] == "0S")
                {
                    if (mazeArray[playerPosition[0], col] == "0F" && PersistentManager.Instance.audioWaitActive)
                    {
                        break;
                    }
                    else
                    {
                        moveVector.z -= 1;
                        playerPosition[1] = col;
                        break;
                    }
                }
                // Empty - increment movement but don't break out
                else if (mazeArray[playerPosition[0], col] == "0N")
                {
                    moveVector.z -= 1;
                    playerPosition[1] = col;
                }

            }

        }
        // Player is trying to move right
        else if (direction == "right")
        {
            // Check to the right of player position
            for (int col = playerPosition[1] + 1; col < mazeArray.GetLength(1); col++)
            {
                // Out of bounds check
                if (col > 14)
                {
                    break;
                }

                // Wall
                if (mazeArray[playerPosition[0], col] == "1N")
                {
                    //print("WALL");
                    break;
                }
                // Intersection, End, Start
                else if (mazeArray[playerPosition[0], col] == "0I" || mazeArray[playerPosition[0], col] == "0F" || mazeArray[playerPosition[0], col] == "0S")
                {
                    if (mazeArray[playerPosition[0], col] == "0F" && PersistentManager.Instance.audioWaitActive)
                    {
                        break;
                    }
                    else
                    {
                        moveVector.z += 1;
                        playerPosition[1] = col;
                        break;
                    }
                }
                // Empty - increment movement but don't break out
                else if (mazeArray[playerPosition[0], col] == "0N")
                {
                    moveVector.z += 1;
                    playerPosition[1] = col;
                }

            }


        }
        // Shouldn't happen (player isn't facing a 90 degree angle)
        else
        {
            yield return player.transform.position;
            callback(player.transform.position);

        }

        // Call to return vector from coroutine
        callback(moveVector);

    }


}
