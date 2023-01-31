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
    public string popTrigger="balloon_pop";
    private Animator popAnimator;
    public delegate void PopAction(string label);
    public static event PopAction OnPop;
    public string type = "A";
    public int offset = 0;
    private Vector3 force;
    private Rigidbody2D balloon;
    public AudioSource popAudio;
    private TMP_Text label;

    void Start() {
        popAnimator=GetComponent<Animator>();
        balloon = GetComponent<Rigidbody2D>();
        force = new Vector3(Random.Range(-2, 2), Random.Range(50, 150), 0);
        
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color32 color = new Color32(
            ( byte )Random.Range(100, 200),
            ( byte )Random.Range(100, 200),
            ( byte )Random.Range(100, 200),
            ( byte ) 255);
        
        sprite.color = color;

        balloon.AddForce(force);

        label = balloon.GetComponentInChildren<TMP_Text>();
        label.text = ((char)Random.Range(65 + offset, 70 + offset)).ToString();

        transform.position = new Vector3(Random.Range(-8, 8), -6, 0);

    }

    void Update() {
        // transform.Rotate(0, 0, Random.Range(-1, 1) * 2.0f * Time.deltaTime);
    }

    public void OnMouseDown() {
        popAnimator.SetTrigger(popTrigger);
        // Debug.Log(popAudio);
        // Debug.Log(popAudio.clip);
        popAudio.Play();
        Debug.Log(popAudio.isPlaying);
        if (OnPop != null) {
            OnPop(label.text);
        }
        // Destroy(gameObject);
    }
}
