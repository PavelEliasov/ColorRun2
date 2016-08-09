using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainEquipment : MonoBehaviour {
    public ItemEnum Item;

    Button _itemButton;

    [SerializeField]
   public  Image lightImage;
  //  [SerializeField]
   //public GameObject ItemForEnable;

    public GameObject[] itemsForEnable;
    // Use this for initialization
    void Start () {
        _itemButton = GetComponent<Button>();

        CheckItemStatus();

    }


    public void CheckItemStatus() {
        switch (Item) {
       
            case ItemEnum.RollerSkate:

                _itemButton.interactable = Managers._itemManager.RollerSkate;
                lightImage.gameObject.SetActive(Managers._itemManager.DressOnRollerSkate);
               // ItemForEnable.SetActive(Managers._itemManager.DressOnRollerSkate);
                foreach (var item in itemsForEnable) {
                    item.SetActive(Managers._itemManager.DressOnRollerSkate);
                }

                break;

            case ItemEnum.Skate:

                _itemButton.interactable = Managers._itemManager.Skate;
                lightImage.gameObject.SetActive(Managers._itemManager.DressOnSkate);
               // ItemForEnable.SetActive(Managers._itemManager.DressOnSkate);
                foreach (var item in itemsForEnable) {
                    item.SetActive(Managers._itemManager.DressOnSkate);
                }
                break;

            case ItemEnum.Moto:

                _itemButton.interactable = Managers._itemManager.Moto;
                lightImage.gameObject.SetActive(Managers._itemManager.DressOnMoto);
                //  ItemForEnable.SetActive(Managers._itemManager.DressOnMoto);
                foreach (var item in itemsForEnable) {
                    item.SetActive(Managers._itemManager.DressOnMoto);
                }
                break;


        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
