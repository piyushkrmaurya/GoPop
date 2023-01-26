using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    void Start()
    {
        
    }

    public void StartLevel() {
        SceneManager.LoadScene("Level");
    }

    void Update()
    {
        
    }
}
