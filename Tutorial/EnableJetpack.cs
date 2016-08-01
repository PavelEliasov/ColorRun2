using UnityEngine;
using System.Collections;

public class EnableJetpack : MonoBehaviour {

    [SerializeField]
    GameObject _jetPack;
    MovePlayer _player;
	// Use this for initialization
	void Start () {
        _player = GetComponent<MovePlayer>();
        _jetPack.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag=="JetPack") {
            other.gameObject.SetActive(false);
            _jetPack.SetActive(true);
            _player._jumpState = MovePlayer.State.JetPack;
        }

    }
}
