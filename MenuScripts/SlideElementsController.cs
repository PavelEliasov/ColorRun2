using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlideElementsController : MonoBehaviour {
    public enum UnlockType {
        Default,
        Skate,
        Moto
    }
    public UnlockType _unlockType = UnlockType.Default;

    [SerializeField]
    int sceneNumber;
    [SerializeField]
    int unlockLimit;
    [SerializeField]
    GameObject playButton;
    [SerializeField]
    Image _lock;
    [SerializeField]
    Text timeSec;
    [SerializeField]
    Text timeSec2;
    [SerializeField]
    Text timeMSec;
    [SerializeField]
    Text timeMSec2;
    [SerializeField]
    Text banksDec;
    [SerializeField]
    Text banksUnits;
    [SerializeField]
    Image firstStar;
    [SerializeField]
    Image secondStar;
    [SerializeField]
    Image thirdStar;
    [SerializeField]
    Image itemLock;
    string[] time=new string[2];
    bool interactable;
    // Use this for initialization
    void Start () {

        //  Debug.Log(Managers._gameManager.statistics[1].Time);

      //  Debug.Log(sceneNumber);
        DisableElements();
        
        if (Managers._gameManager.LevelsComplete>=unlockLimit) {


            switch (_unlockType) {
                case UnlockType.Default:
                    _lock.enabled = false;
                    playButton.SetActive(true);

                    if (sceneNumber != 1) {
                        EnableElements();
                    }

                    FillCard();
                    break;
                case UnlockType.Skate:
                    if (Managers._itemManager.DressOnSkate || Managers._gameManager.LevelsComplete>=sceneNumber) {

                        Debug.Log("Skate");
                        if (itemLock != null) {
                            itemLock.enabled = false;
                        }
                        _lock.enabled = false;
                        playButton.SetActive(true);

                        if (sceneNumber != 1) {
                            EnableElements();
                        }
                        FillCard();
                    }
                    break;
                case UnlockType.Moto:
                    if (Managers._itemManager.DressOnMoto || Managers._gameManager.LevelsComplete >= sceneNumber) {

                        Debug.Log("Moto");
                        if (itemLock != null) {
                            itemLock.enabled = false;
                        }
                        _lock.enabled = false;
                        playButton.SetActive(true);

                        if (sceneNumber != 1) {
                            EnableElements();
                        }
                        FillCard();
                    }
                    break;

            }
          //  Debug.Log(sceneNumber);
         

            //if (itemLock != null) {
            //    itemLock.enabled = false;
            //}

            //  Debug.Log(Managers._gameManager.statistics.ContainsKey(sceneNumber));


        }
	}


    void FillCard() {

        if (Managers._gameManager.statistics.ContainsKey(sceneNumber)) {

            //  Debug.Log(Managers._gameManager.statistics[sceneNumber].Stars);
            //  Debug.Log(Managers._gameManager.statistics[sceneNumber].Time);
            banksDec.text = Managers._gameManager.statistics[sceneNumber].Banks.ToString();
            if (Managers._gameManager.statistics[sceneNumber].Banks > 9) {
                banksDec.text = Managers._gameManager.statistics[sceneNumber].Banks.ToString().ToCharArray()[0].ToString();
                banksUnits.text = Managers._gameManager.statistics[sceneNumber].Banks.ToString().ToCharArray()[1].ToString();
            }
            else {
                banksDec.text = "0";
                banksUnits.text = Managers._gameManager.statistics[sceneNumber].Banks.ToString();
            }
            // timeSec.text=Managers._gameManager.statistics[sceneNumber].Time.ToString();
            time = Managers._gameManager.statistics[sceneNumber].Time.ToString().Split('.');

            Debug.Log(Managers._gameManager.statistics[sceneNumber].Time.ToString());
            timeSec.text = time[0].ToCharArray()[0].ToString(); //separate string value of time

            timeSec2.text = time[0].ToCharArray()[1].ToString();

            if (time.Length < 2) {
                timeMSec.text = "0";
                timeMSec2.text = "0";
            }
            else {
                timeMSec.text = time[1].ToCharArray()[0].ToString();

                if (time[1].ToCharArray().Length < 2) {
                    timeMSec2.text = "0";
                }
                else {
                    timeMSec2.text = time[1].ToCharArray()[1].ToString();
                }

            }

            // Debug.Log(time[1].ToCharArray()[0].ToString());
            //  Debug.Log(time[0]);
            StarsCount();


        }

    }

    void StarsCount() {
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

    void DisableElements() {
        playButton.SetActive(false);
        playButton.GetComponent<Button>().interactable = false;
        interactable = false;
       // timeSec.gameObject.SetActive(false);
        banksDec.gameObject.SetActive(false);
        firstStar.gameObject.SetActive(false);
        secondStar.gameObject.SetActive(false);
        thirdStar.gameObject.SetActive(false);
    }

    void EnableElements() {
        playButton.SetActive(true);
        timeSec.gameObject.SetActive(true);
        banksDec.gameObject.SetActive(true);
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
