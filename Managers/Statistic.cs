using UnityEngine;
using System.Collections;

public class Statistic : MonoBehaviour {
    float _time;
    float _stars;
    float _score;
    public float Time {
        get {
            return _time;
        }

        set  {
            _time = value;
        }
    }

    public float Stars  {
        get  {
            return _stars;
        }

        set {
            _stars = value;
        }
    }

    public float Score   {
        get {
            return _score;
        }

        set {
            _score = value;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
