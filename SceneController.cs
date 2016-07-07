using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    static SceneController _instance;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Image[] lifeImages;


    float _score;
    float _starcount;
    int _lifecount=3;

   

    public static SceneController Instance {
        get  {
            if (_instance == null) {
                var container = FindObjectOfType<SceneController>();
                _instance = container;
            }
            return _instance;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeScore(float value) {
        _score += value;
        _score = _score < 0 ? 0 : _score;
        scoreText.text = _score.ToString();

    }

    public void RemoveLife() {
        lifeImages[_lifecount].enabled = false;
        _lifecount--;

        if (_lifecount<0) {
             Die();
        }
        
    }

    public void AddLife() {

    }
    public void Die() {
        SceneManager.LoadScene("1");
    }
}
