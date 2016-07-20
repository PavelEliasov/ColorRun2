using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameManager : MonoBehaviour {


    int _totalBanks;
    int _spentBanks;
    int _receivedBanks;

    public int _levelsComplete;
    public int sceneNumber;//The number of last unload scene for position of slide
    public int _selectedScene;
    public int LevelsComplete {
        get {
            return _levelsComplete;
        }
        set {
            if (value>=_levelsComplete) {
                _levelsComplete = value;
            }
        }
    }

    public int TotalBanks {
        get  {
            return _totalBanks;
        }

        set  {
            _totalBanks = value;
        }
    }
    public int SpentBanks   {
        get    {
            return _spentBanks;
        }

        set {
            _spentBanks = value;
        }
    }

    public int ReceivedBanks  {
        get {
            return _receivedBanks;
        }

        set {
            _receivedBanks = value;
        }
    }

 



    //public Dictionary<int, Statistic> Statistics  {
    //    get {
    //        return statistics;
    //    }

    //   private set {
    //        statistics = value;
    //    }
    //}
    [SerializeField]
    public  Dictionary<int, Statistic> statistics = new Dictionary<int, Statistic>();

    //[SerializeField]
    //public static ItemManager _items;



    [SerializeField]
     List<Statistic> test;

    Serializer<int,Statistic> serial;

    void Awake() {
        

      //  Managers._itemManager.Boots = true;
        Debug.Log(JsonUtility.ToJson(Managers._itemManager));
        serial = new Serializer<int, Statistic>();
        if (serial.Deserialize().Count>0) {
            statistics = serial.Deserialize();
        }
        _levelsComplete = PlayerPrefs.GetInt("LevelComplete");
        CalculateTotalSumOfBanks();

    }

    void OnLevelWasLoaded() {
        if (SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "MainMenu") {
            Debug.Log(JsonUtility.ToJson(Managers._itemManager));

            //  ItemManager.Instance=JsonUtility.FromJson<ItemManager>(JsonUtility.ToJson(ItemManager.Instance));
            //Debug.Log(JsonUtility.ToJson(ItemManager.Instance));
            // Debug.Log(_items);
        }
        // Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name=="Menu") {
            CalculateTotalSumOfBanks();
            Debug.Log("Menu");
        }

    }
	// Use this for initialization
	void Start () {
      //  var serial = new Serializer<int, Statistic>();
        // statistics.Add(1, new Statistic() { Banks=5});
        // statistics.Add(2, new Statistic());
        // statistics.Add(3, new Statistic());
    //    serial.SerializeDictionary(statistics);
        //// statistics.Clear();

      //  Debug.Log(serial.Deserialize().Count);
        //statistics =  serial.Deserialize();

       // Debug.Log(statistics[1].Banks);
       // test = new List<Statistic>();
       // Statistic  st = new Statistic();
       // st.Banks = 10;
       // st.Stars = 10;
       // st.Time = 10;
       // test.Add(st);
       
      //  Debug.Log(JsonUtility.ToJson(test));
       // string a = JsonUtility.ToJson(st);
       // Debug.Log(a);
	}

    public void Stats(int scene,Statistic stats) {
        _levelsComplete = scene;
       
        if (statistics.ContainsKey(scene)) {
            if (stats.Banks>statistics[scene].Banks) {
                statistics[scene].Banks = stats.Banks;
            }
            if (stats.Time < statistics[scene].Time) {
                statistics[scene].Time = stats.Time;
            }
            if (stats.Stars >statistics[scene].Stars) {
                statistics[scene].Stars = stats.Stars;

                Debug.Log(stats.Stars);
            }
            // statistics[scene] = stats;

        }  else {
            statistics.Add(scene,stats);
        }

        serial.SerializeDictionary(statistics);
        PlayerPrefs.SetInt("LevelComplete",_levelsComplete);
      //  Debug.Log(JsonUtility.ToJson(statistics));
       // Debug.Log(statistics[1].Banks);
    } 
	// Update is called once per frame
	void Update () {
        if (statistics.ContainsKey(1)) {
           // Debug.Log(statistics[1].Stars);
        }
         // Debug.Log(_selectedScene);
    }

    void CalculateTotalSumOfBanks() {
        int sum = 0;
        foreach (var stat in statistics) {
            sum += stat.Value.Banks;
        }
        sum = sum - _spentBanks + _receivedBanks;
        _totalBanks = sum;

        Debug.Log(_totalBanks);
    }
}
