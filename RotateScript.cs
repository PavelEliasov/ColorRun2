using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {
    Transform _trans;

    public float speed;
	// Use this for initialization
	void Start () {
        _trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        _trans.Rotate(0,Time.deltaTime*speed,0);
	}
}
