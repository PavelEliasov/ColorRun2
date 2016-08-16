using UnityEngine;
using System.Collections;

public class DieTrigger : MonoBehaviour {
    SceneController _controller;
	// Use this for initialization
	void Start () {
        _controller = FindObjectOfType<SceneController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag=="Player" || other.gameObject.tag=="PlayerDamaged") {
            _controller.Die();
        }

    }
}
