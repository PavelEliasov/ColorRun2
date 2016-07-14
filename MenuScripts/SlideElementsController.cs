using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlideElementsController : MonoBehaviour {
    
    [SerializeField]
    int unlockLimit;
    [SerializeField]
    GameObject playButton;
    [SerializeField]
    Image _lock;
	// Use this for initialization
	void Start () {

     //   Debug.Log(Managers._gameManager.LevelsComplete);
        playButton.SetActive(false);
        if (Managers._gameManager.LevelsComplete==unlockLimit) {
            _lock.enabled =false;
            playButton.SetActive(true);

        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
