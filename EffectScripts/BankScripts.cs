using UnityEngine;
using System.Collections;

public class BankScripts : MonoBehaviour {
    [SerializeField]
    GameObject droplets;
    MeshRenderer _mesh;
    Collider _bankCollider;
	// Use this for initialization
	void Start () {
        _mesh = GetComponent<MeshRenderer>();

        _bankCollider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            _bankCollider.enabled = false;
            Debug.Log("Player");
            _mesh.enabled = false;
            droplets.SetActive(true);
            Destroy(this.gameObject, 0.5f);
        }

    }
    //void OnCollisionEnter(Collision other) {
    //    if (other.gameObject.tag == "Player") {
    //        Debug.Log("Player");
    //        _mesh.enabled = false;
    //        droplets.SetActive(true);
    //        Destroy(this.gameObject, 0.5f);
    //    }

    //}
}
