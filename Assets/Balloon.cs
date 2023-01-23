using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Balloon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Vector3 force;
    private Rigidbody2D balloon;
    private TMP_Text label;

    void Start()
    {
        balloon = GetComponent<Rigidbody2D>();
        force = new Vector3(Random.Range(-200, 200), Random.Range(2000, 4000), 0);
        
        Image image = GetComponent<Image>();
        Color32 color = new Color32(
            ( byte )Random.Range(100, 200),
            ( byte )Random.Range(100, 200),
            ( byte )Random.Range(100, 200),
            ( byte ) 255);
        
        image.color = color;

        balloon.AddForce(force);

        label = balloon.GetComponentInChildren<TMP_Text>();
        label.text = ((char)Random.Range(65 + LevelController.score, 70 + LevelController.score)).ToString();

        transform.position = new Vector3(Random.Range(20, Screen.width-20), -100, 0);
    }

    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        string matchCharacter = ((char)(label.text[0]-1)).ToString();
        if((label.text == "A" && LevelController.lastPopped == "") || (matchCharacter == LevelController.lastPopped)) {
            LevelController.lastPopped = label.text;
            LevelController.score++;
        }
        Text scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        scoreText.text = LevelController.score.ToString();
        Destroy(gameObject);
    }
}
