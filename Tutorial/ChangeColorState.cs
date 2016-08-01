using UnityEngine;
using System.Collections;

public class ChangeColorState : MonoBehaviour {
    MovePlayer _player;
	// Use this for initialization
	void Start () {
        _player = FindObjectOfType<MovePlayer>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag=="Player") {
            _player._pallette = MovePlayer.Pallette.YR;
        }
    }
}
