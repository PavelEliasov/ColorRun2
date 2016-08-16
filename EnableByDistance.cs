using UnityEngine;
using System.Collections;

public class EnableByDistance : MonoBehaviour {
    public enum EnableComponents {
        ParticleSystem,
        MeshRenderer
    }

    public EnableComponents ComponentForEnable;

    public float distanceForEnable;
    bool hide=true;

    Transform _playerTrans;
    Transform thisTrans;

	// Use this for initialization
	void Start () {
        switch (ComponentForEnable) {
            case EnableComponents.ParticleSystem:
                this.GetComponent<ParticleSystem>().GetComponent<Renderer>().enabled=false; 
                break;
            case EnableComponents.MeshRenderer:
                GetComponent<MeshRenderer>().enabled=false;
                break;
        }
        thisTrans = GetComponent<Transform>();
        _playerTrans = FindObjectOfType<MovePlayer>().transform;
	
	}
	
	// Update is called once per frame
	void Update () {
        if ((_playerTrans.position-thisTrans.position).magnitude<distanceForEnable && hide) {
            hide = false;
            switch (ComponentForEnable) {
                case EnableComponents.ParticleSystem:
                    this.GetComponent<ParticleSystem>().GetComponent<Renderer>().enabled = true;
                    break;
                case EnableComponents.MeshRenderer:
                    GetComponent<MeshRenderer>().enabled = true;
                    break;
            }

        }
	}
}
