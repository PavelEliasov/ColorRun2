using UnityEngine;
using System.Collections;

public class EnableHelpPlane : MonoBehaviour {

    [SerializeField]
    GameObject Pointer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {

            Pointer.SetActive(true);
            StartCoroutine(Disable());

        }

    }

    IEnumerator Disable() {
        yield return new WaitForSeconds(1f);
        Pointer.SetActive(false);
    }
}
