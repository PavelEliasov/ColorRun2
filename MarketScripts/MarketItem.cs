using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MarketItem : MonoBehaviour {

    public ItemEnum Item;
    public int price;
   
    [SerializeField]
    public  GameObject _priceImage;
    public Text upgradeLvl;

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

                break;
            case ItemEnum.Skate:
                if (Managers._itemManager.Skate == true) {
                    itemButton.interactable = false;
                    _priceImage.SetActive(false);
                }
                break;
            case ItemEnum.Boots:

                break;
            case ItemEnum.Flesh:

                break;
            case ItemEnum.Magnet:
                if (Managers._itemManager.Magnet==3) {
                    itemButton.interactable = false;
                    _priceImage.SetActive(false);
                }
                upgradeLvl.text = Managers._itemManager.Magnet.ToString() + "/3";
                break;

        }
    }
}
