using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class EquipmentItem : MonoBehaviour {


    public ItemEnum Item;

    Button _itemButton;

    [SerializeField]
    Image lightImage;
    [SerializeField]
    GameObject ItemForEnable;
	// Use this for initialization
	void Start () {
        _itemButton = GetComponent<Button>();
      
        CheckItemStatus();
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

    public void CheckItemStatus() {
        switch (Item) {
            case ItemEnum.HeadPhones:
                
                    _itemButton.interactable = Managers._itemManager.HeadPhones;
                    lightImage.gameObject.SetActive(Managers._itemManager.DressOnHeadPhones);
                    ItemForEnable.SetActive(Managers._itemManager.DressOnHeadPhones);
              
                break;

            case ItemEnum.RollerSkate:
              
                    _itemButton.interactable = Managers._itemManager.RollerSkate;
           
                break;

            case ItemEnum.Skate:
             
                    _itemButton.interactable = Managers._itemManager.Skate;

                break;


        }
    }


}
