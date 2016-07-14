using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour {


    void Awake() {
        SceneManager.LoadScene("Menu");
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
