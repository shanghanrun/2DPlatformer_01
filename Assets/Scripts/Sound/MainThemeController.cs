using UnityEngine;
using System.Collections;

public class MainThemeController : MonoBehaviour
{
    private AudioSource mainThemeAudio;
    private float fadeDuration = 180f;  // 페이드아웃할 시간 (초)
    private float fadeStartTime = 60f;  // 페이딩을 시작할 지점 (초)

    private void Start()
    {
        mainThemeAudio = GetComponent<AudioSource>(); // MainTheme의 AudioSource 컴포넌트를 바로 가져옴

        if (mainThemeAudio != null)
        {
            mainThemeAudio.Play();   // 음악 시작
            StartCoroutine(FadeOutAfterDelay());
        }
    }

    private IEnumerator FadeOutAfterDelay()
    {
        yield return new WaitForSeconds(fadeStartTime);  // 20초 대기

        float startVolume = mainThemeAudio.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            mainThemeAudio.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        mainThemeAudio.volume = 0;  // 볼륨을 0으로 설정
        mainThemeAudio.Stop();      // 음악 정지
    }
}
