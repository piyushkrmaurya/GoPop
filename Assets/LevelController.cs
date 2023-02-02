using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum LevelTypes {
    CapitalLetters = 0,
    SmallLetters,
    Numbers,
    MaxLevelCount
}

public class LevelController : MonoBehaviour
{
    private int score = 0;
    private int lastPoppedIndex = -1;
    private LevelTypes level;
    [SerializeField] GameObject balloonPrefab;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;

    public List<string>[] levelLabels = new List<string>[(int)LevelTypes.MaxLevelCount];

    void InitLevelLabels() {
        for(int levelIndex=0; levelIndex<(int)LevelTypes.MaxLevelCount; levelIndex++) {
            levelLabels[levelIndex] = new List<string>();
        }
        for (char x = 'A'; x <= 'Z'; x++) {
            levelLabels[(int)LevelTypes.CapitalLetters].Add(x.ToString());
        }
        for (char x = 'a'; x <= 'z'; x++) {
            levelLabels[(int)LevelTypes.SmallLetters].Add(x.ToString());
        }
        for (int i=1; i<=100; i++) {
            levelLabels[(int)LevelTypes.Numbers].Add(i.ToString());
        }
    }

    void Start()
    {
        InitLevelLabels();
        level = (LevelTypes)LevelSelector.selectedLevel;
        string levelHelpText = "Pop " + LevelSelector.selectedLevelName;
        TMP_Text levelHelp = GameObject.FindGameObjectWithTag("LevelHelp").GetComponent<TMP_Text>();
        levelHelp.text = levelHelpText;

        InvokeRepeating("Spawn", 0.02f, 1f);
    }

    public void GameOver() {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
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
        if(levelLabels[(int)level][lastPoppedIndex+1]==label) {
            lastPoppedIndex++;
            if(lastPoppedIndex == levelLabels[(int)level].Count-1) {
                lastPoppedIndex = -1;
            }
            score++;
        } 
        else {
            GameOver();
        }
        TMP_Text scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TMP_Text>();
        scoreText.text = score.ToString();
    }
}
