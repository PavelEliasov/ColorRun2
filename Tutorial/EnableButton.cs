using UnityEngine;
using System.Collections;

public class EnableButton : MonoBehaviour {
    [SerializeField]
    GameObject _pinkButton;
    [SerializeField]
    GameObject _yelloowButton;

    [SerializeField]
    GameObject TutorialPanel;
    MovePlayer _player;

    static bool firstStartPanel;
    void Awake() {
        _player = FindObjectOfType<MovePlayer>();
    }
    // Use this for initialization
    void Start () {
        firstStartPanel = false;
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag=="Player") {

            if (_player.color==Colors.Red) {
                _pinkButton.SetActive(false);
                _pinkButton.SetActive(true);
            }
            if (_player.color == Colors.Yellow) {
                _yelloowButton.SetActive(false);
                _yelloowButton.SetActive(true);
                if (TutorialPanel != null  && firstStartPanel==false ) {
                    Time.timeScale = 0.1f;
                    TutorialPanel.SetActive(true);
                    StartCoroutine(UnscaleTime());
                    firstStartPanel = true;
                }
            }

        }

    }

    IEnumerator UnscaleTime() {
        yield return new WaitForSeconds(0.9f);
        TutorialPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
