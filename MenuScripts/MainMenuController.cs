using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayButton() {
        StartCoroutine(Loader("Menu"));
       // SceneManager.LoadScene("Menu");
    }
    public void MarketButton() {
        StartCoroutine(Loader("Market"));
       // SceneManager.LoadScene("Market");
    }
    public void EquipmentButton() {
        StartCoroutine(Loader("Equipment"));
        //SceneManager.LoadScene("Equipment");

    }
    public void SettingsButton() {
        StartCoroutine(Loader("Settings"));
        //SceneManager.LoadScene("Equipment");

    }

    IEnumerator Loader(string sceneName) {
        yield return new WaitForSeconds(0.09f);
        SceneManager.LoadScene(sceneName);
    }
}
