using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AnimateBanks : MonoBehaviour {
    Transform _playerTrans;
    Transform bankTrans;
    Vector3 startPos;
    SpriteRenderer bankSprite;
    MeshRenderer meshrenderer;
	// Use this for initialization
	//void Start () {
  
	//}

    void OnEnable() {
       // bankSprite = GetComponent<SpriteRenderer>();
        meshrenderer = GetComponent<MeshRenderer>();
        meshrenderer.enabled = true;
        _playerTrans = FindObjectOfType<MovePlayer>().GetComponent<Transform>();
        bankTrans = GetComponent<Transform>();
        startPos = bankTrans.localPosition;
        //bankSprite.enabled = true;
        bankTrans.DOLocalMove(_playerTrans.position+Vector3.up*0.5f,1f);
      
    }

    void OnDisable() {
        DOTween.Clear();
        bankTrans.localPosition = startPos;
       
    }
    // Update is called once per frame

 
	//void Update () {
	
	//}


    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag=="Player") {
          //  bankSprite.enabled = false;
            meshrenderer.enabled = false;
        }
    }
}
