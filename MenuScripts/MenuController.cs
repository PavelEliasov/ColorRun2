﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour {
    [SerializeField]
    Text totalBanks;

    [SerializeField]
    GameObject loadPanel;

    [SerializeField]
    GameObject[] dots;

    [SerializeField]
    Text Time;

    AudioSource audiosource;
    [SerializeField]
    AudioClip click;
    [SerializeField]
    AudioClip _slide;

    float volume;

    [SerializeField]
    TextAsset textAsset;
    string[] lines; 


    public string this[int index] {
        set {
            lines[index] = value;
        }

        get {
            if (index > lines.Length - 1) {
                return "";
            }
            else {
                return lines[index];
            }
           
        }
    }

   void Awake() {


        lines = textAsset.text.Split('\n');

    }
	// Use this for initialization
	void Start () {
        if (Screen.width > 1920) {
            EventSystem.current.pixelDragThreshold = 20;
        }
        foreach (string l in lines) {

            Debug.Log(l);
        }
        UnityEngine.Time.timeScale = 1;
        totalBanks.text = "X " + Managers._gameManager.TotalBanks.ToString();
        if (Managers._gameManager.statistics.ContainsKey(1) && Managers._gameManager.statistics[1]!=null) {
            Time.text = Managers._gameManager.statistics[1].Time.ToString();
        }
        audiosource = GetComponent<AudioSource>();
        volume = Managers._audioManager.SoundEffectVolume;
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

    public void LoadScene() {
        // Managers._audioManager.SoundEffectVolume = 0.9f;
        audiosource.PlayOneShot(click,volume);
        StartCoroutine(LoadSceneAsyn(Managers._gameManager._selectedScene.ToString()));
        //SceneManager.LoadScene(Managers._gameManager._selectedScene.ToString());
    }
    public void TestClick() {

        audiosource.PlayOneShot(_slide,volume*0.9f);
    }
    public void ResetStats() {
        PlayerPrefs.SetInt("LevelComplete", -1);
        PlayerPrefs.SetInt("TotalBanks", 0);
        PlayerPrefs.SetInt("SpentBanks", 0);
        PlayerPrefs.SetInt("RecievedBanks", 0);
        PlayerPrefs.SetString("DicKey",null);
        PlayerPrefs.SetString("DicValue", null);
        PlayerPrefs.SetString("ItemManager", null);
        PlayerPrefs.SetString("AudioManager",null);
    }


    IEnumerator LoadSceneAsyn( string scene) {
        loadPanel.SetActive(true);
        StartCoroutine(DotAnimate());
        yield return null;
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;
        while (!ao.isDone) {
            if (Mathf.Approximately(ao.progress,0.9f)) {
                ao.allowSceneActivation = true;
            }
            yield return null;
        }

    }

    IEnumerator DotAnimate() {
        int dotCount = 0;
        yield return null;
        while (true) {
            if (dotCount>=dots.Length) {
                dotCount = 0;
                foreach (GameObject dot in dots) {
                    dot.SetActive(false);
                }
            }
            dots[dotCount].SetActive(true);
            dotCount++;
            yield return new WaitForSeconds(0.1f);
        }


    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToMovieScene() {
        SceneManager.LoadScene("movie");
    }
}
