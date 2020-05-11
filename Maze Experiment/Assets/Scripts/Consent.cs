using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Consent : MonoBehaviour
{

    public ScrollRect scrollRect;
    public GameObject agreement;

    public GameObject consent;
    public GameObject egoInstructions;
    public GameObject alloInstructions;
    public GameObject egoAlloInstructions;


    // Changes to either a different instruction set depending on constraints
    public void ChangeToInstructions()
    {
        // Show ego instructions
        if (PersistentManager.Instance.egoCentric == true && PersistentManager.Instance.alloCentric == false)
        {
            consent.SetActive(false);
            egoInstructions.SetActive(true);
        }
        // Show allo instructions
        else if (PersistentManager.Instance.egoCentric == false && PersistentManager.Instance.alloCentric == true)
        {
            consent.SetActive(false);
            alloInstructions.SetActive(true);
        }
        // Show ego and allo instructions
        else if (PersistentManager.Instance.egoCentric == true && PersistentManager.Instance.alloCentric == true)
        {
            consent.SetActive(false);
            egoAlloInstructions.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Show agreement checkbox when the scrollbar hits the bottom
        if (scrollRect.verticalNormalizedPosition <= 0.01f)
        {
            agreement.SetActive(true);
        }

    }
}
