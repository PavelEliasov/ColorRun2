using UnityEngine;
using System.Collections;

public class Dust : MonoBehaviour {
    static Dust _instance;
    public Material _dustMaterial;

    public static Dust Instance   {
        get {
            if (_instance==null) {
                var container= FindObjectOfType<Dust>();
                _instance = container;
            
            }
            return _instance;
        }

      
    }
}
