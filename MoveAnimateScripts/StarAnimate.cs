using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StarAnimate : MonoBehaviour {
    Transform starTrans;
    public float rotateSpeed;
    //public float a;
    // Use this for initialization
    MovePlayer _player;
    Transform _playerTransform;
    float _distance=5;
    bool magnet;
	void Start () {
        _player = FindObjectOfType<MovePlayer>();
        _playerTransform = _player.GetComponent<Transform>();
      
        starTrans = GetComponent<Transform>();
       // starTrans.DOMove(new Vector3(100, 270, 100), 1f);
         StartCoroutine(RotateStar());
    }
	
	// Update is called once per frame
	void Update () {

       // Debug.Log(_player.magnetState);
        //starTrans.DOMove(new Vector3(100, 270, 100), 1f);
        starTrans.Rotate(Vector3.up*rotateSpeed*10*Time.deltaTime);

        if ((starTrans.position-_playerTransform.position).magnitude<_distance &&_player.magnetState==true) {
            GoToPlayer();
           // magnet = true;
        }

       // Debug.Log("Star");
    }


    void GoToPlayer() {
        starTrans.DOMove(_playerTransform.position, 2f).SetSpeedBased(true);
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
