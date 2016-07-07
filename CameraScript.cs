using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraScript : MonoBehaviour {
    [SerializeField]
    Transform playerTransform;

    Transform cameraTransform;

    public float _distance;
    Vector3 startPos;
    Sequence move;
    BallDirection cameraDirection;
    MovePlayer player;
    // Use this for initialization
    void Start () {
        Messenger.AddListener("CameraRotate",MoveBack);

        player = playerTransform.GetComponent<MovePlayer>();
        cameraDirection = new BallDirection();
        move = DOTween.Sequence();
       // DOTween.defaultAutoKill = false;
        cameraTransform = GetComponent<Transform>();
        startPos = cameraTransform.localPosition;
        // move.Append(cameraTransform.DOMove(new Vector3(playerTransform.position.x, startPos.y, playerTransform.position.z - _distance),));

        //Debug.Log(startPos.x);
	}
    void OnDestroy() {
        Messenger.RemoveListener("CameraRotate",MoveBack);
    }
	
	// Update is called once per frame
	void Update () {
       
     //  cameraTransform.position = new Vector3(playerTransform.position.x,startPos.y,playerTransform.position.z-_distance);
	}
    void LateUpdate() {
        cameraTransform.LookAt(playerTransform.position + Vector3.up * 0.5f);

       
    }

   public void MoveBack() {
        Invoke("MoveBackDelayed", 0.1f);

    }

    void MoveBackDelayed() {

        //move.Append(cameraTransform.DOLocalMoveZ(startPos.z + _distance + 1, 1f));
        //move.Join(cameraTransform.DOLocalMoveY(startPos.y + _distance + 2, 1f));
        //StartCoroutine(AnimateCameraForward());

        switch (player.ballDirect) {
            case BallDirection.left:
                move.Append(cameraTransform.DOLocalMoveZ(startPos.z, 1f));
                move.Join(cameraTransform.DOLocalMoveY(startPos.y + _distance-1, 1f));
                move.Join(cameraTransform.DOLocalMoveX(startPos.x + _distance-1, 1f));
                StartCoroutine(AnimateCameraLeft());
                break;
            case BallDirection.right:
                move.Append(cameraTransform.DOLocalMoveZ(startPos.z, 1f));
                move.Join(cameraTransform.DOLocalMoveY(startPos.y + _distance-1, 1f));
                move.Join(cameraTransform.DOLocalMoveX(startPos.x - _distance+1, 1f));
                StartCoroutine(AnimateCameraRight());
                break;
            case BallDirection.forward:
                move.Append(cameraTransform.DOLocalMoveZ(startPos.z - _distance, 1f));
                move.Join(cameraTransform.DOLocalMoveY(startPos.y + _distance , 1f));
                StartCoroutine(AnimateCameraForward());
                break;

        }


        // move.Append(cameraTransform.DOMoveZ(playerTransform.position.z - _distance, 1f));
        // cameraTransform.DOMoveZ(playerTransform.position.z - _distance - 2, 1f);
        //move.PlayBackwards();
        //Debug.Log(move.IsComplete());

    }
    IEnumerator AnimateCameraForward() {
        yield return new WaitForSeconds(0.6f);
        move.Append(cameraTransform.DOLocalMoveZ(startPos.z, 0.6f));
        move.Join(cameraTransform.DOLocalMoveY(startPos.y, 1f));
    }

    IEnumerator AnimateCameraLeft() {
        yield return new WaitForSeconds(0.8f);
        move.Append(cameraTransform.DOLocalMoveZ(startPos.z, 0.6f));
        move.Join(cameraTransform.DOLocalMoveY(startPos.y, 1f));
        move.Join(cameraTransform.DOLocalMoveX(startPos.x, 0.4f).SetEase(Ease.OutSine));
    }

    IEnumerator AnimateCameraRight() {
        yield return new WaitForSeconds(0.8f);
        move.Append(cameraTransform.DOLocalMoveZ(startPos.z, 0.6f));
        move.Join(cameraTransform.DOLocalMoveY(startPos.y, 1f));
        move.Join(cameraTransform.DOLocalMoveX(startPos.x, 0.4f).SetEase(Ease.OutSine));
    }
}
