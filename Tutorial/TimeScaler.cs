using UnityEngine;
using System.Collections;

public class TimeScaler : MonoBehaviour {
    [SerializeField]
    float timeScale;

    [SerializeField]
    float unscaleTime;
    // Use this for initialization
   

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Time.timeScale = timeScale;
            StartCoroutine(UnscaleTime());
        }
    }

 

    IEnumerator UnscaleTime() {
        yield return new WaitForSeconds(unscaleTime);
        Time.timeScale = 1;
    }
}
