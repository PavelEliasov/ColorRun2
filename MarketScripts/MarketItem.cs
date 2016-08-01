using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MarketItem : MonoBehaviour {

    public ItemEnum Item;
    public int price;
   
    [SerializeField]
    public  GameObject _priceImage;
    public Text upgradeLvl;

    public GameObject[] _itemsForEnabled;
    Button itemButton;
    
	// Use this for initialization
	void Start () {
        itemButton = GetComponent<Button>();
        CheckItem();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CheckItem() {
        switch (Item) {
            case ItemEnum.HeadPhones:
                if (Managers._itemManager.HeadPhones==true) {
                    itemButton.interactable = false;
                    _priceImage.SetActive(false);
                }
               
                break;

            case ItemEnum.RollerSkate:
                if (Managers._itemManager.RollerSkate == true) {
                    itemButton.interactable = false;
                    _priceImage.SetActive(false);
                }
                break;

            case ItemEnum.Skate:
                if (Managers._itemManager.Skate == true) {
                    itemButton.interactable = false;
                    _priceImage.SetActive(false);
                }
                break;
            case ItemEnum.Moto:
                if (Managers._itemManager.Moto == true) {
                    itemButton.interactable = false;
                    _priceImage.SetActive(false);
                }
                break;


            case ItemEnum.Boots:
                if (Managers._itemManager.Boots == 3) {
                    itemButton.interactable = false;
                    _priceImage.SetActive(false);
                }
                upgradeLvl.text = Managers._itemManager.Boots.ToString() + "/3";
                break;

            case ItemEnum.Flesh:
                if (Managers._itemManager.Flash == 3) {
                    itemButton.interactable = false;
                    _priceImage.SetActive(false);
                }
                upgradeLvl.text = Managers._itemManager.Flash.ToString() + "/3";
                break;

            case ItemEnum.Magnet:
                if (Managers._itemManager.Magnet==3) {
                    itemButton.interactable = false;
                    _priceImage.SetActive(false);
                }
                upgradeLvl.text = Managers._itemManager.Magnet.ToString() + "/3";
                break;


            case ItemEnum.Key:
                if (Managers._itemManager.Key ==true) {
                    itemButton.interactable = false;
                    _priceImage.SetActive(false);
                }
               
                break;

        }
    }
}
