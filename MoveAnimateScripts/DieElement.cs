using UnityEngine;
using System.Collections;
using DG.Tweening;


public class DieElement : MonoBehaviour {
    MeshRenderer _mesh;
    Vector3 startPos;
    Quaternion startRotation;
    Transform _elementTransform;
    Rigidbody _rigidBody;

    public delegate void LoseHeart();
    public LoseHeart Lose;
    // Use this for initialization
    void Start () {
      
        _rigidBody = GetComponent<Rigidbody>();
        if (_rigidBody != null) {
            _rigidBody.isKinematic = true;
        }
        _elementTransform=GetComponent<Transform>();
        startPos = _elementTransform.localPosition;
        startRotation= _elementTransform.localRotation;

        if (GetComponent<MeshRenderer>() != null) {
            _mesh = GetComponent<MeshRenderer>();
        }
        Lose = new LoseHeart(ReturnForm);
        Lose += new LoseHeart(LoseHP);

      //  Debug.Log(_rigidBody);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void EnableRigidbody() {
        if (_rigidBody!=null) {
            _rigidBody.isKinematic = false;
        }
       
    }
    public void ReturnForm() {
        if (_rigidBody != null) {
            _rigidBody.isKinematic = true;
        }
        _elementTransform.DOLocalMove(startPos,0.3f);
        _elementTransform.localRotation = startRotation;
    }
    public void LoseHP() {
        StartCoroutine(LoseLife());
    }

    IEnumerator LoseLife() {
        float time = 0;
        yield return null;

            while (time < 1f) {
            time += 0.1f;
            if (_mesh!=null) {
                _mesh.enabled = !_mesh.enabled;
            }
            
            yield return new WaitForSeconds(0.1f);

        }


    }
}
