using UnityEngine;
using System.Collections;


public class DieElement : MonoBehaviour {
    Rigidbody _rigidBody;
	// Use this for initialization
	void Start () {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void EnableRigidbody() {
        _rigidBody.isKinematic = false;
    }
}
