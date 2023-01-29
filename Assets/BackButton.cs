using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void OnMouseDown() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
