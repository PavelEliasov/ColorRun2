using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AnimateButon : MonoBehaviour {
   Transform _buttonTrans;
    Sequence buttonSeq;
    Tween buttonTween;
    // Use this for initialization
    void Awake() {
     
      //   DOTween.defaultAutoKill = false;
        _buttonTrans = GetComponent<Transform>();

    }
	void Start () {
      


    }

    void OnEnable() {
        buttonSeq = DOTween.Sequence();
      //  DOTween.defaultTimeScaleIndependent = false;
        // DOTween.timeScale = 1;
        // buttonSeq.SetUpdate(false);
         buttonSeq.Append(_buttonTrans.DOScale(Vector3.one * 1.3f, 0.4f)).SetAutoKill(false);
         buttonSeq.OnComplete(buttonSeq.PlayBackwards);
        // buttonSeq.Kill(true);
    }
	// Update is called once per frame
	void Update () {
	
	}

    void PlayBack() {

        Debug.Log("Back");
       
       
    }
}
