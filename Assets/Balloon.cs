using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balloon : MonoBehaviour
{
    [SerializeField] private Vector3 force;
    private Rigidbody2D balloon;

    // Start is called before the first frame update
    void Start()
    {
        balloon = GetComponent<Rigidbody2D>();
        force = new Vector3(-Random.Range(100, 200), Random.Range(2000, 4000), 0);
        // force = new Vector3(0, 0, 0);
        
        Image image = GetComponent<Image>();
        Color32 color = new Color32(
            ( byte )Random.Range(100, 200),
            ( byte )Random.Range(100, 200),
            ( byte )Random.Range(100, 200),
            ( byte ) 255);
        
        Debug.Log(color);

        image.color = color;

        balloon.AddForce(force);

        transform.position = new Vector3(Random.Range(20, Screen.width-20), -100, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
