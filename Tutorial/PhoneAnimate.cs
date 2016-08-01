using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class PhoneAnimate : MonoBehaviour {
    Vector3 startpos;
    Transform phoneTrans;
    [SerializeField]
    Image tapImage;
    Transform tapTrans;
	// Use this for initialization
	void Start () {
        tapTrans = tapImage.gameObject.GetComponent<Transform>();
        phoneTrans = GetComponent<Transform>();
        startpos = phoneTrans.localPosition;
      //  phoneTrans.DOLocalMove(startpos - Vector3.left*10, 2f);
	}


    void OnEnable() {
        phoneTrans.DOLocalMove(startpos - Vector3.right*300, 0.5f);
        tapTrans.DOScale(Vector3.one*1.1f,0.2f).SetLoops(7,LoopType.Yoyo);
        tapImage.DOColor(Color.red,0.5f).SetLoops(5,LoopType.Incremental);
        StartCoroutine(Disable());
    }

    IEnumerator Disable() {
        yield return new WaitForSeconds(2f);
        phoneTrans.DOLocalMove(startpos, 0.5f);

    }
	// Update is called once per frame
	void Update () {
	
	}
}
