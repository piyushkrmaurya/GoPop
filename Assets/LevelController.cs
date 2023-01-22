using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject balloonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        int level = LevelSelector.selectedLevel;
        string levelHelpText = "Pop " + LevelSelector.selectedLevelName;
        Debug.Log(level);
        Debug.Log(levelHelpText);
        Text levelHelp = GetComponentInChildren<Text>();
        levelHelp.text = levelHelpText;

        InvokeRepeating("Spawn", 0.02f, 0.5f);
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
