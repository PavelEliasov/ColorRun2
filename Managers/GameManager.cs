using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameManager : MonoBehaviour {
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

    [SerializeField]
     List<Statistic> test;

	// Use this for initialization
	void Start () {
        var serial = new Serializer<int, Statistic>();
        statistics.Add(1, new Statistic() { Banks=5});
        statistics.Add(2, new Statistic());
        statistics.Add(3, new Statistic());
        serial.SerializeDictionary(statistics);
       // statistics.Clear();
        statistics =  serial.Deserialize();

        Debug.Log(statistics[1].Banks);
        test = new List<Statistic>();
        Statistic  st = new Statistic();
        st.Banks = 10;
        st.Stars = 10;
        st.Time = 10;
        test.Add(st);
       
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
            //if (stats.Score > statistics[scene].Score) {
            //    statistics[scene].Score = stats.Score;
            //}
           // statistics[scene] = stats;

        }  else {
            statistics.Add(scene,stats);
        }
        Debug.Log(JsonUtility.ToJson(statistics));
       // Debug.Log(statistics[1].Banks);
    } 
	// Update is called once per frame
	void Update () {
        if (statistics.ContainsKey(1)) {
           // Debug.Log(statistics[1].Stars);
        }
         // Debug.Log(_selectedScene);
    }
}
