using UnityEngine;
using UnityEngine.UI;

public class SelectLevelController : MonoBehaviour
{
    [Header("Fade Effect")]
    [SerializeField] Image imageFadeScreen;

    //레벨정보를 표현하는 UI프리팹 저장을 위한
    [Header("Level UI")]
    [SerializeField] UILevel levelPrefab;
    [SerializeField] Transform levelParent;

    void Awake(){
        StartCoroutine(FadeEffect.Fade(imageFadeScreen, 1,0,1, AfterFadeEffect));
        LoadLevelData();
    }

    void AfterFadeEffect(){
        imageFadeScreen.gameObject.SetActive(false); //  비활성화해야 다른 오브젝트방해안함
    }
    void LoadLevelData(){
        //첫번째 레벨은 항상 해금되어 있게
        PlayerPrefs.SetInt($"{Constants.LevelUnlock}1", 1);

        for (int i=1; i<= Constants.MaxLevel; ++i){
            UILevel level = Instantiate(levelPrefab, levelParent);
            (bool isUnlock, bool[] stars) levelData = Constants.LoadLevelData(i);
            //(bool, bool[]) levelData = Constants.LoadLevelData(i);

            level.SetLevel(i, levelData.isUnlock, levelData.stars, imageFadeScreen);
            //level.SetLevel(i, levelData.Item1, levelData.Item2, imageFadeScreen);
        }
    }

    [ContextMenu("ResetData")]
    void ResetData(){
        PlayerPrefs.DeleteAll();
    }
}
