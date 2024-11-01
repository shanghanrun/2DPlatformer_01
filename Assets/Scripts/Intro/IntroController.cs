using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textPressAnyKey;
    [SerializeField] float          textBlinkTime = 0.5f;
    [SerializeField] Image          imageFadeScreen;

    bool                            isKeyDown = false;

    IEnumerator Start(){
        while(true){
            //  'Press Any Key'의 알파값을 1에서 0으로 textBlinkTime 동안 재생하고, 사라지게
            yield return StartCoroutine(FadeEffect.Fade(textPressAnyKey, 1, 0, textBlinkTime));
            yield return StartCoroutine(FadeEffect.Fade(textPressAnyKey, 0, 1, textBlinkTime)); // 결국 1초 주기로 깜빡거린다.
        }
    }

    void Update(){
        if ( isKeyDown){ // 이미 키를 눌렀던 상태 이후라면 아무 반응안함
            return;
        }

        if ( Input.anyKeyDown){
            isKeyDown = true;
            // imageFadeScreen의 알파값을 0에서 1로 1초동안 변화시킴, 그리고 action함수를 실행
            StartCoroutine(FadeEffect.Fade(imageFadeScreen, 0,1,1, AfterFadeEffect));
        }
    }

    void AfterFadeEffect(){
        Utils.LoadScene(SceneNames.SelectLevel);
    }
}
