using UnityEngine;

public class UIGamePopup : MonoBehaviour
{
    [Header("공통 : 검은 배경")]
    [SerializeField] GameObject overlayBackground;

    [Header("일시정지")]
    [SerializeField] GameObject popupPause;

    [Header("레벨 실패")]
    [SerializeField] GameObject popupLevelFailed;
    [Header("레벨 완료")]
    [SerializeField] GameObject popupLevelComplete;
    [SerializeField] GameObject[] starObjects;

    public void SetTimeScale(float scale){
        Time.timeScale = scale;
    }

    public void Pause(){
        SetTimeScale(0);
        overlayBackground.SetActive(true);
        popupPause.SetActive(true);
    }
    public void LevelFailed(){
        SetTimeScale(0);
        overlayBackground.SetActive(true);
        popupLevelFailed.SetActive(true);
    }
    public void LevelComplete(bool[] stars)
    {
        SetTimeScale(0);
        overlayBackground.SetActive(true);
        popupLevelComplete.SetActive(true);

        for(int i=0; i<starObjects.Length; i++){
            starObjects[i].SetActive(stars[i]);
        }
    }
    public void Resume(){
        SetTimeScale(1);
        overlayBackground.SetActive(false);
        popupPause.SetActive(false);
    }
    public void SelectLevel(){
        
        SetTimeScale(1); //! 0으로 하면 UI업데이트가 안되며 검게 나온다.
        Utils.LoadScene(SceneNames.SelectLevel);
    }
    public void Restart(){
        
        SetTimeScale(1);
        Utils.LoadScene(); // 현재 씬을 다시 로드
    }
    public void NextLevel(){
        SetTimeScale(1);

        int currentLevel = PlayerPrefs.GetInt(Constants.CurrentLevel);
        if (currentLevel >= Constants.MaxLevel) {//현재레벨이 마지막 레벨이라면
            SelectLevel(); // 레벨을 선택하는 곳으로 가고
        }
        else{
            PlayerPrefs.SetInt(Constants.CurrentLevel, currentLevel +1); 
            Utils.LoadScene(); // 현재는 다음레벨씬이 없다. 그래서 일단은 현재씬을 로드하게 한다.
        }
    }
    
}
