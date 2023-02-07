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

    public List<Sprite> stickers;
    public List<AudioClip> stickersAudios;
    public delegate void PopAction(Balloon balloon);
    public static event PopAction OnPop;
    public int offset = 0;
    public TMP_Text label;

    private static new UnityEngine.Camera camera;
    private static AudioSource popAudio;
    private static string[] stickerTriggers = {"rain_start","sun_shine","thunder_start","star_twinkle"};
    private static string popTrigger = "balloon_pop";
    private static int balloonCounter = 0;
    
    private Animator popAnimator;
    private Vector3 force;
    private Rigidbody2D balloon;
    private SpriteRenderer stickerRenderer;
    private static int stickerIndex = -1;

    private int[,] colors = new int[22, 4] {
        {255,0,0,255}, {255,192,0,255}, {255,252,0,255}, {255,0,0,255}, {0,255,255,255}, {255,0,0,255},
        {255,53,94,255}, {253,91,120,255}, {255,96,55,255}, {255,153,102,255}, {255,153,51,255},
        {255,204,51,255}, {255,255,102,255}, {255,255,102,255}, {204,255,0,255}, {102,255,102,255},
        {170,240,209,255}, {80,191,230,255}, {255,110,255,255}, {238,52,210,255}, {255,0,204,255}, {255,0,204,255}
    };

    void Start() {
        camera = UnityEngine.Camera.main;
        popAudio = GetComponent<AudioSource>();

        popAnimator=GetComponent<Animator>();
        balloon = GetComponent<Rigidbody2D>();
        stickerRenderer = balloon.GetComponentsInChildren<SpriteRenderer>()[1];
    
        force = new Vector3(Random.Range(-2, 2), Random.Range(80, 150), 0);

        
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        int randomColorIndex = Random.Range(0, colors.GetLength(0)-1);
        Color32 color = new Color32(
            ( byte )colors[randomColorIndex, 0],
            ( byte )colors[randomColorIndex, 1],
            ( byte )colors[randomColorIndex, 2],
            ( byte )colors[randomColorIndex, 3]);
        
        sprite.color = color;

        balloon.AddForce(force);

        List<string> labels = LevelController.levelLabels[LevelSelectorButton.selectedLevel];

        label = balloon.GetComponentInChildren<TMP_Text>();

        if(balloonCounter == 0 || balloonCounter == 3) {
            label.text = labels[Random.Range(offset, 5 + offset)];
        }
        else if(balloonCounter == 1 || balloonCounter == 4) {
            label.text = "";
            stickerIndex = (stickerIndex + 1) % stickers.Count;
            stickerRenderer.sprite = stickers[stickerIndex];
            Debug.Log(stickerIndex);
            Debug.Log(stickers[stickerIndex]);
            Debug.Log(stickerRenderer.sprite);
            stickerRenderer.enabled = true;
        }
        else {
            label.text = labels[offset];
        }

        transform.position = new Vector3(Random.Range(-8f, 8f), -6, 0);

        balloonCounter = (balloonCounter + 1) % 5;
    }

    void Update() {
        Vector3 position = camera.WorldToViewportPoint(transform.position);
        if (position.y >= 1.5) {
            DestroyAfterPop();
        }
    }

    public void OnMouseDown() {
        if (Time.timeScale == 1 || !EventSystem.current.IsPointerOverGameObject()) {
            if (OnPop != null) {
                OnPop(this);
            }
            popAnimator.SetTrigger(popTrigger);
            popAudio.Play();
            
            if(label.text == "") {
                AudioSource audioSource = GameObject.Find("Controller").AddComponent<AudioSource>();
                audioSource.clip = stickersAudios[stickerIndex];
                popAnimator.SetTrigger(stickerTriggers[stickerIndex]);
                audioSource.Play();
            }
        }
    }

    public void DestroyAfterPop() {
        Destroy(gameObject);
    }
}
