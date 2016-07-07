using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioManager))]
public class Managers : MonoBehaviour {
    //static Managers _instance;
    //public static Managers Instance {
    //    get {
    //        if (_instance==null) {
    //            var container = FindObjectOfType<Managers>();
    //            _instance = container;
    //            if (_instance==null) {
    //                _instance = new GameObject().AddComponent<Managers>();
    //            }
    //        }
    //        return _instance;
    //    }

    //}


    public static AudioManager _audioManager { get; private set; }



    List<IGameManager> managers;
    // Use this for initialization

    void Awake() {
        if (FindObjectsOfType<Managers>().Length>1) {
            Destroy(this.gameObject);
        }
        managers = new List<IGameManager>();
        _audioManager = GetComponent<AudioManager>();

       // Debug.Log(_audioManager);

        managers.Add(_audioManager);
        StartCoroutine(StartUpManagers());
        DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator StartUpManagers() {
        foreach (IGameManager manager in managers) {
            manager.Startup();
        }
        yield return null;

        int startedManagerscount = 0;

        while (startedManagerscount<managers.Count) {
            foreach (IGameManager manager in managers) {
                if (manager.status==ManagerStatus.Started) {
                    startedManagerscount++;
                }
            }
            
        }
        Debug.Log(startedManagerscount+"/"+managers.Count + " managers started");

    }
	void Start () {

       // Debug.Log("Start");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadScene() {
        _audioManager.SoundEffectVolume = 0.9f;
        SceneManager.LoadScene("1");
    }
}
