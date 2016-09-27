using UnityEngine;
using System.Collections;

public class AngleDetector : MonoBehaviour {
    Transform platformTrans;
	// Use this for initialization
	void Start () {
        platformTrans = GetComponent<Transform>();

      //  Debug.Log(platformTrans.localRotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {

    }
}
