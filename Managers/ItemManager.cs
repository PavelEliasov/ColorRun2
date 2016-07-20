using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemManager :MonoBehaviour{
   [SerializeField]
    public bool Skate;
    [SerializeField]
    public bool HeadPhones;
    [SerializeField]
    public bool RollerSkate;
   [SerializeField]
    private int magnet;
    [SerializeField]
    private int boots;
   [SerializeField]
    private int flash;

    [SerializeField]
    public bool DressOnSkate;
    [SerializeField]
    public bool DressOnHeadPhones;
    [SerializeField]
    public bool DressOnRollerSkate;

    public int Magnet   {
        get  {
            if (magnet>3) {
                magnet=3;
            }
            return magnet;
        }

        set {
            if (magnet+value>3) {
                magnet = 3;
            }
            magnet = value;
        }
    }

    public int Boots   {
        get {
            if (boots>3) {
                boots = 3;
            }
            return boots;
        }

        set  {
            if (boots+value>3) {
                boots = 3;
            }
            boots = value;
        }
    }

    public int Flash{
        get  {
            if (flash > 3) {
                flash = 3;
            }
            return flash;
        }

        set {
            if (flash + value > 3) {
                flash = 3;
            }
            flash = value;
        }
    }

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
