using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Rotator : MonoBehaviour {
    Transform _transform;
	// Use this for initialization
	void Start () {
      //  DOTween.defaultRecyclable = true;
        _transform = GetComponent<Transform>();
        _transform.DOLocalRotate(Vector3.right * 360, 5f, RotateMode.FastBeyond360).SetRecyclable(true);
        
	}
	
	// Update is called once per frame
	void Update () {

	}
}
