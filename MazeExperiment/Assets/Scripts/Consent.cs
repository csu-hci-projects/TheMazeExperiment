using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Consent : MonoBehaviour
{

    public ScrollRect scrollRect;
    public GameObject agreement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scrollRect.verticalNormalizedPosition <= 0.01f)
        {
            agreement.SetActive(true);
        }

    }
}
