using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{

    public ScrollRect scrollRect;
    public GameObject nextButton;

    // Update is called once per frame
    void Update()
    {
        // Checks if the scrollbar has hit the bottom and shows the next button if so.
        if (scrollRect.verticalNormalizedPosition <= 0.01f)
        {
            nextButton.SetActive(true);
        }

    }
}
