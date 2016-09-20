using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MoveSlide : MonoBehaviour {

    // Use this for initialization
    Transform slideTrans;
    float step;
    float scale;
    Sequence seq;
	void Start () {
        seq = DOTween.Sequence();
        slideTrans = GetComponent<Transform>();

       // Debug.Log(slideTrans.position);
	}
	
	// Update is called once per frame
	//void Update () {
 //       //slideTrans.Translate(step,slideTrans.localPosition.y, slideTrans.localPosition.z);
 //     //  slideTrans.localPosition= Vector3.MoveTowards(slideTrans.localPosition,new Vector3(step, slideTrans.localPosition.y, slideTrans.localPosition.z),5f);
	//}
    public void MoveRight(float step) {
        slideTrans.DOLocalMoveX(step, 0.3f, true);
    }

    public void MoveLeft(float step) {
     
       slideTrans.DOLocalMoveX(step, 0.3f,true);
    }
    public void MoveUp(float step) {
        slideTrans.DOLocalMoveY(step, 0.2f, true);
    }

    public void MoveDown(float step) {

        slideTrans.DOLocalMoveY(step, 0.2f, true);
    }

    public void Scale(Vector3 scaleValue) {

        slideTrans.DOScale(scaleValue,0.5f);
    }
    public void UnScale() {
        slideTrans.DOScale(Vector3.one,0.3f);
    }
    public void StopAllTweens() {

        DOTween.Clear();
    }

    public void RightDrag(int dragPos) {
        slideTrans.localPosition+= new Vector3(dragPos,0,0);
        //for (int i=0;i<=10;++i) {
        //    slideTrans.localPosition += new Vector3(dragPos, 0, 0);
        //}
        //slideTrans.DOMoveX(slideTrans.position.x+dragPos,0.001f,true);
       // Debug.Log(slideTrans.localPosition);
    }
    public void LeftDrag(int dragPos) {
        slideTrans.localPosition -= new Vector3(dragPos, 0, 0);
    }

    public void ScaleDrag(float scaleValue) {
        // float value = 0;
        scale += scaleValue;
        slideTrans.localScale = new Vector3(Mathf.Clamp(scale,1,1.2f),Mathf.Clamp(scale, 1, 1.2f), Mathf.Clamp(scale, 1, 1.2f));
    }

    public void UnscaleDrag() {

    }
}
