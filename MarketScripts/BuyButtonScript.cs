using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyButtonScript : MonoBehaviour {
    Animator animator;
    Button _button;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(AnimatePush);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void AnimatePush() {
        animator.SetBool("Pushed",true);
        StartCoroutine(DisablePushAnimate());
       // animator.SetBool("Pushed", false);
    }

    IEnumerator DisablePushAnimate() {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Pushed", false);
    }
}
