using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UILevel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Sprite spriteLevelLock;
    [SerializeField] Image imageLevelIcon;
    [SerializeField] TextMeshProUGUI textLevel;
    [SerializeField] GameObject starBackgroundObject;
    [SerializeField] GameObject[] starObjects;

    int level;
    bool isUnlock;
    Image imageFadeScreen;

    public void SetLevel(int level, bool isUnlock, bool[] stars, Image imageFadeScreen){
        this.level = level;
        this.isUnlock = isUnlock;
        this.imageFadeScreen = imageFadeScreen;

        if( isUnlock){
            textLevel.enabled = true;
            textLevel.text = level.ToString();
        }
        else{
            imageLevelIcon.sprite = spriteLevelLock; // 자물쇠 이미지
            textLevel.enabled = false; 

            starBackgroundObject.SetActive(false); // 별 출력되지 않도록
        }

        for (int i=0; i< starObjects.Length; i++){
            starObjects[i].SetActive( stars[i]);
        }
    }

    public void OnPointerClick(PointerEventData data){
        if (isUnlock){
            imageFadeScreen.gameObject.SetActive(true); //이미지스프라이트를 가진 오브젝트
            StartCoroutine(FadeEffect.Fade(imageFadeScreen, 0,1,1, AfterFadeEffect));
        }
    }

    void AfterFadeEffect(){
        PlayerPrefs.SetInt(Constants.CurrentLevel, level);
        Utils.LoadScene(SceneNames.Game); // Game은 인덱스 2값
    }
}
