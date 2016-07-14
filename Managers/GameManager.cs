using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
   public int _levelsComplete;
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

    Dictionary<int, Statistic> statistics = new Dictionary<int, Statistic>();
   

   
	// Use this for initialization
	void Start () {
	
	}

    public void Stats(int scene,Statistic stats) {
        if (statistics.ContainsKey(scene)) {
            if (stats.Stars>statistics[scene].Stars) {
                statistics[scene].Stars = stats.Stars;
            }
            if (stats.Time < statistics[scene].Time) {
                statistics[scene].Time = stats.Time;
            }
            if (stats.Score > statistics[scene].Score) {
                statistics[scene].Score = stats.Score;
            }
           // statistics[scene] = stats;

        }  else {
            statistics.Add(scene,stats);
        }

    } 
	// Update is called once per frame
	void Update () {

      //  Debug.Log(_levelsComplete);
	}
}
