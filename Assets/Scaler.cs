using UnityEngine;
using UnityEngine.UI;

public class Scaler : MonoBehaviour {

    void Start() {
        transform.localScale *= Screen.width / 800;
    }
}