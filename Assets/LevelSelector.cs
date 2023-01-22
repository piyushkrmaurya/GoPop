using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public static int selectedLevel;
    public static string selectedLevelName;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartLevel() {
        // Button button = GetComponent<Button>();
        Button button = gameObject.GetComponent<Button>();
        selectedLevel = level;
        selectedLevelName = button.GetComponentInChildren<TMP_Text>().text;
        SceneManager.LoadScene("Level");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
