using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadScene() {
        Managers._audioManager.SoundEffectVolume = 0.9f;
        SceneManager.LoadScene("1");
    }
    public void TestClick() {

        Debug.Log("Click");
    }
}
