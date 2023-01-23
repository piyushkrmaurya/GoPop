using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static int score = 0;
    public static string lastPopped = "";
    [SerializeField] GameObject balloonPrefab;

    void Start()
    {
        int level = LevelSelector.selectedLevel;
        string levelHelpText = "Pop " + LevelSelector.selectedLevelName;
        Text levelHelp = GameObject.FindGameObjectWithTag("LevelHelp").GetComponent<Text>();
        levelHelp.text = levelHelpText;

        InvokeRepeating("Spawn", 0.02f, 1f);
    }

    public void Pause() {
        SceneManager.LoadScene("PauseMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn() {
        Instantiate(balloonPrefab, transform.position, transform.rotation, this.transform);
    }
}
