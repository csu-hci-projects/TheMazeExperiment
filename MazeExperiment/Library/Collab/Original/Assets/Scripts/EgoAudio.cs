using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class EgoAudio : MonoBehaviour
{
    //Dir path of audio files
    private string audioDir = Directory.GetCurrentDirectory() + "/Assets/Audio/Ego/";
    //Audio source
    private AudioSource Source;
    //Current sound playing
    private AudioClip Clip;

    //List of AudioClips
    List<AudioClip> Clips = new List<AudioClip>();

    private int randomNum;

    bool waitActive = false;

    int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        randomNum = Random.Range(3, 6);

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

    void Play(int index)
    {
        Clip = Clips[index];
        Source.clip = Clip;
        Source.Play();

    }

    // Update is called once per frame
    void Update()
    {

        if (!waitActive)
        {

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                counter += 1;
            }

            if (counter == randomNum)
            {

                int randomFileIndex = (int)Random.Range(0, Clips.Count);
                Play(randomFileIndex);
                StartCoroutine(Wait((int)Clips[randomFileIndex].length));
                randomNum = (int)Random.Range(3, 6);
                counter = 0;

            }
        }

    }

    IEnumerator Wait(int time)
    {

        waitActive = true;
        yield return new WaitForSeconds(time + 4);
        waitActive = false;

    }

    IEnumerator GetAudioClip(string fileName)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(fileName, AudioType.OGGVORBIS))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                Clips.Add(myClip);
            }
        }
    }

}
