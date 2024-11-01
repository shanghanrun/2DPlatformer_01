using UnityEngine; // PlayerPrefs 클래스를 사용하기 위해

public enum ItemType{
    Random =-1,
    Coin =0,
    Invincibility,
    HPPotion,
    Projectile,
    Star,
    Count
}

public class Constants{
    public static readonly int MaxLevel = 10;
    public static readonly int StarCount = 3;
    public static readonly string CurrentLevel = "CURRENT_LEVEL"; // 이것들은 PlayerPrefs에 문자열 넣기 위한 것
    public static readonly string LevelUnlock = "LEVEL_UNLOCK";
    public static readonly string LevelStar ="LEVEL_STAR_"; //현재 레벨 별 Key값
    public static readonly string Coin = "COINCOUNT";

    //레벨 정보를 불러올 때 호출하는 LoadLevelData
    public static (bool isUnlock, bool[] stars) LoadLevelData(int level){
        bool isUnlock = PlayerPrefs.GetInt($"{LevelUnlock}{level}", 0) == 1 ? true : false;

        bool[] stars = new bool[StarCount];
        for (int i=0; i< stars.Length; i++){
            stars[i] = PlayerPrefs.GetInt($"{LevelStar}{level}_{i}", 0) == 1 ? true: false;
        }

        return (isUnlock, stars); // 튜플로 반환
    }

    // 레벨을 클리어 했을 때 정보저장을 위해 호출하는 LevelComplete 메소드
    public static void LevelComplete(int level, bool[] stars, int coinCount){
        PlayerPrefs.SetInt(Coin, coinCount);

        if( level < MaxLevel){
            PlayerPrefs.SetInt($"{LevelUnlock}{level+1}",1); // 다음 레벨을 해금한다.
        }

        for( int i=0; i< StarCount; i++){
            PlayerPrefs.SetInt($"{LevelStar}{level}_{i}", stars[i] == true ? 1 : 0);
        } // 현재 레벨에서 획득한 별 정보 저장
    }
}