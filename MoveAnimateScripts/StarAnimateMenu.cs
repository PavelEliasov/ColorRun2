using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StarAnimateMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Debug.Log("Animate Stars");
        gameObject.transform.DOShakeScale(0.5f,Vector3.one,3,1,true);
	}
	
	
}
