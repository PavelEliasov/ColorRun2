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
    Text banks;
    [SerializeField]
    Image firstStar;
    [SerializeField]
    Image secondStar;
    [SerializeField]
    Image thirdStar;

    bool interactable;
    // Use this for initialization
    void Start () {

        //  Debug.Log(Managers._gameManager.statistics[1].Time);

      //  Debug.Log(sceneNumber);
        DisableElements();
        
        if (Managers._gameManager.LevelsComplete>=unlockLimit) {
            _lock.enabled =false;
            EnableElements();

            Debug.Log(Managers._gameManager.statistics.ContainsKey(sceneNumber));
            if (Managers._gameManager.statistics.ContainsKey(sceneNumber)) {

                Debug.Log(Managers._gameManager.statistics[sceneNumber].Stars);
                Debug.Log(Managers._gameManager.statistics[sceneNumber].Time);
                banks.text ="X"+" "+ Managers._gameManager.statistics[sceneNumber].Banks.ToString();
                time.text="Time: "+ Managers._gameManager.statistics[sceneNumber].Time.ToString();
                switch (Managers._gameManager.statistics[sceneNumber].Stars) {
                  
                    case 1:
                        firstStar.gameObject.SetActive(true);
                        break;
                    case 2:
                        firstStar.gameObject.SetActive(true);
                        secondStar.gameObject.SetActive(true);
                        break;
                    case 3:
                        firstStar.gameObject.SetActive(true);
                        secondStar.gameObject.SetActive(true);
                        thirdStar.gameObject.SetActive(true);
                        break;
                }

            }

        }
	}

    void DisableElements() {
        playButton.SetActive(false);
        playButton.GetComponent<Button>().interactable = false;
        interactable = false;
        time.gameObject.SetActive(false);
        banks.gameObject.SetActive(false);
        firstStar.gameObject.SetActive(false);
        secondStar.gameObject.SetActive(false);
        thirdStar.gameObject.SetActive(false);
    }
    void EnableElements() {
        playButton.SetActive(true);
        time.gameObject.SetActive(true);
        banks.gameObject.SetActive(true);
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
