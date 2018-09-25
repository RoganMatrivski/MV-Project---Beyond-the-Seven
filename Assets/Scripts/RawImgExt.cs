using UnityEngine.UI;
using UnityEngine;

using DG.Tweening;

public static class FlashExtensions
{
    public static Tween WhiteFlashFade(this RawImage img, float from, float to, float duration)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        Debug.Log("Fade Flash");

        var tempImg = img.gameObject.AddComponent<RawImage>();

        tempImg.enabled = true;

        tempImg.color = new Color(1, 1, 1, from);

        Tween tween = DOTween.ToAlpha(() => tempImg.color, x => tempImg.color = x, 1, duration);

        tween.onComplete += () => Object.Destroy(tempImg);

        return tween;
    }

    public static Tween BlackFlashFade(this RawImage img, float from, float to, float duration)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        Debug.Log("Fade Flash");

        var tempImg = img.gameObject.AddComponent<RawImage>();

        tempImg.enabled = true;

        tempImg.color = new Color(0, 0, 0, from);

        Tween tween = DOTween.ToAlpha(() => tempImg.color, x => tempImg.color = x, 1, duration);

        tween.onComplete += () => Object.Destroy(tempImg);

        return tween;
    }

    public static Tween WhiteFlashFade(this GameObject obj, float from, float to, float duration)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        Debug.Log("Fade Flash Obj");

        var tempObj = Object.Instantiate(obj, canvas.transform);
        tempObj.SetActive(true);
        RawImage tempImg = tempObj.GetComponent<RawImage>();

        tempImg.color = new Color(1, 1, 1, from);

        Tween tween = DOTween.ToAlpha(() => tempImg.color, x => tempImg.color = x, to, duration);

        tween.onComplete += () => Object.Destroy(tempObj);

        return tween;
    }

    public static Tween BlackFlashFade(this GameObject obj, float from, float to, float duration)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        Debug.Log("Fade Flash Obj");

        var tempObj = Object.Instantiate(obj, canvas.transform);
        tempObj.SetActive(true);
        RawImage tempImg = tempObj.GetComponent<RawImage>();

        tempImg.color = new Color(0, 0, 0, from);

        Tween tween = DOTween.ToAlpha(() => tempImg.color, x => tempImg.color = x, to, duration);

        tween.onComplete += () => Object.Destroy(tempObj);

        return tween;
    }

    public static GameObject WhiteDuplicate(this GameObject obj, float alpha)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        var tempObj = Object.Instantiate(obj, canvas.transform);
        tempObj.SetActive(true);
        RawImage tempImg = tempObj.GetComponent<RawImage>();

        tempImg.color = new Color(1, 1, 1, alpha);

        return tempObj;
    }

    public static GameObject BlackDuplicate(this GameObject obj, float alpha)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        var tempObj = Object.Instantiate(obj, canvas.transform);
        tempObj.SetActive(true);
        RawImage tempImg = tempObj.GetComponent<RawImage>();

        tempImg.color = new Color(0, 0, 0, alpha);

        return tempObj;
    }
}
