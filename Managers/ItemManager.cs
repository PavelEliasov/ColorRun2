using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemManager :MonoBehaviour{
  [SerializeField]  public bool Skate;
    public bool HeadPhones;
    public bool RollerSkate;
    public bool Magnet;
    public bool Boots;
    public bool Flesh;

    public bool DressOnSkate;
    public bool DressOnHeadPhones;
    public bool DressOnRollerSkate;

   // private static ItemManager _instance;

    //public static ItemManager Instance {
    //    get  {
    //        if (_instance==null) {
    //            _instance = new ItemManager();
    //        }
    //        return _instance;
    //    }

    //    set {
    //        _instance = value;
    //    }
    //}

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
