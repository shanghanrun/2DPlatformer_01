using UnityEngine.SceneManagement;
using UnityEngine;

public enum SceneNames { Intro=0, SelectLevel, Game}

public static class Utils
{
    public static string GetActiveScene(){
        return SceneManager.GetActiveScene().name;
    }

    public static void LoadScene(string sceneName=""){
        if (sceneName == ""){
            SceneManager.LoadScene(GetActiveScene());
        }
        else{
            SceneManager.LoadScene(sceneName);
        }
    }

    public static void LoadScene(SceneNames sceneName){ //이것은 Utils.LoadScene 메소드이다.
        // SceneNames 열거형으로 매개변수를 받아온 경우에는 ToString() 처리를 한다.(열거형은 인덱스 숫자)
        Debug.Log("sceneName.ToString()  " + sceneName.ToString());
        SceneManager.LoadScene(sceneName.ToString());
        
    }
}
