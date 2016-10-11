using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraFallow : MonoBehaviour {

   // [SerializeField]
     public Transform player;
     Transform cameraTrans;
    RippleEffect _rippleEffect;
	// Use this for initialization
	void Start () {
        _rippleEffect = GetComponent<RippleEffect>();
        cameraTrans = GetComponent<Transform>();
        cameraTrans.position = GetComponent<Transform>().position;

       // Debug.Log(cameraTrans.position);
	}
	
	// Update is called once per frame
	void Update () {

        //  cameraTrans.Translate(new Vector3(cameraTrans.position.x,cameraTrans.position.y,player.transform.position.z));
        // cameraTrans.position = new Vector3(cameraTrans.position.x, Mathf.Lerp(cameraTrans.position.y, player.transform.position.y+1, 0.05f),Mathf.Lerp( cameraTrans.position.z,player.transform.position.z,0.05f));
        // cameraTrans.position = Vector3.Lerp(cameraTrans.position,player.position+Vector3.right*3+Vector3.up*2,0.1f);
        // cameraTrans.position = player.position+Vector3.right*2;
        cameraTrans.DOMoveZ(player.position.z,2f);
      //  cameraTrans.DOMoveZ(player.position.z+7f, 2f);
        cameraTrans.DOMoveY(player.position.y+1, 2f);

    }

    //public void Jump() {
    //    _rippleEffect.enabled = true;
    //    StartCoroutine(DisableRipple());

    //}

    //IEnumerator DisableRipple() {
    //    yield return new WaitForSeconds(0.4f);
    //    _rippleEffect.enabled = false;
    //}
}
