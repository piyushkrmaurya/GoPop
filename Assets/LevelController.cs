using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private int score = 0;
    private string lastPopped = "";
    private string levelType;
    [SerializeField] GameObject balloonPrefab;
    [SerializeField] GameObject pauseMenu;

    void Start()
    {
        int level = LevelSelector.selectedLevel;
        string levelHelpText = "Pop " + LevelSelector.selectedLevelName;
        Text levelHelp = GameObject.FindGameObjectWithTag("LevelHelp").GetComponent<Text>();
        levelHelp.text = levelHelpText;

        InvokeRepeating("Spawn", 0.02f, 1f);
    }

    public void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    
    public void Resume() {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }

    public void Exit() {
        SceneManager.LoadScene("StartMenu");
        Resume();
    }

    void Update()
    {
        
    }

    Balloon Spawn() {
        GameObject instance = Instantiate(balloonPrefab, transform.position, transform.rotation, this.transform);
        Balloon balloon = instance.GetComponent<Balloon>();
        balloon.offset = score;
        return balloon;
    }

    void OnEnable(){
        Balloon.OnPop += UpdateScore;
    }

    void OnDisable(){
        Balloon.OnPop -= UpdateScore;
    }

    void UpdateScore(string label){
        Debug.Log("Balloon with label = " + label + " was popped.");
        string matchCharacter = ((char)(label[0]-1)).ToString();
        if((label == "A" && lastPopped == "") || (matchCharacter == lastPopped)) {
            lastPopped = label;
            score++;
        }
        Text scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        scoreText.text = score.ToString();
    }
}
