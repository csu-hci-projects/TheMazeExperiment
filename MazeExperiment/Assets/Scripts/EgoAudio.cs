using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class EgoAudio : MonoBehaviour
{
    //Dir path of audio files
    private string audioDir = Directory.GetCurrentDirectory() + "/Assets/Audio/Ego";
    //Audio source
    private AudioSource Source;
    //Current sound playing
    private AudioClip Clip;

    //List of AudioClips
    List<AudioClip> Clips = new List<AudioClip>();

    //See if audio is disabled
    private bool disabledAudio = PersistentManager.Instance.disableAudio;

    private int randomNum;

    private bool waitActive = false;

    int counter = 0;

    private int WaitDelay = 3;
    public int MinMoves = 3;
    public int MaxMoves = 6;

    private bool hasAnswered = false;

    private int randomFileIndex;

    // Start is called before the first frame update
    void Start()
    {
        if (!disabledAudio)
        {
            randomNum = Random.Range(MinMoves, MaxMoves);

            Source = GetComponent<AudioSource>();

            //Grabs all files from audioDir
            string[] files;
            files = Directory.GetFiles(audioDir);

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].EndsWith(".ogg"))
                {

                    StartCoroutine(GetAudioClip(files[i]));

                }

            }
        }
    }

    void Play(int index)
    {
        if (PersistentManager.Instance.mazeArrayIndex < 10)
        {
            Clip = Clips[index];
            Source.clip = Clip;
            Source.Play();
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (!waitActive && !disabledAudio)
        {

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                counter += 1;
            }

            if (counter == randomNum)
            {

                randomFileIndex = (int)Random.Range(0, Clips.Count);
                Play(randomFileIndex);
                StartCoroutine(Wait((int)Clips[randomFileIndex].length));
                randomNum = (int)Random.Range(MinMoves, MaxMoves);
                counter = 0;

            }
        }

        if (waitActive && !disabledAudio)
        {

            if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.N))
            {

                hasAnswered = true;

                if (Input.GetKeyDown(KeyCode.Y))
                {

                    PersistentManager.Instance.audioAnswer = "y";
                    // Add Audio Answer to csv
                    gameObject.GetComponent<InfoTracker>().SetDirection("NULL", "Ego", "y");

                }

                else if (Input.GetKeyDown(KeyCode.N))
                {

                    PersistentManager.Instance.audioAnswer = "n";
                    // Add Audio Answer to csv
                    gameObject.GetComponent<InfoTracker>().SetDirection("NULL", "Ego", "n");

                }

            }



        }

    }

    IEnumerator Wait(int time)
    {

        waitActive = true;
        yield return new WaitForSeconds(time + WaitDelay);

        if (!hasAnswered)
        {

            PersistentManager.Instance.totalErrors += 1;

        }
        else if (hasAnswered)
        {
            hasAnswered = false;
            PersistentManager.Instance.audioAnswer = "";
        }

        waitActive = false;

    }

    IEnumerator GetAudioClip(string fileName)
    {

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(new System.Uri(fileName).AbsoluteUri, AudioType.OGGVORBIS))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                myClip.name = fileName;
                Clips.Add(myClip);
            }
        }
    }

    public string GetAudioCheck()
    {

        if (PersistentManager.Instance.mazeArrayIndex >= 10)
        {
            return "0";
        }
        else if (waitActive)
        {
            Clip = Clips[randomFileIndex];
            string name = Clip.name;
            name = name.Split('_', '.')[1];

            return name;
        }
        else
        {
            return "0";
        }

    }

}
