using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameAudio {
    StartMenu,
    Level,
    ButtonClick,
    CorrectPop,
    IncorrectPop,
    GameOver
};

public class AudioManager : MonoBehaviour {
    
    public List<AudioClip> audioClips = new List<AudioClip>();

    public static void Play(GameAudio audio) {
        AudioSource audioSource = new AudioSource();
        // audioSource.clip = audioClips[(int)audio];
        // audioSource.Play();
    }
}
