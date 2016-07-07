using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour,IGameManager {
    public ManagerStatus status  { get; set; }
    float _musicVolume;
    bool _musicOff;
    public float SoundEffectVolume {
        get { return AudioListener.volume; }
        set {  AudioListener.volume = value; }
        
    }
    public bool SoundEffectOff {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    public float MusicVolume {
        get { return _musicVolume; }
        set { _musicVolume = value; }
    }

    public bool MusicOff   {
        get { return _musicOff; }
        set { _musicOff = value; }
    }

    public void Startup() {
        status = ManagerStatus.Started;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
