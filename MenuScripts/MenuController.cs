using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    [SerializeField]
    Text totalBanks;
	// Use this for initialization
	void Start () {
        totalBanks.text = "X " + Managers._gameManager.TotalBanks.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadScene() {
        Managers._audioManager.SoundEffectVolume = 0.9f;
        SceneManager.LoadScene(Managers._gameManager._selectedScene.ToString());
    }
    public void TestClick() {

        Debug.Log("Click");
    }
    public void ResetStats() {
        PlayerPrefs.SetInt("LevelComplete", -1);
        PlayerPrefs.SetInt("SpentBanks", 0);
        PlayerPrefs.SetString("DicKey",null);
        PlayerPrefs.SetString("DicValue", null);
        PlayerPrefs.SetString("ItemManager", null);
    }


}
