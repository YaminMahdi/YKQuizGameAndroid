using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayMusic : MonoBehaviour
{
    public AudioSource playaudio;
    // Start is called before the first frame update      
    void Start() { }
    // Update is called once per frame      
    void Update() { }
    public void Play_Music()
    {
        playaudio.Play();
        Debug.Log("play");
    }
}