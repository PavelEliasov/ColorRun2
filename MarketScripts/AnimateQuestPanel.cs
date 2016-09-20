using UnityEngine;
using System.Collections;

public class AnimateQuestPanel : MonoBehaviour {
    Animator _animator;
	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

    public void RemovePanel() {
        _animator.SetBool("Remove",true);
    }
}
