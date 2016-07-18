using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class EndOfLevel : MonoBehaviour {

    [SerializeField]
    Text bankCount;
    [SerializeField]
    Image[] stars;
   
	// Use this for initialization
	void Start () {
        this.gameObject.transform.localScale = Vector3.one * 0.1f;
       // gameObject.transform.localEulerAngles = Vector3.back * 360;
        gameObject.transform.DOLocalRotate(Vector3.back*360,1f,RotateMode.FastBeyond360);
        this.gameObject.transform.DOScale(Vector3.one,1f).SetEase(Ease.InExpo);
        StartCoroutine(CountingBanks());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    IEnumerator CountingBanks() {
        int banks=0;
        yield return new WaitForSeconds(1f);
        while (banks<=SceneController.Instance.stats.Banks) {
            if (banks>=SceneController.Instance.firstStarPrice) {
                stars[0].gameObject.SetActive(true);
            }
            if (banks >= SceneController.Instance.secondStarPrice) {
                stars[1].gameObject.SetActive(true);
            }
            if (banks >= SceneController.Instance.thirdStarPrice) {
                stars[2].gameObject.SetActive(true);
            }
            bankCount.text = "X " + banks.ToString();
            banks++;
            yield return new WaitForSeconds(0.1f);

        }
    }
}
