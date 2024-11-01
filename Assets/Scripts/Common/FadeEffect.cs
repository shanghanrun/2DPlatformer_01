using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public static class FadeEffect
{
    public static IEnumerator Fade(Tilemap target, float start, float end, float fadeTime=1, UnityAction action=null){
        if (target == null) yield break;

        float percent =0;

        while(percent <1){
            percent += Time.deltaTime /fadeTime;

            Color color = target.color;
            color.a     = Mathf.Lerp(start, end, percent);
            target.color = color;

            yield return null;
        }

        action?.Invoke(); // 등록된 action이 있나 확인하고 null이 아니면 실행
    }

    public static IEnumerator Fade(SpriteRenderer target, float start, float end, float fadeTime = 1, UnityAction action = null)
    {
        if (target == null) yield break;

        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime / fadeTime;

            Color color = target.color;
            color.a = Mathf.Lerp(start, end, percent);
            target.color = color;

            yield return null;
        }

        action?.Invoke(); // 등록된 action이 있나 확인하고 null이 아니면 실행
    }

    // Text 의 경우  알파값을 start에서 end로 바뀌게
    public static IEnumerator Fade(TextMeshProUGUI target, float start, float end, float fadeTime = 1, UnityAction action = null)
    {
        if (target == null) yield break;

        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime / fadeTime;

            Color color = target.color;
            color.a = Mathf.Lerp(start, end, percent);
            target.color = color;

            yield return null;
        }

        action?.Invoke(); // 등록된 action이 있나 확인하고 null이 아니면 실행
    }

    // Image 알파값을 바꾸기

    public static IEnumerator Fade(Image target, float start, float end, float fadeTime = 1, UnityAction action = null)
    {
        if (target == null) yield break;

        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime / fadeTime;

            Color color = target.color;
            color.a = Mathf.Lerp(start, end, percent);
            target.color = color;

            yield return null;
        }

        action?.Invoke(); // 등록된 action이 있나 확인하고 null이 아니면 실행
    }
}
