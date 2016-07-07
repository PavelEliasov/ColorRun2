using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PaintBall : MonoBehaviour {
    [HideInInspector]
    public string color;

    public Material ballPathMaterial;

    //public Material red;
    //public Material green;
    //public Material blue;
    //public Material black;
    //public Material yellow;

    MeshRenderer paintBallMeshRend;
    Transform paintBallTransform;
    Rigidbody paintballRigigdbody;

    BallDirection fallingDirection;
    // Use this for initialization
    void Awake() {

        paintBallMeshRend = GetComponent<MeshRenderer>();
        paintBallTransform = GetComponent<Transform>();
        paintballRigigdbody = GetComponent<Rigidbody>();
        Debug.Log("Awake");
    }

    void Start () {
      

 }
	
	// Update is called once per frame
	void Update () {

	}

   public void ChangeColor(Color color,string colorName,BallDirection direct,Vector3 startpos) {
        // fallingDirection = direct;
        // Vector3 startpos = paintBallTransform.localPosition;
        paintBallMeshRend.material.SetColor("_Color",color+new Color(0,0,0,-0.3f));
        if (color == Color.black) {
            ballPathMaterial.SetColor("_TintColor", Color.grey);
        }
        else {
            ballPathMaterial.SetColor("_TintColor", color);
        }
        
       // Debug.Log(paintBallMeshRend.material.color);
        switch (colorName) {
            case Colors.Red:
              //  paintBallMeshRend.material = red;
                this.color = Colors.Red;
                break;
            case Colors.Green:
              //  paintBallMeshRend.material = green;
                this.color = Colors.Green;
                break;
            case Colors.Blue:
              //  paintBallMeshRend.material = blue;
                this.color = Colors.Blue;
                break;
            case Colors.Black:
              //  paintBallMeshRend.material = black;
                this.color = Colors.Black;
                break;
            case Colors.Yellow:
              //  paintBallMeshRend.material = yellow;
                this.color = Colors.Yellow;
                break;
        }

        switch (direct) {
            case BallDirection.left:
                paintBallTransform.DOMoveX(startpos.x-1,0.5f);
               
                break;
            case BallDirection.right:
                paintBallTransform.DOLocalMoveX(startpos.x+1, 0.5f);
                break;
            case BallDirection.forward:
                break;

        }
        paintBallTransform.DOMoveZ(startpos.z + 8, 0.5f);
        paintBallTransform.DOMoveY(startpos.y -20, 4f);
       // paintballRigigdbody.AddForce(Vector3.forward*1000+Vector3.down*500);

    }
}
