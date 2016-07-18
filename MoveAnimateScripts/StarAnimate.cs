using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StarAnimate : MonoBehaviour {
    Transform starTrans;
    public float rotateSpeed;
    //public float a;
	// Use this for initialization
	void Start () {
        starTrans = GetComponent<Transform>();
       // starTrans.DOMove(new Vector3(100, 270, 100), 1f);
         StartCoroutine(RotateStar());
    }
	
	// Update is called once per frame
	void Update () {
        //starTrans.DOMove(new Vector3(100, 270, 100), 1f);
        starTrans.Rotate(Vector3.up*rotateSpeed*10*Time.deltaTime);
       // Debug.Log("Star");
    }

    IEnumerator RotateStar() {
        yield return new WaitForSeconds(0.1f);

        while (true) {
            // starTrans.DOShakeRotation(5f,Vector3.up*10,5,0,false);
            // starTrans.DOShakeScale(2f, new Vector3(1,1,0), 0, 0,true);
            starTrans.DOPunchPosition(Vector3.up*0.5f,5f,1,0,false);
            yield return new WaitForSeconds(5f);
        }

    }
}
