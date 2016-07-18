using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlideElementsController : MonoBehaviour {
    [SerializeField]
    int sceneNumber;
    [SerializeField]
    int unlockLimit;
    [SerializeField]
    GameObject playButton;
    [SerializeField]
    Image _lock;
    [SerializeField]
    Text time;
    [SerializeField]
    Text stars;

    bool interactable;
    // Use this for initialization
    void Start () {

        // Debug.Log(Managers._gameManager.LevelsComplete);

        DisableElements();
        
        if (Managers._gameManager.LevelsComplete>=unlockLimit) {
            _lock.enabled =false;
            EnableElements();

            Debug.Log(Managers._gameManager.statistics.ContainsKey(sceneNumber));
            if (Managers._gameManager.statistics.ContainsKey(sceneNumber)) {

             //   Debug.Log(Managers._gameManager.statistics[sceneNumber].Stars);
               stars.text ="X"+" "+ Managers._gameManager.statistics[sceneNumber].Banks.ToString();
                time.text="Time: "+ Managers._gameManager.statistics[sceneNumber].Time.ToString();
            }

        }
	}

    void DisableElements() {
        playButton.SetActive(false);
        playButton.GetComponent<Button>().interactable = false;
        interactable = false;
        time.gameObject.SetActive(false);
        stars.gameObject.SetActive(false);
    }
    void EnableElements() {
        playButton.SetActive(true);
        time.gameObject.SetActive(true);
        stars.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (sceneNumber == Managers._gameManager._selectedScene && interactable==false ) {
            playButton.GetComponent<Button>().interactable = true;
            interactable = true;
           // interacteble = true;
        }
        if (sceneNumber != Managers._gameManager._selectedScene && interactable==true) {
           playButton.GetComponent<Button>().interactable = false;
            interactable = false;
           // interacteble = false;
        }
	}
}
