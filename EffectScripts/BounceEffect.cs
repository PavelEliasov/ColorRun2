using UnityEngine;
using System.Collections;

public class BounceEffect : MonoBehaviour {

    static BounceEffect _instance;
    [HideInInspector]
    public GameObject _bounceEffect;
    public Transform _transform;
    public static BounceEffect Instance {
        get   {


            if (_instance == null) {
                var container = FindObjectOfType<BounceEffect>();
                _instance = container;

            }
            return _instance;
        }


    }

    void Start() {
        _bounceEffect = Instance.gameObject;
        _transform = _bounceEffect.GetComponent<Transform>();
        _bounceEffect.SetActive(false);
    }
}
