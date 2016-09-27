using UnityEngine;
using System.Collections;

public class JumpEffect : MonoBehaviour {
    static JumpEffect _instance;
    
    public static JumpEffect Instance
    {
        get
        {

           
            if (_instance == null) {
                var container = FindObjectOfType<JumpEffect>();
                _instance = container;

            }
            return _instance;
        }


    }

   
}
