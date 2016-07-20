using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MarketItem : MonoBehaviour {

    public ItemEnum Item;
    public int price;
    [SerializeField]
    public  GameObject _priceImage;
    Button itemButton;
    
	// Use this for initialization
	void Start () {
        itemButton = GetComponent<Button>();
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

                break;
            case ItemEnum.Boots:

                break;
            case ItemEnum.Flesh:

                break;
            case ItemEnum.Magnet:

                break;

        }
    }
}
