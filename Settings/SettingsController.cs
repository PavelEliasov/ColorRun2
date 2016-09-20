using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour {
    [SerializeField]
    Slider musicVolumeSlider;
    [SerializeField]
    Slider effectsVolumeSlider;

    [SerializeField]
    Toggle _MusicOff;
    [SerializeField]
    Toggle _EffectsOff;
    // Use this for initialization
    void Start () {
        musicVolumeSlider.value = Managers._audioManager.MusicVolume;
        effectsVolumeSlider.value = Managers._audioManager.SoundEffectVolume;
        _MusicOff.isOn = Managers._audioManager.MusicOff;
        _EffectsOff.isOn = Managers._audioManager.SoundEffectOff;
        //_MusicOff.onValueChanged.AddListener(MusicOff) ;
    }
	
	// Update is called once per frame
	//void Update () {
	
	//}

    public void MusicVolume() {
        Managers._audioManager.MusicVolume = musicVolumeSlider.value;
       // Managers._audioManager._audioSource.volume = musicVolumeSlider.value;
       // Debug.Log(volume);
    }
    public void EffectsVolume() {
        Managers._audioManager.SoundEffectVolume =effectsVolumeSlider.value;
        //Managers._audioManager._audioSource.volume = musicVolumeSlider.value;
        // Debug.Log(volume);
    }

    public void MusicOff() {
        Managers._audioManager.MusicOff = _MusicOff.isOn;

      //  Debug.Log("toggle");
    }

    public void EffectsOff() {
        Managers._audioManager.SoundEffectOff = _EffectsOff.isOn;

        //  Debug.Log("toggle");
    }

    public void MainMenu8Button() {
        SceneManager.LoadScene("MainMenu");

    }
}
