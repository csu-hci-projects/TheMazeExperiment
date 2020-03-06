using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton class to store the state of the application
public class PersistentManager : MonoBehaviour
{
    // Single instance of the class
    public static PersistentManager Instance { get; private set; }

    // Contents
    public string Current;

    // Instantiation of first call
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            // Delete duplicate creations of Instance
            Destroy(gameObject);
        }
    }


}
