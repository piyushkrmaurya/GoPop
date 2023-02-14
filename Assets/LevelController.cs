using System.Collections;
using System.Collections.Generic;
using System;
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

public static class StringExtensions
{
    public static string AddColor(this string text, Color col) => $"<color={ColorHexFromUnityColor(col)}>{text}</color>";
    public static string ColorHexFromUnityColor(this Color unityColor) => $"#{ColorUtility.ToHtmlStringRGBA(unityColor)}";
}


public class LevelController : MonoBehaviour
{
    private int score = 0;
    private int lastPoppedIndex = -1;
    private LevelType level;
    private Animator sceneAnimator;
    private static string[] stickerTriggers = {"background_rain","background_sunshine","background_thunder","background_twinkle"};

    private AudioSource backgoundAudio;
    private AudioSource correctPopAudio;
    private AudioSource incorrectPopAudio;
    private TMP_Text levelHelp;
    private Queue<Tuple<string, Color32>> bannerLabels = new Queue<Tuple<string, Color32>>();

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
        string levelHelpText = "POP " + LevelSelectorButton.selectedLevelName;
        levelHelp = GameObject.FindGameObjectWithTag("LevelHelp").GetComponent<TMP_Text>();
        levelHelp.text = levelHelpText;

        backgoundAudio = GetComponents<AudioSource>()[0];
        correctPopAudio = GetComponents<AudioSource>()[1];
        incorrectPopAudio = GetComponents<AudioSource>()[2];
        sceneAnimator = GameObject.Find("Background").GetComponent<Animator>();

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
        balloon.offset = score;
        return balloon;
    }

    void OnEnable(){
        Balloon.OnPop += HandlePopEvent;
    }

    void OnDisable(){
        Balloon.OnPop -= HandlePopEvent;
    }

    void HandlePopEvent(Balloon balloon){
        if(balloon.label.text == "" && balloon.stickerIndex>=0) {
            sceneAnimator.SetTrigger(stickerTriggers[balloon.stickerIndex]);
            return;
        }
        else if(levelLabels[(int)level][lastPoppedIndex+1]==balloon.label.text) {
            lastPoppedIndex++;
            if(lastPoppedIndex == levelLabels[(int)level].Count-1) {
                lastPoppedIndex = -1;
            }
            balloon.GetComponentsInChildren<TMP_Text>()[1].text = "+1";

            Color32 balloonColor = balloon.GetComponent<SpriteRenderer>().color;
            bannerLabels.Enqueue(new Tuple<string, Color32>(balloon.label.text, balloonColor));

            if(bannerLabels.Count > 10) bannerLabels.Dequeue();

            string newBanner = $"";
            foreach (Tuple<string, Color32> label in bannerLabels) {
                newBanner += $"{label.Item1.AddColor(label.Item2)}" + " ";
            }

            levelHelp.text = newBanner;
            score++;
            correctPopAudio.Play();

            if(lastPoppedIndex == -1) {
                bannerLabels.Clear();
            }
        } 
        else {
            incorrectPopAudio.Play();
        }
        TMP_Text scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TMP_Text>();
        scoreText.text = score.ToString();
    }
}
