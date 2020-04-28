using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{

    public ScrollRect scrollRect;
    public GameObject nextButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (scrollRect.verticalNormalizedPosition <= 0.01f)
        {
            nextButton.SetActive(true);
        }

    }
}
