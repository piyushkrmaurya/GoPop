using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Labels {
    CapitalLetters = 0,
    SmallLetters,
    Numbers,
    MaxLevelCount
}

public class LevelController : MonoBehaviour
{
    private int score = 0;
    private int lastPoppedIndex = -1;
    private Labels level;
    [SerializeField] GameObject balloonPrefab;
    [SerializeField] GameObject pauseMenu;
    public List<string>[] levelLabels = new List<string>[(int)Labels.MaxLevelCount];

    void InitLevelLabels() {
        for(int levelIndex=0; levelIndex<(int)Labels.MaxLevelCount; levelIndex++) {
            levelLabels[levelIndex] = new List<string>();
        }
        for (char x = 'A'; x <= 'Z'; x++) {
            levelLabels[(int)Labels.CapitalLetters].Add(x.ToString());
        }
        for (char x = 'a'; x <= 'z'; x++) {
            levelLabels[(int)Labels.SmallLetters].Add(x.ToString());
        }
        for (int i=1; i<=100; i++) {
            levelLabels[(int)Labels.Numbers].Add(i.ToString());
        }
    }

    void Start()
    {
        InitLevelLabels();
        level = (Labels)LevelSelector.selectedLevel;
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
        if(levelLabels[(int)level][lastPoppedIndex+1]==label) {
            lastPoppedIndex++;
            if(lastPoppedIndex == levelLabels[(int)level].Count-1) {
                lastPoppedIndex = -1;
            }
            score++;
        }
        Text scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        scoreText.text = score.ToString();
    }
}
