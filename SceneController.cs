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

    public Statistic stats;//=new Statistic();

    float _score;
    float _starcount;
    int _lifecount=3;

    int sceneNumber;

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
         int.TryParse(SceneManager.GetActiveScene().name,out sceneNumber);
         stats = new Statistic();

       // Debug.Log(SceneManager.GetActiveScene().name);
    }
	
	// Update is called once per frame
	void Update () {

      //  Debug.Log(stats.Stars);
	}

    void OnDestroy() {
       // LevelComplete();
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

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag=="Player") {
            LevelComplete();
            SceneManager.LoadScene("Menu");
        }
       
    }
     void LevelComplete() {
        Managers._gameManager.Stats(sceneNumber, stats);
      //  Managers._gameManager.LevelsComplete = 1;

    }
}
