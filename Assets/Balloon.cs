using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class Balloon : MonoBehaviour, IPointerClickHandler
{
    public delegate void PopAction(string label);
    public static event PopAction OnPop;
    public string type = "A";
    public int offset = 0;
    private Vector3 force;
    private Rigidbody2D balloon;
    private TMP_Text label;

    void Start() {
        balloon = GetComponent<Rigidbody2D>();
        force = new Vector3(Random.Range(-2, 2), Random.Range(20, 40), 0);
        
        Image image = GetComponent<Image>();
        Color32 color = new Color32(
            ( byte )Random.Range(100, 200),
            ( byte )Random.Range(100, 200),
            ( byte )Random.Range(100, 200),
            ( byte ) 255);
        
        image.color = color;

        balloon.AddForce(force);

        label = balloon.GetComponentInChildren<TMP_Text>();
        label.text = ((char)Random.Range(65 + offset, 70 + offset)).ToString();

        Canvas canvas = FindObjectOfType<Canvas>();
        float width = canvas.GetComponent<RectTransform>().rect.width;
        transform.position = new Vector3(Random.Range(-8, 8), -6, 0);
    }

    void Update() {
        
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (OnPop != null) {
            OnPop(label.text);
        }
        Destroy(gameObject);
    }
}
