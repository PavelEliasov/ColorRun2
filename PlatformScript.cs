using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlatformScript : MonoBehaviour {
    public enum PlatformColor {
        red,
        green,
        blue
    }
    PlatformColor _platformColor;

    public enum AnimateDirection {
        FirstPlatform,
        Default,
        Left,
        Right,
        Backward,
        Forward,
        Up,
        Down
    }

    public AnimateDirection FlyDirection = AnimateDirection.Default;

    MeshRenderer _platformMeshRend;
    Transform _platformTrans;
    Transform _playerTrans;

    Vector3 startPos;

    string colorOfPlatform;

    public float enableDistance=20;
       
    public Material red;
    public Material green;
    public Material blue;
    public Material black;
    public Material yellow;

    MovePlayer player;

    bool hide=true;

    // Use this for initialization
    void Awake() {
       
    }
    void Start () {
       // ParticleDust.SetActive(false);
        _platformTrans = GetComponent<Transform>();
        startPos = _platformTrans.position;

        _platformMeshRend = GetComponent<MeshRenderer>();
        if (FindObjectOfType<MovePlayer>() !=null) {
            player = FindObjectOfType<MovePlayer>();
        }
        _playerTrans = player.GetComponent<Transform>();

        colorOfPlatform = IdentifyColor(_platformMeshRend.material.color);
        _platformMeshRend.enabled = false; 


     //   Debug.Log(StateManager.playerPos);

    }

    void OnBecameInvisible() {
      //  Destroy(this.gameObject);
      //  Debug.Log("Invisible");
    }
	
	// Update is called once per frame
	void Update () {

        if ((_platformTrans.position-_playerTrans.position).magnitude< enableDistance && hide) {

            hide = false;
            StartMove();
            _platformMeshRend.enabled =true;
            // Debug.Log("Uhide");
        }
       
	}

    //void StartColor(PlatformColor color) {
    //    switch (color) {
    //        case PlatformColor.red:
    //            _platformMeshRend.material = red;
    //            colorOfPlatform = Colors.Red;
    //            break;
    //        case PlatformColor.green:
    //            _platformMeshRend.material = green;
    //            colorOfPlatform = Colors.Green;
    //            break;
    //        case PlatformColor.blue:
    //            _platformMeshRend.material = blue;
    //            colorOfPlatform = Colors.Blue;
    //            break;
    //    }

    //}
    string IdentifyColor(Color color) {
        if (color.r >color.g && color.r > color.b) {

            return Colors.Red;

        }

        if (color.g >color.r && color.g > color.b && color.g - color.r > 0.2f) {

            return Colors.Green;

        }

        if (color.b > color.g && color.b > color.r) {

            return Colors.Blue;

        }

        if (color.b < 0.1f && color.g < 0.1f && color.r < 0.1f) {

            return Colors.Black;

        }
        if (color.g - color.r < 0.2f) {

            return Colors.Yellow;

        }
        return Colors.Yellow;

    }

    void StartMove() {
        switch (FlyDirection) {
            case AnimateDirection.FirstPlatform:
                //colorOfPlatform = player.color;
                ChangePlatformColor(player.color);
                break;

            case AnimateDirection.Default:
                
                break;
            case AnimateDirection.Left:

               //ebug.Log("LeftMove");
                _platformTrans.position = new Vector3(startPos.x - 15, startPos.y, startPos.z);

              //Debug.Log(_platformTrans.position.x);
                _platformTrans.DOMoveX(startPos.x,1f).SetEase(Ease.InOutExpo);
                break;
            case AnimateDirection.Right:
                _platformTrans.position = new Vector3(startPos.x + 15, startPos.y, startPos.z);
                _platformTrans.DOMoveX(startPos.x, 1f).SetEase(Ease.InOutExpo);
                break;
            case AnimateDirection.Down:
                _platformTrans.position = new Vector3(startPos.x , startPos.y + 15, startPos.z);
                _platformTrans.DOMoveY(startPos.y, 1f).SetEase(Ease.InOutExpo);
                break;
            case AnimateDirection.Up:
                _platformTrans.position = new Vector3(startPos.x, startPos.y - 15, startPos.z);
                _platformTrans.DOMoveY(startPos.y, 1f).SetEase(Ease.InOutExpo);
                break;
            case AnimateDirection.Backward:
                _platformTrans.position = new Vector3(startPos.x, startPos.y, startPos.z + 15);
                _platformTrans.DOMoveZ(startPos.z, 1f).SetEase(Ease.InOutExpo);
                break;
            case AnimateDirection.Forward:
                _platformTrans.position = new Vector3(startPos.x, startPos.y-2, startPos.z - 15);
                Sequence seq = DOTween.Sequence();
                seq.Append(_platformTrans.DOMoveZ(startPos.z, 0.5f).SetEase(Ease.InOutExpo));
                seq.Append(_platformTrans.DOMoveY(startPos.y, 1f).SetEase(Ease.InOutExpo));

                break;

        }

    }
    void ChangePlatformColor(string color) {
        switch (color) {
            case Colors.Red:
                _platformMeshRend.material = red;
                colorOfPlatform = Colors.Red;
                break;
            case Colors.Green:
                _platformMeshRend.material = green;
                colorOfPlatform = Colors.Green;
                break;
            case Colors.Blue:
                _platformMeshRend.material = blue;
                colorOfPlatform = Colors.Blue;
                break;
            case Colors.Black:
                _platformMeshRend.material = black;
                colorOfPlatform = Colors.Black;
                break;
            case Colors.Yellow:
                _platformMeshRend.material = yellow;
                colorOfPlatform = Colors.Yellow;
                break;
        }

    }

    //void OnCollisionEnter(Collision other) {
       
    //    if (other.gameObject.tag=="Player") {

    //        Debug.Log(colorOfPlatform);

    //        Dust.Instance.gameObject.SetActive(true);
    //        Dust.Instance.gameObject.transform.position = _playerTrans.position + Vector3.forward/2 ;
    //        Dust.Instance._dustMaterial.SetColor("_Color", _platformMeshRend.material.color);
    //        Invoke("DisableDustParticle",1f);
           
    //        if (player.color == colorOfPlatform) {
    //            SceneController.Instance.ChangeScore(10);
    //          //  Debug.Log("Equal Of Colors");
    //        }
    //        else {
    //            SceneController.Instance.ChangeScore(-5);
    //            SceneController.Instance.RemoveLife();
    //            // Debug.Log("Colors not Equal");
    //        }
    //    }
      

    //}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PaintBall") {

            ChangePlatformColor(other.gameObject.GetComponent<PaintBall>().color);
        }

        if (other.gameObject.tag == "Player") {
            _playerTrans.localRotation = new Quaternion(_platformTrans.rotation.x, 0, 0, 1);// _platformTrans.localRotation.;
          //  Debug.Log(colorOfPlatform);

           // Debug.Log(player.GetComponent<MovePlayer>().color);

            Dust.Instance.gameObject.SetActive(true);
            Dust.Instance.gameObject.transform.position = _playerTrans.position + Vector3.forward / 2;
            Dust.Instance._dustMaterial.SetColor("_Color", _platformMeshRend.material.color);
            Invoke("DisableDustParticle", 1f);

            if (player.color == colorOfPlatform) {
                SceneController.Instance.ChangeScore(10);
                  Debug.Log("Equal Of Colors");
            }
            else {
                Debug.Log("Colors not Equal");
                if (player.flashState==false) {
                    SceneController.Instance.RemoveLife();
                }
                SceneController.Instance.ChangeScore(-5);
               
               
            }
        }


    }

    void OnTriggerExit(Collider other) {

        if (other.gameObject.tag == "Player") {
            _playerTrans.localRotation = Quaternion.identity;
        }
   }

    void DisableDustParticle() {
        Dust.Instance.gameObject.SetActive(false);
    }
    //void OnControllerColliderHit(ControllerColliderHit other) {

    //    Debug.Log(other);
    //}
}
