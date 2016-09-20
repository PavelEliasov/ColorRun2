using UnityEngine;
using System.Collections;

[System.Serializable]
public class Statistic  {
    [SerializeField]
    float _time;
    [SerializeField]
    int _bank;
    [SerializeField]
    int _stars;
   
    [SerializeField]
    public float Time {
        get {
            return _time;
        }

        set  {
            _time = value;
        }
    }

    public int Banks  {
        get  {
            return _bank;
        }

        set {
            _bank = value;
        }
    }

    public int Stars   {
        get {
            return _stars;
        }

        set {
            _stars = value;
        }
    }

    // Use this for initialization
 //   void Start () {
	
	//}
	
	// Update is called once per frame
	//void Update () {
	
	//}
}
