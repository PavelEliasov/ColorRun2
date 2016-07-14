using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MirrorFallow : MonoBehaviour {
    public Transform cameraTrans;
    Transform mirrorTrans;
    float delta;
    // Use this for initialization
    void Start () {
        mirrorTrans = GetComponent<Transform>();
        delta = mirrorTrans.position.z + cameraTrans.position.z;
        //Debug.Log(mirrorTrans.position);
	}
	
	// Update is called once per frame
	void Update () {
        mirrorTrans.DOMoveZ(cameraTrans.position.z+150,1f);
	}
}
