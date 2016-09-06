using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(AudioSource))]
public class MainMenuController : MonoBehaviour {
    [SerializeField]
    AudioClip click;

    AudioSource audiosource;
    float volume;
	// Use this for initialization
	void Start () {
        audiosource = GetComponent<AudioSource>();
        volume = Managers._audioManager.SoundEffectVolume;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayButton() {
        PlaySound();
        StartCoroutine(Loader("Menu"));
       // SceneManager.LoadScene("Menu");
    }
    public void MarketButton() {
        PlaySound();
        StartCoroutine(Loader("Market"));
       // SceneManager.LoadScene("Market");
    }
    public void EquipmentButton() {
        PlaySound();
        StartCoroutine(Loader("Equipment"));
        //SceneManager.LoadScene("Equipment");

    }
    public void SettingsButton() {
        PlaySound();
        StartCoroutine(Loader("Settings"));
        //SceneManager.LoadScene("Equipment");

    }

    public void Exit() {
        PlaySound();
        Application.Quit();
        //SceneManager.LoadScene("Equipment");

    }
    IEnumerator Loader(string sceneName) {
        yield return new WaitForSeconds(0.09f);
        SceneManager.LoadScene(sceneName);
    }

     void PlaySound() {
        audiosource.PlayOneShot(click,volume);
    }
}
