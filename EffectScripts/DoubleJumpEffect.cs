using UnityEngine;
using System.Collections;

public class DoubleJumpEffect : MonoBehaviour {
    static DoubleJumpEffect _instance;

    public static DoubleJumpEffect Instance {
        get {
            if (_instance == null) {
                var container = FindObjectOfType<DoubleJumpEffect>();
                _instance = container;
                if (_instance==null) {
                    _instance = new GameObject().AddComponent<DoubleJumpEffect>();
                }
            }
            return _instance;
        }

    }
}
