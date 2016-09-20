using UnityEngine;
using System.Collections;

public class EnableJetpack : MonoBehaviour {

    [SerializeField]
    GameObject _jetPack;
    MovePlayer _player;
    AudioSource _audioSource;
    public AudioClip jetPackDressOn;
    // Use this for initialization
    void Start () {
        _player = GetComponent<MovePlayer>();
        _audioSource = GetComponent<AudioSource>();
        _jetPack.SetActive(false);
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag=="JetPack") {
             _audioSource.PlayOneShot(jetPackDressOn, Managers._audioManager.SoundEffectVolume);
            other.gameObject.SetActive(false);
            _jetPack.SetActive(true);
            _player._jumpState = MovePlayer.State.JetPack;
        }

    }

   
}
