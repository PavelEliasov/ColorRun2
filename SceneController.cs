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

    [SerializeField]
   public int firstStarPrice;
    [SerializeField]
   public int secondStarPrice;
    [SerializeField]
   public int thirdStarPrice;

    [HideInInspector]
    public Statistic stats;//=new Statistic();

    [SerializeField]
    GameObject endOfLevelPanel;

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
        StartCoroutine(Timer());
       Debug.Log(SceneManager.GetActiveScene().name);
    }
	
	// Update is called once per frame
	void Update () {

     //   Debug.Log(stats.Time);
        //Debug.Log(stats.Stars);
	}

    void OnDestroy() {
        Managers._gameManager.sceneNumber = sceneNumber;
    }

    public void ChangeScore(float value) {
        _score += value;
        _score = _score < 0 ? 0 : _score;
        scoreText.text = _score.ToString();

    }

    public void RemoveLife() {
        lifeImages[_lifecount-1].enabled = false;
        _lifecount--;

        if (_lifecount==0) {
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
          
        }
       
    }
     void LevelComplete() {
        CountStars();
        Managers._gameManager.Stats(sceneNumber, stats);
        endOfLevelPanel.SetActive(true);
        Debug.Log(stats.Stars);
      //  SceneManager.LoadScene("Menu");

        StopCoroutine(Timer());
      //  StartCoroutine(UnloadScene());
        //  Managers._gameManager.LevelsComplete = 1;

    }

    void CountStars() {
        if (stats.Banks>=firstStarPrice) {
            stats.Stars++;
        }
        if (stats.Banks>=secondStarPrice) {
            stats.Stars++;
        }
        if (stats.Banks>=thirdStarPrice) {
            stats.Stars++;
        }

    }

    IEnumerator UnloadScene() {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");

    }
    IEnumerator Timer() {
        yield return null;
        while (true) {
            stats.Time += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

    }
}
