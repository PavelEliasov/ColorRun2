using UnityEngine;
using System.Collections;

public class LightningAnimate : MonoBehaviour {
	Transform cylTrans;
	MeshRenderer _mesh;
	[SerializeField]
	Material _cylMat;
	public Texture [] _textures;
	Quaternion rot;
	float a;
	// Use this for initialization
	void Start () {
        _cylMat.mainTexture = _textures[0];
        rot = Quaternion.identity;
		cylTrans=GetComponent<Transform>();
		_mesh=GetComponent<MeshRenderer>();
		
	}

    void OnEnable() {
        StartCoroutine(OnOff());
       // Debug.Log("Enabled");
    }
	// Update is called once per frame
	void Update () {
		//a += Time.deltaTime*5;
		cylTrans.Rotate (0,50*Time.deltaTime,0);
		//cylTrans.localRotation =new Quaternion(0,a,0,1);
	}

	IEnumerator OnOff(){
		yield return new WaitForSeconds(0f);
		while(true){
			_mesh.enabled=!_mesh.isVisible;
			_cylMat.mainTexture=_textures[Random.Range(0,2)];
			yield return new WaitForSeconds(0.1f);

		}
	}
}
