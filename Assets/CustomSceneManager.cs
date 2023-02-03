using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CustomSceneManager : MonoBehaviour
{

    private IEnumerator CustomLoadSceneRoutine(string sceneNameToLoad) {
        AudioSource audioSource = GetComponent<AudioSource>();
        float delaySeconds = 0;
        if(audioSource != null) {
            delaySeconds = audioSource.clip.length;
            Debug.Log(audioSource);
            Debug.Log(delaySeconds);
        }
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene(sceneNameToLoad);
    }

    public void CustomLoadScene(string sceneNameToLoad) {
        StartCoroutine(CustomLoadSceneRoutine(sceneNameToLoad));
    }

}
