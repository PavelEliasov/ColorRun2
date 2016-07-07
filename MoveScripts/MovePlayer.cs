using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MovePlayer : MonoBehaviour {
    public enum State {
        Default,
        JetPack,
        Skate
    }
    public enum Pallette {
        RGB,
        RYB,//Red Yellow Black
        Yellow_Black
    }
    // Use this for initialization
    // public LayerMask whatIsGround;
    public GameObject Smoke;
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
    public float _gravity;
    float _verticalSpeed;
    float _fallingForce;
    [HideInInspector]
    public string color;

    bool jump;
    bool doubleJump = false;
    bool secondJump=false;
    bool grounded;

    [HideInInspector]
    public BallDirection ballDirect;

    public State _equipment = State.Default;

    public Pallette _pallette = Pallette.RGB;
    // Dictionary<Material, string> playerColor= new Dictionary<Material, string>();
    // public SkinnedMeshRenderer playerSkinnedMesh;

    public Material _CharacterMaterial;
    public Material _SmokeMaterial;
    public Material red;
    public Material green;
    public Material blue;
    public Material black;
    public Material yellow;

    public AudioClip [] groundedSounds;
    public AudioClip jumpsound;

    Vector3 startPos;
    Vector3 movement;

    MeshRenderer playerMesh;
    // public Material[] aMaterials;
    void Start() {

        Debug.Log(Managers._audioManager.SoundEffectVolume);

        Smoke.SetActive(false);
        // DustParticle.SetActive(false);
        // _dustMaterial=


        //  Debug.Log(_SmokeMaterial.color);
        ballDirect = BallDirection.forward;

        animator = GetComponent<Animator>();
        //  _material.SetColor("_Color",red.color);
        // _material = playerSkinnedMesh.material;
        playerMesh = GetComponent<MeshRenderer>();
        playerTrans = GetComponent<Transform>();
        playerRigidBody = GetComponent<Rigidbody>();
        _charcontroller = GetComponent<CharacterController>();
        _audioController = GetComponent<AudioSource>();

        // Debug.Log(_charcontroller.detectCollisions);

    }

    // Update is called once per frame
    void Update() {

        acceleration.text = Input.acceleration.x.ToString();

        if ((_charcontroller.isGrounded && jump == true) || doubleJump==true) { // ||  (grounded == true && Input.GetKeyDown(KeyCode.Space))) {
            if (doubleJump==true) {
                animator.SetBool("DoubleJump", true);
                secondJump = true;
            }
            doubleJump = false;
            _audioController.PlayOneShot(jumpsound,Managers._audioManager.SoundEffectVolume);
           // jump = true;
            //  Debug.Log("jump");
            // playerRigidBody.AddForce(Vector3.up*_jumpForce,ForceMode.Impulse);
            animator.SetBool("Jump",true);
          //  DustParticle.SetActive(false);
            Smoke.SetActive(true);
          //  Time.timeScale = 0.7f;
            StartCoroutine(ReturnTimeScale());
            grounded = false;

          
            startPos = playerTrans.position;
            ballDirect = BallDirection.forward;

            _verticalSpeed = _jumpForce;


        }
    

        if (Input.GetAxis("Horizontal")<-0.5f && jump==true || Input.acceleration.x<-0.15f && jump==true) {
            jump = false;
          //  Time.timeScale = 0.7f;
            StartCoroutine(ReturnTimeScale());
          
            playerTrans.transform.DOMoveX(Mathf.Round( playerTrans.position.x-1),1f);
            ballDirect = BallDirection.left;
        }

        if (Input.GetAxis("Horizontal") > 0.5f && jump==true || Input.acceleration.x > 0.15f && jump == true) {
            jump = false;
           // Time.timeScale = 0.7f;
            StartCoroutine(ReturnTimeScale());
            playerTrans.transform.DOMoveX(Mathf.Round(playerTrans.position.x + 1), 1f);
            ballDirect = BallDirection.right;
        }

        if (_charcontroller.isGrounded == false) {
            _verticalSpeed += _gravity * 2 * Time.deltaTime;
            _fallingForce += Time.deltaTime;

          //  Debug.Log(_fallingForce);
            if (_verticalSpeed <= _gravity) {
                _verticalSpeed = _gravity;
            }
           // Debug.Log(_verticalSpeed);
        } 

       
        movement = new Vector3(0,0, _speed);
        movement.y = _verticalSpeed;
        movement *= Time.deltaTime;
        _charcontroller.Move(movement);

        if (playerTrans.position.y>=2.5f) {
            Smoke.SetActive(false);
           // Debug.Log("Stop Smoke");
        }

        
        if (_charcontroller.isGrounded && grounded==false) {
            animator.SetBool("Jump",false);
            animator.SetBool("DoubleJump", false);
            Smoke.SetActive(false);
            jump = false;
            secondJump = false;
            grounded = true;
           // _fallingForce = 0;
           // Debug.Log(grounded);
        }
    //   Debug.Log(ballDirect);

    }

    void OnCollisionEnter(Collision other) {

        Debug.Log("Collision");
      
    }
    void OnTriggerEnter(Collider other) {

        Debug.Log("TriggerEnter");
        if (other.gameObject.tag=="Ground") {
            _audioController.PlayOneShot(groundedSounds[Random.Range(0,groundedSounds.Length)],Managers._audioManager.SoundEffectVolume*0.2f*_fallingForce);
            _fallingForce = 0;
        }
    }


    
    public void Jump() {


        switch (_equipment) {

            case State.Default:
                if (_charcontroller.isGrounded == false) {
                    return;
                }
                ChangeColor();
                jump = true;
                break;

            case State.JetPack:
                if (_charcontroller.isGrounded == false && secondJump == true) {
                    return;
                }
                if (_charcontroller.isGrounded == false) {
                    doubleJump = true;
                    return;
                }
                ChangeColor();
                jump = true;
                break;

            case State.Skate:
                break;

        }
     
       
        Messenger.Broadcast("CameraRotate");
        

    }

    void ChangeColor() {
        int rand = Random.Range(1, 4);

        if (_pallette==Pallette.RGB) {
             rand = Random.Range(1, 4);
        }
        if (_pallette == Pallette.RYB) {
             rand = Random.Range(3, 6);
        }
        if (_pallette==Pallette.Yellow_Black) {
            rand = Random.Range(4, 6);
        }
       

        switch (rand) {
          
            case 1:
                playerMesh.material = green;
                _CharacterMaterial.SetColor("_Color", green.color);
                _SmokeMaterial.SetColor("_TintColor", green.color);
               
             //   Debug.Log(_SmokeMaterial.color);
                color = Colors.Green;
                break;
            case 2:
                playerMesh.material = blue;
                _CharacterMaterial.SetColor("_Color", blue.color);
                _SmokeMaterial.SetColor("_TintColor", blue.color);
               
             //   Debug.Log(_SmokeMaterial.color);
                color = Colors.Blue;

                break;
            case 3:
                playerMesh.material = red;
                _CharacterMaterial.SetColor("_Color", red.color);
                _SmokeMaterial.SetColor("_TintColor", red.color);
                
            //    Debug.Log(_SmokeMaterial.color);
                color = Colors.Red;
                break;
            case 4:
                playerMesh.material = black;
                _CharacterMaterial.SetColor("_Color", black.color);
                _SmokeMaterial.SetColor("_TintColor", Color.grey);
              
            //    Debug.Log(_SmokeMaterial.color);
                color = Colors.Black;
                break;
            case 5:
                playerMesh.material = yellow;
                _CharacterMaterial.SetColor("_Color", yellow.color);
                _SmokeMaterial.SetColor("_TintColor", yellow.color);
               
              //  Debug.Log(_SmokeMaterial.color);
                color = Colors.Yellow;
                break;

        }

    }



    IEnumerator ReturnTimeScale() {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;

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
        var ball = Instantiate(PaintBall, playerTrans.position, Quaternion.identity) as GameObject;
        ball.GetComponent<PaintBall>().ChangeColor(color, colorName, ballDirect, startPos);
    }

    public void Startagain() {
        // System.GC.Collect();
      //  StateManager.playerPos = Vector3.zero;

      //  Debug.Log(StateManager.playerPos);
        SceneManager.LoadScene("1");
    }
}
