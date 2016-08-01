using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EndOfTutorialLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.transform.localScale = Vector3.one * 0.1f;
        // gameObject.transform.localEulerAngles = Vector3.back * 360;
        gameObject.transform.DOLocalRotate(Vector3.back * 360, 1f, RotateMode.FastBeyond360);
        this.gameObject.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InExpo);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
