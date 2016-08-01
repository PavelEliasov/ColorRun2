using UnityEngine;
using System.Collections;

public class JetPackAnimate : MonoBehaviour {
    Transform jetpackTrans;
	// Use this for initialization
	void Start () {
        jetpackTrans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        jetpackTrans.Rotate(Vector3.up*Time.deltaTime*50);
	}
}
