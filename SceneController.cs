﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class SceneController : MonoBehaviour {
    static SceneController _instance;
    PortalScript _portal;

    [SerializeField]
    Text Time;
    [SerializeField]
    Sprite brokenHeart;
    [SerializeField]
    Image[] lifeImages;

    [SerializeField]
   public int firstStarPrice;
    [SerializeField]
   public int secondStarPrice;
    [SerializeField]
   public int thirdStarPrice;

    [HideInInspector]
    public Statistic stats;//=new Statistic();

    [SerializeField]
    GameObject endOfLevelPanel;

    [SerializeField]
    GameObject diePanel;

    [SerializeField]
    GameObject pausePanel;

    MovePlayer _player;
    Animator _playerAnimator;

    [Header("Equipment")]
    [SerializeField]
    GameObject JetPack;
    [SerializeField]
    GameObject Scarf;
    [SerializeField]
    GameObject HeadPhones;
    [SerializeField]
    GameObject Skate;
    [SerializeField]
    GameObject Moto;
    [SerializeField]
    GameObject LeftRoller;
    [SerializeField]
    GameObject RightRoller;

    [Header("Music")]
    [SerializeField]
    AudioClip [] _sceneSounds;
 

    private DieElement[] dieElements;

    float _score;
    float _starcount;
    int _lifecount=3;

    int sceneNumber;

    bool endofLvl;

    public static SceneController Instance {
        get  {
            if (_instance == null) {
                var container = FindObjectOfType<SceneController>();
                _instance = container;
            }
            return _instance;
        }
    }

    // Use this for initialization
    void Start () {
        if (Screen.width>1920) {
            EventSystem.current.pixelDragThreshold = 20;
        }
        if (Managers._audioManager._audioSource!=null) {
            int rand=0;
            if (_sceneSounds != null) {
                rand = UnityEngine.Random.Range(0, _sceneSounds.Length);

                if (_sceneSounds[rand] != null) {

                    Managers._audioManager.PlayMusic(_sceneSounds[rand]);
                }
            }
           
        }
        _portal = FindObjectOfType<PortalScript>();
       
        _player = FindObjectOfType<MovePlayer>();
        _playerAnimator = _player.GetComponent<Animator>();
        
        dieElements= FindObjectsOfType<DieElement>();


         int.TryParse(SceneManager.GetActiveScene().name,out sceneNumber);
         stats = new Statistic();
        StartCoroutine(Timer());
       Debug.Log(SceneManager.GetActiveScene().name);
        CheckEquipmentState();
    }

    void CheckEquipmentState() {
        HeadPhones.SetActive(Managers._itemManager.DressOnHeadPhones);
        Skate.SetActive(Managers._itemManager.DressOnSkate);
        Moto.SetActive(Managers._itemManager.DressOnMoto);
        LeftRoller.SetActive(Managers._itemManager.DressOnRollerSkate);
        RightRoller.SetActive(Managers._itemManager.DressOnRollerSkate);

        if (_player._jumpState == MovePlayer.State.Default || Managers._itemManager.DressOnMoto == true) {
            JetPack.SetActive(false);
            if (Managers._itemManager.DressOnMoto == true) {
                Scarf.SetActive(true);
            }
        }
        else {
            JetPack.SetActive(true);
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (endofLvl==false) {
            Time.text =string.Format("{0:00.00}",UnityEngine.Time.timeSinceLevelLoad);
        }
        
      //  Debug.Log(stats.Time);
      //Debug.Log(stats.Stars);
    }

    void OnDestroy() {
        Managers._gameManager.sceneNumber = sceneNumber;
    }

    public void ChangeScore(float value) {
        _score += value;
        _score = _score < 0 ? 0 : _score;
      //  scoreText.text = _score.ToString();

    }

    public void RemoveLife() {
        

       // lifeImages[_lifecount-1].enabled = false;
        lifeImages[_lifecount - 1].sprite = brokenHeart;
        _lifecount--;

        if (_lifecount==0) {
             Die();
            return;
        }
        StartCoroutine(ReturnForm());

    }

    public void AddLife() {

    }
    public void Die() {
        _player.Die();
        _player.tag = "PlayerDamaged";//set player tag to "Untagged" for disable collisions from platform
        _playerAnimator.enabled = false;
        _player.enabled = false;
        foreach (DieElement element in dieElements) {
            element.EnableRigidbody();
        }
        Invoke("EnableDiePanel",1f);
       // SceneManager.LoadScene("1");
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag=="Player") {
            LevelComplete();
            StopCoroutine("Timer");
            StopAllCoroutines();
        }
       
    }
     void LevelComplete() {
        _portal.DisablePortal();
        endofLvl = true;
        CountStars();
        stats.Time= UnityEngine.Time.timeSinceLevelLoad;
        if (SceneManager.GetActiveScene().name == "2") {
            stats.Time=23.00f;
        }
        // StopCoroutine(Timer());
        stats.Time =(float) System.Math.Round((double)stats.Time,2,System.MidpointRounding.AwayFromZero);
        Managers._gameManager.Stats(sceneNumber, stats);

        Debug.Log(stats.Time);
        endOfLevelPanel.SetActive(true);
        _player.gameObject.SetActive(false);
       Debug.Log(stats.Banks);
      //  SceneManager.LoadScene("Menu");

      //  StartCoroutine(UnloadScene());
        //  Managers._gameManager.LevelsComplete = 1;

    }

   
    void CountStars() {
        if (stats.Banks>=firstStarPrice) {
            stats.Stars++;
        }
        if (stats.Banks>=secondStarPrice) {
            stats.Stars++;
        }
        if (stats.Banks>=thirdStarPrice) {
            stats.Stars++;
        }

    }

    void EnableDiePanel() {
        diePanel.SetActive(true);
    }

    IEnumerator UnloadScene() {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");

    }
    IEnumerator Timer() {
       // yield return null;
        while (true) {
            stats.Time += 0.01f;
           // Time.text = stats.Time.ToString();
           // Debug.Log(stats.Time);
            yield return new WaitForSeconds(0.01f);
        }

    }


    IEnumerator ReturnForm() {
        _player.tag = "PlayerDamaged";//set player tag to "Untagged" for disable collisions
        _playerAnimator.enabled = false;
       // _player.enabled = false;
        foreach (DieElement element in dieElements) {
            element.EnableRigidbody();
        }
        yield return new WaitForSeconds(0.3f);
       
        _playerAnimator.enabled = true;
       // _player.enabled = false;
        foreach (DieElement element in dieElements) {
            if (element.Lose!=null) {
                element.Lose();
            }
           
            //element.ReturnForm();
        }
        yield return new WaitForSeconds(0.1f);
        _player.tag = "Player";//

    }
    public void RestartLevel() {
        UnscaleTime();
        SceneManager.LoadScene(sceneNumber.ToString());
    }
    public void ReturnToMenu() {
        SceneManager.LoadScene("Menu");
    }

     void UnscaleTime() {
        UnityEngine.Time.timeScale = 1;
    }

    void PauseTime() {
        UnityEngine.Time.timeScale = 0;
    }


    public void PauseGame() {
        PauseTime();
        pausePanel.SetActive(true);

    }
    public void UnPausedGame() {
        UnscaleTime();
        pausePanel.SetActive(false);
    }
}
