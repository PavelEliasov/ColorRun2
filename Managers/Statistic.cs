using UnityEngine;
using System.Collections;

[System.Serializable]
public class Statistic  {
    [SerializeField]
    float _time;
    [SerializeField]
    float _bank;
    [SerializeField]
    float _stars;
   
    [SerializeField]
    public float Time {
        get {
            return _time;
        }

        set  {
            _time = value;
        }
    }

    public float Banks  {
        get  {
            return _bank;
        }

        set {
            _bank = value;
        }
    }

    public float Stars   {
        get {
            return _stars;
        }

        set {
            _stars = value;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
