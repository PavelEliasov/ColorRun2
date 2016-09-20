using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ItemManager))]
[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(GameManager))]

public class Managers : MonoBehaviour {
   // UnityEngine.Object[] amanagers;
   
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
    public static GameManager _gameManager; //{ get;private set; }
    public static ItemManager _itemManager;

    List<IGameManager> managers;
    // Use this for initialization
    void Reset() {

        Debug.Log("Reset");
    }
    void Awake() {
        //   amanagers = new List<Object>();
        //   
        if (FindObjectsOfType<Managers>().Length > 1) {
            this.gameObject.SetActive(false);
            this.gameObject.GetComponent<GameManager>().enabled = false;
            Destroy(this.gameObject);
           // Destroy(_gameManager);
            // amanagers.Add(FindObjectsOfType<Managers>()[0]);
            //if (FindObjectsOfType<Managers>()[0].gameObject.GetHashCode() != this.gameObject.GetHashCode()) {
            //    Destroy(FindObjectsOfType<Managers>()[1].gameObject);
            //}
            //else {
            //    Destroy(FindObjectsOfType<Managers>()[0].gameObject);
            //}
            // Destroy(FindObjectsOfType<Managers>()[0].gameObject);
            //  Debug.Log(this.gameObject.GetHashCode());
            //  Debug.Log(FindObjectsOfType<Managers>()[0].gameObject.GetHashCode());
            //  Debug.Log(FindObjectsOfType<Managers>()[1].gameObject.GetHashCode());

            // FindObjectsOfType<Managers>();
            //  Destroy(this.gameObject);
        }
        else {
            managers = new List<IGameManager>();
            _gameManager = GetComponent<GameManager>();
            _audioManager = GetComponent<AudioManager>();
            _itemManager = GetComponent<ItemManager>();
            //  Debug.Log(_gameManager.LevelsComplete);
            // Debug.Log(_audioManager);

            managers.Add(_audioManager);
            StartCoroutine(StartUpManagers());
            DontDestroyOnLoad(this.gameObject);

        }
     

        Debug.Log(_audioManager);
        Debug.Log(_gameManager);
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
	//void Start () {

 //       // Debug.Log("Start");

 //     // Debug.Log(_gameManager.LevelsComplete);
	//}
    void OnEnable() {
     //  Debug.Log(_gameManager.LevelsComplete);
    }
    void OnDestroy() {
       // Debug.Log(_gameManager.LevelsComplete);
       // Debug.Log("DestroyManager");
    }
	
	// Update is called once per frame
	//void Update () {
	
	//}

  
}
