using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelectorButton : MonoBehaviour
{

    public static int selectedLevel;
    public static string selectedLevelName;
    public int level;

    private static AudioSource clickAudio;

    private Button button;

    private static IEnumerator DelaySceneLoad(float delaySeconds) {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene("Level");
    }

    private void StartLevel() {
        clickAudio.Play();
        selectedLevel = level;
        selectedLevelName = GetComponentInChildren<TMP_Text>().text;
        StartCoroutine(DelaySceneLoad(clickAudio.clip.length));
    }

    void Start() {
        clickAudio = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        button.onClick.AddListener(clickAudio.Play);
		button.onClick.AddListener(StartLevel);
    }
}
