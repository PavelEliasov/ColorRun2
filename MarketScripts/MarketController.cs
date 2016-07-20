﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MarketController : MonoBehaviour {
    [Header("UI")]
    [SerializeField]
    Text _totalBanks;
   
    [SerializeField]
    GameObject QuestPanel;
    [SerializeField]
    GameObject QuestBuyPanel;
    [SerializeField]
    GameObject QuestNotEnoughBanksPanel;
    [SerializeField]
    GameObject QuestPickItemPanel;

    [SerializeField]
    GameObject[] equipment;

    MarketItem _selectedItem;

    GameObject _objForEnable;

    int priceOfSelectedItem;

    void Awake() {
        _objForEnable = null;
    }
	// Use this for initialization
	void Start () {
        _totalBanks.text ="X "+Managers._gameManager.TotalBanks.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void EnableEquipment(GameObject objforEnable) {
        _objForEnable = objforEnable;
        foreach (var eq in equipment) {
            eq.SetActive(false);
        }
        objforEnable.SetActive(true);
    }
    public void SelectItem(MarketItem item) {
        priceOfSelectedItem = item.price;
        _selectedItem = item;//add Item to public MarketItem field for manipulate it

    }
    //public void CostOfEquipment(int price) {
    //    priceOfSelectedItem = price;
    //}

    public void EnableQuestPanel() {
        QuestPanel.SetActive(true);
        if (_objForEnable == null) {
            QuestPickItemPanel.SetActive(true);
        }
        else {
            QuestBuyPanel.SetActive(true);
        }
    }
    public void CloseQuestPanel() {
        QuestPanel.SetActive(false);

        QuestPickItemPanel.SetActive(false);
        QuestBuyPanel.SetActive(false);
        QuestNotEnoughBanksPanel.SetActive(false);
    }

    public void BuyItemFinaly() {
        if (priceOfSelectedItem > Managers._gameManager.TotalBanks) {
            QuestBuyPanel.SetActive(false);
            QuestNotEnoughBanksPanel.SetActive(true);
        }
        else {
            switch (_selectedItem.Item) {
                case ItemEnum.HeadPhones:
                    Managers._itemManager.HeadPhones = true;
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
            Managers._gameManager.SpentBanks += priceOfSelectedItem;
          //  Managers._gameManager.TotalBanks -= priceOfSelectedItem;

            CloseQuestPanel();
            StartCoroutine(DownCountTotalBanks());
        }

    }


    IEnumerator DownCountTotalBanks() {
       // yield return null;
        int step = 0;
        if (priceOfSelectedItem<30) {
            while (step <priceOfSelectedItem) {
                step++;
                Managers._gameManager.TotalBanks--;
                _totalBanks.text = "X " + Managers._gameManager.TotalBanks;
                yield return new WaitForSeconds(0.1f);
            }
        }
        if (priceOfSelectedItem >=30) {
            while (step < priceOfSelectedItem) {
                step++;
                Managers._gameManager.TotalBanks--;
                _totalBanks.text = "X " + Managers._gameManager.TotalBanks;
                yield return new WaitForSeconds(0.05f);
            }
        }

    }
}