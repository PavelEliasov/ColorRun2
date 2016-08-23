using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour,IGameManager {
    public  AudioSource _audioSource;
    public ManagerStatus status  { get; set; }
    [SerializeField]
    float _musicVolume;
    [SerializeField]
    bool _musicOff;
    [SerializeField]
    float _soundEffectVolume;
    [SerializeField]
    bool _soundEffectOff;


    AudioClip defaultClip;

    public float SoundEffectVolume {
        get {
            return _soundEffectVolume; }
        set {
            _soundEffectVolume = value;

            AudioListener.volume = _soundEffectVolume;
           
        }
        
    }
    public bool SoundEffectOff {
        get {
            return _soundEffectOff; }
        set {
            AudioListener.pause = value;
            _soundEffectOff=AudioListener.pause;
        }
    }

    public float MusicVolume {
        get { return _musicVolume; }
        set {
            _musicVolume = value;
            if (_audioSource==null) {
                _audioSource = GetComponent<AudioSource>();
            }
            _audioSource.volume = _musicVolume;
        }
    }

    public bool MusicOff   {
        get {
            return _musicOff;
        }
        set {
            _audioSource.mute =value;
            _musicOff = _audioSource.mute;
        }
    }

    public void Startup() {
        status = ManagerStatus.Started;
    }

    // Use this for initialization
    void Start () {
        
        _audioSource = GetComponent<AudioSource>();

        Debug.Log("sdfsdfsdg");
        defaultClip = _audioSource.clip;
        _audioSource.ignoreListenerVolume=true;
        _audioSource.ignoreListenerPause = true;
        _audioSource.volume = MusicVolume;
        _audioSource.mute = _musicOff;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnLevelWasLoaded() {

        Debug.Log(SceneManager.GetActiveScene().name) ;

        if (SceneManager.GetActiveScene().name == "Market" || SceneManager.GetActiveScene().name == "Equipment"
            || SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Settings") {
            if (_audioSource.clip != defaultClip) {

                PlayMusic(defaultClip);
            }
        }
        else {
          //  _audioSource.enabled = false;
        }

       // _audioSource.volume = _musicVolume;
    
    }

    public void PlayMusic(AudioClip clip) {

        _audioSource.clip=clip;
        _audioSource.Play();


    }


    //public void Music(float volume) {
      
    //    MusicVolume = volume;
    //    _audioSource.volume = MusicVolume;
    //} 
 
}
