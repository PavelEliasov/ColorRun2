using UnityEngine;
using System.Collections;
using DG.Tweening;
[RequireComponent (typeof(AudioSource))]
public class PortalScript : MonoBehaviour {
    Transform _transform;
    AudioSource _audiosource;
    [SerializeField]
    AudioClip _endofLevel;
	// Use this for initialization
	void Start () {
        _transform = GetComponent<Transform>();
        _audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisablePortal() {
        _transform.DOScale(Vector3.zero, 2f);
        _audiosource.PlayOneShot(_endofLevel,Managers._audioManager.SoundEffectVolume*0.8f);
       
    }
}
