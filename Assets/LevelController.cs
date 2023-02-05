using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum LevelType {
    CapitalLetters = 0,
    SmallLetters,
    Numbers,
    MaxLevelCount
}

public class LevelController : MonoBehaviour
{
    private int score = 0;
    private int lastPoppedIndex = -1;
    private LevelType level;

    private AudioSource backgoundAudio;
    private AudioSource correctPopAudio;
    private AudioSource incorrectPopAudio;

    [SerializeField] GameObject balloonPrefab;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;

    public static List<string>[] levelLabels = new List<string>[(int)LevelType.MaxLevelCount];

    void InitLevelLabels() {
        for(int levelIndex=0; levelIndex<(int)LevelType.MaxLevelCount; levelIndex++) {
            levelLabels[levelIndex] = new List<string>();
        }
        for (char x = 'A'; x <= 'Z'; x++) {
            levelLabels[(int)LevelType.CapitalLetters].Add(x.ToString());
        }
        for (char x = 'a'; x <= 'z'; x++) {
            levelLabels[(int)LevelType.SmallLetters].Add(x.ToString());
        }
        for (int i=1; i<=100; i++) {
            levelLabels[(int)LevelType.Numbers].Add(i.ToString());
        }
    }

    void Start()
    {
        InitLevelLabels();
        level = (LevelType)LevelSelectorButton.selectedLevel;
        string levelHelpText = "Pop " + LevelSelectorButton.selectedLevelName;
        TMP_Text levelHelp = GameObject.FindGameObjectWithTag("LevelHelp").GetComponent<TMP_Text>();
        levelHelp.text = levelHelpText;

        backgoundAudio = GetComponents<AudioSource>()[0];
        correctPopAudio = GetComponents<AudioSource>()[1];
        incorrectPopAudio = GetComponents<AudioSource>()[2];

        InvokeRepeating("Spawn", 0.02f, 1.6f);
    }

    public void GameOver() {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        backgoundAudio.Pause();
    }

    public void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        backgoundAudio.Pause();
    }

    
    public void Resume() {
        backgoundAudio.Play();
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
        balloon.offset = score % levelLabels[(int)level].Count;
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
            correctPopAudio.Play();
        } 
        else {
            incorrectPopAudio.Play();
        }
        TMP_Text scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TMP_Text>();
        scoreText.text = score.ToString();
    }
}
