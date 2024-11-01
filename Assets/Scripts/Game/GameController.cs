using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] levelPrefabs;
    [SerializeField] StageData[] allStageData;
    [SerializeField] PlayerController playerController;
    [SerializeField] CameraFollowTarget cameraController;

    [SerializeField] UIGamePopup uiGamePopup;
    [SerializeField] PlayerData playerData;
    bool isLevelFailed = false;
    int currentLevel =1;
    bool isLevelComplete = false;

    void Awake(){
        currentLevel = PlayerPrefs.GetInt(Constants.CurrentLevel);
        playerData.Coin = PlayerPrefs.GetInt(Constants.Coin);

        GameObject level = Instantiate(levelPrefabs[currentLevel-1]); // 인덱스라서 -1
        ItemStar[] starObjects = level.GetComponentsInChildren<ItemStar>();

        var levelData = Constants.LoadLevelData(currentLevel);
        for (int i =0; i < levelData.stars.Length; i++){
            if( levelData.stars[i]){ // 불러온 현재 레벨의 i번째 값이 true이면 이미 획득한 별이라는 뜻으로
                playerData.GetStar(i);
                //월드맵에 배치되어 있는 별 오브젝트(starObjects)중 StarIndex가 i와 같은 별이 획득한 별이기 때문에
                // 게임에 보이지 않도록 비활성화한다.
                for(int j=0; j< starObjects.Length; j++){
                    if(starObjects[j].StarIndex == i) starObjects[j].gameObject.SetActive(false);
                }
            }
        }

        playerController.Setup(allStageData[currentLevel-1]);
        cameraController.Setup(allStageData[currentLevel-1]);
    }

    public void LevelFailed(){
        if (isLevelFailed){
            return;
        } else{
            isLevelFailed = true;

            uiGamePopup.LevelFailed();
        }
    }
    public void LevelComplete(){
        if( isLevelComplete) return;

        isLevelComplete = true;

        uiGamePopup.LevelComplete(playerData.Stars);
        Constants.LevelComplete(currentLevel, playerData.Stars, playerData.Coin);
    }
}
