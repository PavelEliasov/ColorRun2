using UnityEngine;
using System.Collections;

public class PhoneEnable : MonoBehaviour {
    PhoneAnimate phone;
    // Use this for initialization
    void Start() {
        phone = FindObjectOfType<PhoneAnimate>();
        phone.gameObject.SetActive(false);
        //Debug.Log(phone);
    }

    // Update is called once per frame
    //void Update() {

    //}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            phone.gameObject.SetActive(true);

        }
    }
}
