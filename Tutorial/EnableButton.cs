using UnityEngine;
using System.Collections;

public class EnableButton : MonoBehaviour {
    [SerializeField]
    GameObject _pinkButton;
    [SerializeField]
    GameObject _yelloowButton;

    MovePlayer _player;

    void Awake() {
        _player = FindObjectOfType<MovePlayer>();
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag=="Player") {

            if (_player.color==Colors.Red) {
                _pinkButton.SetActive(false);
                _pinkButton.SetActive(true);
            }
            if (_player.color == Colors.Yellow) {
                _yelloowButton.SetActive(false);
                _yelloowButton.SetActive(true);
            }

        }

    }
}
