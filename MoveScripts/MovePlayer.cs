using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MovePlayer : MonoBehaviour {
    public enum State {
        Default,
        JetPack
    }
    public enum Pallette {
        Default,
        RGB,
        RYB,//Red Yellow Black
        Yellow_Black,
        YR,
        Blue_Green
    }
    // Use this for initialization
    // public LayerMask whatIsGround;
    RippleEffect _postEffect;
    //[SerializeField]
    //public GameObject doubleJumpEffect;
    [SerializeField]
    public GameObject Smoke;
    //[SerializeField]
    //GameObject SmokeFromSkate;
    public Text acceleration;
    Transform playerTrans;
    Rigidbody playerRigidBody;
    CharacterController _charcontroller;
    Animator animator;
    AudioSource _audioController;

    [SerializeField]
    GameObject PaintBall;



    public float _speed;
    public float _jumpForce;
    float _bounceforce=1.5f;
    public float _gravity;
    float _verticalSpeed;
    float _fallingForce;
    float _soundEffectVolume;
    [HideInInspector]
    public string color;

    bool jump;
    bool doubleJump = false;
    bool secondJump = false;
    bool trippleJump = false;
    bool grounded;
    bool isbounce;

    [HideInInspector]
    public BallDirection ballDirect;

    public State _jumpState = State.Default;

    public Pallette _pallette = Pallette.RGB;
    // Dictionary<Material, string> playerColor= new Dictionary<Material, string>();
    // public SkinnedMeshRenderer playerSkinnedMesh;


    public Material _CharacterMaterial;
    [SerializeField]
    Material _trailMaterial;
    public Material _SmokeMaterial;
    public Material red;
    public Material green;
    public Material blue;
    public Material black;
    public Material yellow;


    public bool magnetState;
    public bool flashState;

    [Header ("Effects")]
    [SerializeField]
    GameObject _lightningSphere;
    [SerializeField]
    GameObject _speedEffect;
    [SerializeField]
    GameObject _bootEffect;
    [SerializeField]
    GameObject _magnetEffect;

    public bool bootState;

    public AudioClip[] groundedSounds;
    public AudioClip jumpsound;
    public AudioClip doubleJumpSound;
    [SerializeField]
    AudioClip damagedSound;
    [SerializeField]
    AudioClip collectBank;
    [SerializeField]
    AudioClip collectflash;
    [SerializeField]
    AudioClip collectBoot;
    [SerializeField]
    AudioClip collectMagnet;
    [SerializeField]
    AudioClip dropBall;
    [SerializeField]
    AudioClip bounceSound;
    [SerializeField]
    AudioClip dieSound;

    Vector3 startPos;
    Vector3 movement;

    MeshRenderer playerMesh;

    Statistic stat;

    GameObject _jumpEffect;
    Transform _jumpeffectTrans;
    GameObject _doubleJumpEffect;
    Transform _doubleJumpTrans;
    GameObject _bounceEffect;
    Transform _bounceEffectTrans;


    bool hold=true;
    public float FallingForce  {
        get     {
            return _fallingForce;
        }

        set {
            if (value >= 1) {
                _fallingForce = 1f;
            }
            else {
                _fallingForce = value;
            }
            
        }
    }

    public float VerticalSpeed  {
        get {
            return _verticalSpeed;
        }

        set {
            _verticalSpeed = value;
        }
    }

    // public Material[] aMaterials;
    void Start() {
        ObjectPool.CreatePool(PaintBall,10);

        _postEffect = FindObjectOfType<RippleEffect>();
        Time.timeScale = 1;

        //-------------Particle Effects------------------
        _jumpEffect = JumpEffect.Instance.gameObject;
        _jumpeffectTrans = _jumpEffect.transform;
        _jumpEffect.SetActive(false);
        _doubleJumpEffect = DoubleJumpEffect.Instance.gameObject;
        _doubleJumpTrans = _doubleJumpEffect.transform;
        _doubleJumpEffect.SetActive(false);
        //_bounceEffect = BounceEffect.Instance.gameObject;
        //_bounceEffectTrans = BounceEffect.Instance.gameObject.transform;
        //_bounceEffect.SetActive(false);
        //-------------///////////////-------------------
        stat = new Statistic();//object for statistic

        Smoke.SetActive(false);
 
        color = IdentifyColor(_CharacterMaterial.color);
         Debug.Log(color);
        ballDirect = BallDirection.forward;

        animator = GetComponent<Animator>();
        //  _material.SetColor("_Color",red.color);
        // _material = playerSkinnedMesh.material;
        playerMesh = GetComponent<MeshRenderer>();
        playerTrans = GetComponent<Transform>();
        playerRigidBody = GetComponent<Rigidbody>();
        _charcontroller = GetComponent<CharacterController>();
        _audioController = GetComponent<AudioSource>();
        CheckState();
        Debug.Log(playerTrans.localRotation);
        _soundEffectVolume = Managers._audioManager.SoundEffectVolume;
    }

    void OnDestroy() {
        
        //Managers._gameManager.Stats(1, stat);
        //Managers._gameManager.LevelsComplete = 1;

     //   Debug.Log("Destroy");

    }

    void CheckState() {
        animator.SetBool("Run",Managers._itemManager.Default);
        animator.SetBool("Skate", Managers._itemManager.DressOnSkate);
        animator.SetBool("Rollers", Managers._itemManager.DressOnRollerSkate);
        animator.SetBool("Moto", Managers._itemManager.DressOnMoto);
      //  SmokeFromSkate.SetActive(Managers._itemManager.DressOnSkate);
    }
    string IdentifyColor(Color color) {
        //if (color.r > color.g && color.r > color.b) {

        //    return Colors.Red;

        //}

        //if (color.g > color.r && color.g > color.b && color.g - color.r > 0.2f) {

        //    return Colors.Green;

        //}

        //if (color.b > color.g && color.b > color.r) {

        //    return Colors.Blue;

        //}

        //if (color.b < 0.1f && color.g < 0.1f && color.r < 0.1f) {

        //    return Colors.Black;

        //}
        //if (color.g - color.r < 0.2f) {

        //    return Colors.Yellow;

        //}
        //return Colors.Yellow;
        if (Mathf.Abs(color.g - color.r) > 0.2f && color.r > color.g && color.r > color.b) {

            return Colors.Red;

        }

        if (color.g > color.r && color.g > color.b && color.g - color.r > 0.2f) {

            return Colors.Green;

        }

        if (color.b > color.g && color.b > color.r) {

            return Colors.Blue;

        }

        if (color.b < 0.1f && color.g < 0.1f && color.r < 0.1f) {

            return Colors.Black;

        }
        if (Mathf.Abs(color.g - color.r) < 0.2f) {

            return Colors.Yellow;

        }
        return Colors.Yellow;

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
      //  Debug.Log(stat.Stars);
       // Debug.Log(Managers._gameManager);
        //acceleration.text = Input.acceleration.x.ToString();

        if ((_charcontroller.isGrounded && jump == true) || doubleJump==true) { // ||  (grounded == true && Input.GetKeyDown(KeyCode.Space))) {
            if (doubleJump == true) {
                animator.SetBool("DoubleJump", true);
                secondJump = true;
                _audioController.PlayOneShot(doubleJumpSound, _soundEffectVolume);
            }
            else {
                _audioController.PlayOneShot(jumpsound, _soundEffectVolume);
               
            }


            if (isbounce) {
              
                if (doubleJump == true) {

                    Debug.Log(_verticalSpeed);
                    // _verticalSpeed += (_jumpForce - Mathf.Abs(_verticalSpeed) * 2f);
                    if (_verticalSpeed <= 2) {
                        _verticalSpeed = _jumpForce;
                    }
                    else {
                        Debug.Log(_verticalSpeed);
                        // _verticalSpeed = _jumpForce* _bounceforce *(_verticalSpeed/ (_jumpForce * _bounceforce));//double jump force in bounce
                        // Debug.Log(_verticalSpeed / (_jumpForce * 1.5f));
                        _verticalSpeed += ((_jumpForce*_bounceforce+_bounceforce*2f)/_verticalSpeed);
                       Debug.Log(_verticalSpeed);
                    }
                   
                }
                else {
                    _verticalSpeed = _jumpForce * _bounceforce;
                }
            }
            else {
                _verticalSpeed = _jumpForce;
            }

            

            doubleJump = false;

            animator.SetBool("Jump", true);
            
            Smoke.SetActive(true);

            if (Managers._itemManager.DressOnSkate) {
             //   SmokeFromSkate.SetActive(false);
            }
           // Time.timeScale = 0.8f;
           // StartCoroutine(ReturnTimeScale());
            grounded = false;

          
           // startPos = playerTrans.position;
            ballDirect = BallDirection.forward;




        }
        //if (Input.GetMouseButton(0) && hold==true) {
        //    _verticalSpeed += _jumpForce *5* Time.deltaTime;
        //    if (_verticalSpeed>=_jumpForce*1.5f) {

        //        Debug.Log("hold");
        //        hold = false;
        //    }

        //}

        //if (Input.GetAxis("Horizontal")<-0.5f && jump==true || Input.acceleration.x<-0.15f && jump==true) {
        //    jump = false;
        //  //  Time.timeScale = 0.7f;
        //  //  StartCoroutine(ReturnTimeScale());

        //    playerTrans.transform.DOMoveX(Mathf.Round( playerTrans.position.x-1),1f);
        //    ballDirect = BallDirection.left;
        //}

        //if (Input.GetAxis("Horizontal") > 0.5f && jump==true || Input.acceleration.x > 0.15f && jump == true) {
        //    jump = false;
        //   // Time.timeScale = 0.7f;
        //  //  StartCoroutine(ReturnTimeScale());
        //    playerTrans.transform.DOMoveX(Mathf.Round(playerTrans.position.x + 1), 1f);
        //    ballDirect = BallDirection.right;
        //}

        if (_charcontroller.isGrounded == false) {
          
            _verticalSpeed += _gravity *2 * Time.deltaTime;
            FallingForce += Time.deltaTime;

          //  Debug.Log(_fallingForce);
            if (_verticalSpeed <= _gravity) {
                _verticalSpeed = _gravity;
            }
           // Debug.Log(_verticalSpeed);
        }

        //if (flashState==true) {
        //    StartCoroutine(ReturnDefaultSpeed(_speed));
        //    flashState = false;
        //}
        movement = new Vector3(0,0, _speed);
        movement.y = _verticalSpeed;
        movement *= Time.deltaTime;
        _charcontroller.Move(movement);

        if (FallingForce>=0.3f) {
            Smoke.SetActive(false);
           // Debug.Log("Stop Smoke");
        }

        
        if (_charcontroller.isGrounded && grounded==false) {
            animator.SetBool("Jump",false);
            animator.SetBool("DoubleJump", false);
            Smoke.SetActive(false);
            if (Managers._itemManager.DressOnSkate) {
                //SmokeFromSkate.SetActive(true);
            }
           
            jump = false;
            secondJump = false;
            grounded = true;
           // _fallingForce = 0;
            Debug.Log(grounded);
        }
    //   Debug.Log(ballDirect);

    }

    void OnCollisionEnter(Collision other) {

        Debug.Log("Collision");
      
    }
    void OnTriggerEnter(Collider other) {
        // Managers._gameManager.LevelsComplete = 3;
        //  Debug.Log(Managers._gameManager.LevelsComplete);

        // Debug.Log(other.gameObject.tag);
        // Debug.Log("TriggerEnter");
        if (other.gameObject.tag == "Boot") {
            _audioController.PlayOneShot(collectBoot, _soundEffectVolume);
           // Debug.Log("Boot");
            Destroy(other.gameObject);
            _bootEffect.SetActive(true);
            trippleJump = true;
        }
        if (other.gameObject.tag == "Magnet") {
            magnetState = true;
            _audioController.PlayOneShot(collectMagnet, _soundEffectVolume);
            _magnetEffect.SetActive(true);
            StartCoroutine(ReturnMagnetState());
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Flash") {
            Destroy(other.gameObject);
            _audioController.PlayOneShot(collectflash, _soundEffectVolume);
            flashState = true;
            _postEffect.enabled = true;
            _postEffect.EmitEffectFlash(playerTrans.position);
            StartCoroutine(ReturnDefaultSpeed(_speed));
        }

        if (other.gameObject.tag == "Bank") {
            SceneController.Instance.stats.Banks++;
            _audioController.PlayOneShot(collectBank,_soundEffectVolume);
            //stat.Stars++;
          //  _postEffect.enabled = true;
          //  _postEffect.EmitEffectBank(playerTrans.position);
          //  Destroy(other.gameObject);
        }

        if (other.gameObject.tag=="Ground") {
            isbounce = false;
            _audioController.PlayOneShot(groundedSounds[Random.Range(0,groundedSounds.Length)],_soundEffectVolume*FallingForce);
            FallingForce = 0;
        }
        if (other.gameObject.tag == "BounceGround") {
            //_bounceEffect.SetActive(true);
            // StartCoroutine(BounceEffectDisable());

            Debug.Log(_charcontroller.isGrounded);
            _audioController.PlayOneShot(bounceSound, _soundEffectVolume);
            FallingForce = 0;
            Bounce();
        }
    }

    public void Damaged() {
        _audioController.PlayOneShot(damagedSound, _soundEffectVolume);
    }


    public void Jump() {//Call from JumpButton
        if (Time.timeSinceLevelLoad<0.5f) {
            return;
        }

        switch (_jumpState) {

            case State.Default:
                if (_charcontroller.isGrounded == false) {
                    return;
                }
                ChangeColor();
                jump = true;
                StartCoroutine(DefaultJumpEffect());//start derfault Jump Effect
                break;

            case State.JetPack:

                
                if (_charcontroller.isGrounded == false && secondJump == true) {
                 
                    if (trippleJump==true) {
                        StartCoroutine(SecondJumpEffect());
                        _bootEffect.SetActive(false);
                        trippleJump = false;
                        doubleJump = true;
                    }
                   
                    return;
                }
                if (_charcontroller.isGrounded == false) {
                    StartCoroutine(SecondJumpEffect());
                    doubleJump = true;
                    return;
                }
                ChangeColor();
                jump = true;
                StartCoroutine(DefaultJumpEffect());//start derfault Jump Effect

                break;

        }


        //   Messenger.Broadcast("CameraRotate");


    }

    void ChangeColor() {
        int rand = Random.Range(1, 4);

        //if (_pallette==Pallette.RGB) {
        //     rand = Random.Range(1, 4);
        //}
        //if (_pallette == Pallette.RYB) {
        //     rand = Random.Range(3, 6);
        //}
        //if (_pallette==Pallette.Yellow_Black) {
        //    rand = Random.Range(4, 6);
        //}

        switch (_pallette) {
            case Pallette.Default:
                rand = 10;
                break;
            case Pallette.RGB:
                rand = Random.Range(1, 4);
                break;
            case Pallette.RYB:
                rand = Random.Range(3, 6);
                break;
            case Pallette.Yellow_Black:
                rand = Random.Range(4, 6);
                break;
            case Pallette.YR:
                rand = RandomValue(new int [2]{ 3,5});
                break;
            case Pallette.Blue_Green:
                rand = Random.Range(1, 3);
                break;
        }
       
       
        switch (rand) {
          
            case 1:
                playerMesh.material = green;
                _CharacterMaterial.SetColor("_Color", green.color);
                _SmokeMaterial.SetColor("_TintColor", green.color * new Color(1, 1, 1, 0.7f));
               
             //   Debug.Log(_SmokeMaterial.color);
                color = Colors.Green;
                break;
            case 2:
                playerMesh.material = blue;
                _CharacterMaterial.SetColor("_Color", blue.color);
                _SmokeMaterial.SetColor("_TintColor", blue.color * new Color(1, 1, 1, 0.7f));
               
             //   Debug.Log(_SmokeMaterial.color);
                color = Colors.Blue;

                break;
            case 3:
                playerMesh.material = red;
                _CharacterMaterial.SetColor("_Color", red.color);
                _SmokeMaterial.SetColor("_TintColor", red.color*new Color(1,1,1,0.7f));
                _trailMaterial.SetColor("_Color", red.color * new Color(1, 1, 1, 0.4f));
                
            //    Debug.Log(_SmokeMaterial.color);
                color = Colors.Red;
                break;
            case 4:
                playerMesh.material = black;
                _CharacterMaterial.SetColor("_Color", black.color);
                _SmokeMaterial.SetColor("_TintColor", Color.grey * new Color(1, 1, 1, 0.7f));
              
            //    Debug.Log(_SmokeMaterial.color);
                color = Colors.Black;
                break;
            case 5:
                playerMesh.material = yellow;
                _CharacterMaterial.SetColor("_Color", yellow.color);
                _SmokeMaterial.SetColor("_TintColor", yellow.color * new Color(1, 1, 1, 0.7f));
                _trailMaterial.SetColor("_Color", yellow.color * new Color(1, 1, 1, 0.4f));
                //  Debug.Log(_SmokeMaterial.color);
                color = Colors.Yellow;
                break;
            case 10:
                playerMesh.material = red;
                _CharacterMaterial.SetColor("_Color", red.color);
                _SmokeMaterial.SetColor("_TintColor", red.color * new Color(1, 1, 1, 0.7f));
                _trailMaterial.SetColor("_Color", red.color * new Color(1, 1, 1, 0.4f));

                //  Debug.Log(_SmokeMaterial.color);
                color = Colors.Red;
                break;

        }

    }

    int RandomValue(int [] values) {

        return values[UnityEngine.Random.Range(0,values.Length)];
    }

    IEnumerator DefaultJumpEffect() {
        _jumpEffect.SetActive(true);
        _jumpeffectTrans.position = playerTrans.position + Vector3.forward;
        yield return new WaitForSeconds(0.5f);
        _jumpEffect.SetActive(false);
    }

    IEnumerator SecondJumpEffect() {
     // doubleJumpEffect.SetActive(true);
        _doubleJumpEffect.SetActive(true);
        _doubleJumpTrans.position= playerTrans.position + Vector3.forward+Vector3.up*0.5f;
        yield return new WaitForSeconds(0.5f);
      //  doubleJumpEffect.SetActive(false);
        _doubleJumpEffect.SetActive(false);

    }

    IEnumerator BounceEffectDisable() {
        // doubleJumpEffect.SetActive(true);
        _bounceEffect.SetActive(true);
        _bounceEffectTrans.position = playerTrans.position + Vector3.forward;
        yield return new WaitForSeconds(0.5f);
        //  doubleJumpEffect.SetActive(false);
        _bounceEffect.SetActive(false);

    }


    IEnumerator ReturnTimeScale() {
        yield return new WaitForSeconds(0.7f);
        Time.timeScale = 1f;

    }

    IEnumerator ReturnDefaultSpeed(float defspeed) {
        _speed = _speed *2f;
        _lightningSphere.SetActive(true);
        _speedEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        _speedEffect.SetActive(false);
        _speed = defspeed;
        yield return new WaitForSeconds(Managers._itemManager.Flash+2);
        _lightningSphere.SetActive(false);
       
        flashState = false;
      //  Debug.Log("Return Default Speed");
    }

    IEnumerator ReturnMagnetState() {
        yield return new WaitForSeconds((Managers._itemManager.Magnet+1)*1.5f);
        _magnetEffect.SetActive(false);
        magnetState = false;
    }

    public void RedButton() {
        DropBall(red.color, Colors.Red);
      
    }

    public void GreenButton() {
        DropBall(green.color, Colors.Green);
      
    }

    public void BlueButton() {
        DropBall(blue.color, Colors.Blue);
  
    }

    public void BlackButton() {
        DropBall(black.color, Colors.Black);

    }

    public void YellowButton() {
        DropBall(yellow.color, Colors.Yellow);

    }

    void DropBall(Color color,string colorName) {
        if (_charcontroller.isGrounded) {
            return;
        }
        _audioController.PlayOneShot(dropBall, Managers._audioManager.SoundEffectVolume);
       // var ball = Instantiate(PaintBall, playerTrans.position+Vector3.forward*0.5f, Quaternion.identity) as GameObject;
        var ball = ObjectPool.Spawn(PaintBall, playerTrans.position + Vector3.forward * 0.5f, Quaternion.identity);
        ball.GetComponent<PaintBall>().ChangeColor(color, colorName, ballDirect, playerTrans.position);
    }


    public void Bounce() {
        // Jump
        isbounce = true;
        StartCoroutine(JumpAfterGrounded());
        
         //_verticalSpeed = _jumpForce*2f;
    }
    IEnumerator JumpAfterGrounded() {
        //yield return new WaitForSeconds(0.0001f);
        yield return new WaitUntil(()=> _charcontroller.isGrounded);
        if (isbounce) {
            Jump();
        }
       
    }

    public void Die() {

        _audioController.PlayOneShot(dieSound,_soundEffectVolume);
    }
  

    public void Startagain() {
        // System.GC.Collect();
      //  StateManager.playerPos = Vector3.zero;

      //  Debug.Log(StateManager.playerPos);
        SceneManager.LoadScene("1");
    }

    public void GoToMenu() {
        SceneManager.LoadScene("Menu");
    }
}
