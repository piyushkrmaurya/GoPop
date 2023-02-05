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

public class AudioManager {
    
    public static List<string> audioNames = new List<string>{
        "start_menu_background_music",
        "level_background_music",
        "button_clic",
        "yay",
        "uhoh",
        "game_over"
    };

    public static List<AudioSource> audioSources = new List<AudioSource>();

    public void Start() {
        for(int i=0; i<audioSources.Count; i++) {
            AudioSource audioSource = new AudioSource();
            audioSource.clip = (AudioClip)Resources.Load(audioNames[i], typeof(AudioClip));
            audioSources.Add(audioSource);
        }
    }

    public static void Play(GameAudio audio) {
        Debug.Log(audio);
        Debug.Log((int)audio);
        Debug.Log(audioSources.Count);
        audioSources[(int)audio].Play();
    }
}
