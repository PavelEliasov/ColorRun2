using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EquipmentController : MonoBehaviour {
    [SerializeField]
    GameObject HeadPhones;

    MovePlayer player;
    Animator _playerAnimator;

    MainEquipment[] mainEquipments;
    // Use this for initialization

    void Awake() {
        

    }
    void Start () {
        player = FindObjectOfType<MovePlayer>();
        _playerAnimator = player.GetComponent<Animator>();
        _playerAnimator.SetBool("Run", Managers._itemManager.Default);
        _playerAnimator.SetBool("Skate", Managers._itemManager.DressOnSkate);
        _playerAnimator.SetBool("Rollers", Managers._itemManager.DressOnRollerSkate);
        _playerAnimator.SetBool("Moto", Managers._itemManager.DressOnMoto);
        mainEquipments = FindObjectsOfType<MainEquipment>();


        Debug.Log(player);
	}
	
    
	// Update is called once per frame
	void Update () {
	
	}
    public void Enable_DisableHeadPhones(Image Light) {
        HeadPhones.SetActive(!HeadPhones.activeSelf);
        Light.gameObject.SetActive(!Light.gameObject.activeSelf);
        Managers._itemManager.DressOnHeadPhones = HeadPhones.activeSelf;
    }
    public void Enable_DisableMainEquipment(MainEquipment eq) {
        DisableOther(eq);

        switch (eq.Item) {
            case ItemEnum.Skate:

                //  eq.ItemForEnable.SetActive(!eq.ItemForEnable.activeSelf);
                foreach (var item in eq.itemsForEnable) {
                    if (item!=null) {
                        item.SetActive(!item.activeSelf);
                    }
                   
                }
              
                if (eq.itemsForEnable[0]!=null) {
                    Managers._itemManager.DressOnSkate = eq.itemsForEnable[0].activeSelf;
                }

                eq.lightImage.gameObject.SetActive(!eq.lightImage.gameObject.activeSelf);

                _playerAnimator.SetBool("Skate", Managers._itemManager.DressOnSkate);
                _playerAnimator.SetBool("Run",! Managers._itemManager.DressOnSkate);
                Managers._itemManager.Default = !Managers._itemManager.DressOnSkate;
                break;

            case ItemEnum.RollerSkate:
                // eq.ItemForEnable.SetActive(!eq.ItemForEnable.activeSelf);
                foreach (var item in eq.itemsForEnable) {
                    if (item != null) {
                        item.SetActive(!item.activeSelf);
                    }

                }

                if (eq.itemsForEnable[0] != null) {
                    Managers._itemManager.DressOnRollerSkate = eq.itemsForEnable[0].activeSelf;
                }
                eq.lightImage.gameObject.SetActive(!eq.lightImage.gameObject.activeSelf);
               // Managers._itemManager.DressOnRollerSkate = eq.ItemForEnable.activeSelf;


                _playerAnimator.SetBool("Skate", Managers._itemManager.DressOnRollerSkate);
                _playerAnimator.SetBool("Run", !Managers._itemManager.DressOnRollerSkate);
                Managers._itemManager.Default = !Managers._itemManager.DressOnRollerSkate;
                break;

            case ItemEnum.Moto:

                //   eq.ItemForEnable.SetActive(!eq.ItemForEnable.activeSelf);
                foreach (var item in eq.itemsForEnable) {
                    if (item != null) {
                        item.SetActive(!item.activeSelf);
                    }

                }

                if (eq.itemsForEnable[0] != null) {
                    Managers._itemManager.DressOnMoto = eq.itemsForEnable[0].activeSelf;
                }

                eq.lightImage.gameObject.SetActive(!eq.lightImage.gameObject.activeSelf);
               // Managers._itemManager.DressOnMoto = eq.ItemForEnable.activeSelf;


                _playerAnimator.SetBool("Moto", Managers._itemManager.DressOnMoto);
                _playerAnimator.SetBool("Run", !Managers._itemManager.DressOnMoto);
                Managers._itemManager.Default = !Managers._itemManager.DressOnMoto;
                break;
        }
     

    }

    void DisableOther(MainEquipment equipment) {
        _playerAnimator.SetBool("Run", false);
        _playerAnimator.SetBool("Skate", false);
        _playerAnimator.SetBool("Moto", false);

        Managers._itemManager.DressOnSkate = false;
        Managers._itemManager.DressOnRollerSkate = false;

        foreach (MainEquipment eq in mainEquipments) {
            if (equipment != eq) {
                foreach (var item in eq.itemsForEnable) {
                    item.SetActive(false);
                }
                   // eq.ItemForEnable.SetActive(false);
             
             //   eq.ItemForEnable.SetActive(false);
                eq.lightImage.gameObject.SetActive(false);

             
            }
          
           // 
          //  eq.lightImage.enabled=false;
        }

    }

    public void MainMenuButton() {
        SceneManager.LoadScene("MainMenu");
    }
}
