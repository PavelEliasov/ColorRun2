using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    GameObject[] equipments;

    MarketItem _selectedItem;
    MarketItem[] _marketItems;

    GameObject _objForEnable;

    MovePlayer player;
    Animator _playerAnimator;

    int priceOfSelectedItem;

    void Awake() {
        _objForEnable = null;
    }
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<MovePlayer>();
        _playerAnimator = player.GetComponent<Animator>();
        _marketItems = FindObjectsOfType<MarketItem>();
        _playerAnimator.SetBool("Run", true);
        _totalBanks.text ="X "+Managers._gameManager.TotalBanks.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //public void EnableEquipment(GameObject objforEnable) {
    //    _objForEnable = objforEnable;
    //    foreach (var eq in equipments) {
    //        eq.SetActive(false);
    //    }
    //    objforEnable.SetActive(true);
    //}
    public void SelectItem(MarketItem item) {

        DisableAllItems();

        priceOfSelectedItem = item.price;
        _selectedItem = item;//add Item to public MarketItem field for manipulate it

        _playerAnimator.SetBool("Run",false);
        _playerAnimator.SetBool("Skate", false);
        _playerAnimator.SetBool("Rollers", false);

        switch (item.Item) {
            case ItemEnum.Skate:
                foreach (var eq in item._itemsForEnabled) {
                    eq.SetActive(true);
                }
                _playerAnimator.SetBool("Skate", true);
                break;
            case ItemEnum.RollerSkate:
                foreach (var eq in item._itemsForEnabled) {
                    eq.SetActive(true);
                }
                _playerAnimator.SetBool("Rollers",true);
                break;
            case ItemEnum.HeadPhones:
                foreach (var eq in item._itemsForEnabled) {
                    eq.SetActive(true);

                }
                _playerAnimator.SetBool("Run", true);
                break;

            case ItemEnum.Flesh:
                foreach (var eq in item._itemsForEnabled) {
                    eq.SetActive(true);

                }
                _playerAnimator.SetBool("Run", true);
                break;
            case ItemEnum.Magnet:
                foreach (var eq in item._itemsForEnabled) {
                    eq.SetActive(true);

                }
                _playerAnimator.SetBool("Run", true);
                break;
            default:
                _playerAnimator.SetBool("Run", true);
                break;
        }

    }
    void DisableAllItems() {
        foreach (var eq in _marketItems) {
            if (eq._itemsForEnabled!=null) {
                foreach (var item in eq._itemsForEnabled) {
                    item.SetActive(false);
                }
            }
        }
    }
    //public void CostOfEquipment(int price) {
    //    priceOfSelectedItem = price;
    //}

    public void EnableQuestPanel() {
        QuestPanel.SetActive(true);
        if (_selectedItem == null) {
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
                    _selectedItem = null;
                    break;
                case ItemEnum.RollerSkate:
                    Managers._itemManager.RollerSkate = true;
                    break;
                case ItemEnum.Skate:
                    Managers._itemManager.Skate = true;
                 //   _selectedItem = null;
                    break;
                case ItemEnum.Moto:
                    Managers._itemManager.Moto = true;
                    //   _selectedItem = null;
                    break;
                case ItemEnum.Key:
                    Managers._itemManager.Key = true;
                    //   _selectedItem = null;
                    break;
                case ItemEnum.Boots:
                    Managers._itemManager.Boots++;
                 //   _selectedItem = null;//UnSelect Item
                    break;
                case ItemEnum.Flesh:
                    Managers._itemManager.Flash++;
                   // _selectedItem = null;
                    break;
                case ItemEnum.Magnet:
                    Managers._itemManager.Magnet++;
                  //  _selectedItem = null;
                    break;

            }
            _selectedItem = null;
            foreach (MarketItem item in _marketItems) {
                item.CheckItem();
            }
            Managers._gameManager.SpentBanks += priceOfSelectedItem;

            CloseQuestPanel();
            StartCoroutine(DownCountTotalBanks());

        }

    }


    IEnumerator DownCountTotalBanks() {
       // yield return null;
        int step = 0;
        int ttlBanks = Managers._gameManager.TotalBanks;
        if (priceOfSelectedItem<30) {
            while (step <priceOfSelectedItem) {
                step++;
               // Managers._gameManager.TotalBanks--;
                ttlBanks--;
                _totalBanks.text = "X " + ttlBanks;
                yield return new WaitForSeconds(0.1f);
            }
        }
        if (priceOfSelectedItem >=30) {
            while (step < priceOfSelectedItem) {
                step++;
                ttlBanks--;
                _totalBanks.text = "X " + ttlBanks;
                yield return new WaitForSeconds(0.05f);
            }
        }

    }

    public void GoToMenu() {

        SceneManager.LoadScene("MainMenu");
    }
}
