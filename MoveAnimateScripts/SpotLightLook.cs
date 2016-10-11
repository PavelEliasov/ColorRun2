using UnityEngine;
using System.Collections;

public class SpotLightLook : MonoBehaviour {
    Transform _trans;
    [SerializeField]
    Transform _boyTrans;
	// Use this for initialization
	void Start () {
        _trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        _trans.LookAt(_boyTrans);
	}
}
