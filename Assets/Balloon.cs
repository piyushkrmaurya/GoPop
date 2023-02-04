using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Audio;
using TMPro;

public class Balloon : MonoBehaviour
{

    public delegate void PopAction(string label);
    public static event PopAction OnPop;
    public string type = "A";
    public int offset = 0;

    private static new UnityEngine.Camera camera;
    private static AudioSource popAudio;
    private static string popTrigger="balloon_pop";
    
    private Animator popAnimator;
    private Vector3 force;
    private Rigidbody2D balloon;
    private TMP_Text label;

    private int[,] colors = new int[6, 4] {
        {255,0,0,255}, {255,192,0,255}, {255,252,0,255}, {255,0,0,255}, {0,255,255,255}, {255,0,0,255}
    };

    void Start() {
        camera = UnityEngine.Camera.main;
        popAudio = GetComponent<AudioSource>();

        popAnimator=GetComponent<Animator>();
        balloon = GetComponent<Rigidbody2D>();
    
        force = new Vector3(Random.Range(-2, 2), Random.Range(50, 150), 0);

        
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        int randomColorIndex = Random.Range(0, colors.GetLength(0)-1);
        Color32 color = new Color32(
            ( byte )colors[randomColorIndex, 0],
            ( byte )colors[randomColorIndex, 1],
            ( byte )colors[randomColorIndex, 2],
            ( byte )colors[randomColorIndex, 3]);
        
        sprite.color = color;

        balloon.AddForce(force);

        label = balloon.GetComponentInChildren<TMP_Text>();
        label.text = ((char)Random.Range(65 + offset, 70 + offset)).ToString();

        transform.position = new Vector3(Random.Range(-8f, 8f), -6, 0);

    }

    void Update() {
        Vector3 position = camera.WorldToViewportPoint(transform.position);
        if (position.y >= 1.5) {
            DestroyAfterPop();
        }
    }

    public void OnMouseDown() {
        if (Time.timeScale == 1 || !EventSystem.current.IsPointerOverGameObject()) {
            popAnimator.SetTrigger(popTrigger);
            popAudio.Play();
            if (OnPop != null) {
                OnPop(label.text);
            }
        }
    }

    public void DestroyAfterPop() {
        Destroy(gameObject);
    }
}
