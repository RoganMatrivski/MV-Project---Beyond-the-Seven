using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class ConfigMenuInit : MonoBehaviour {

    public RawImage black;

    new public AudioSource audio;

    MasterTick masterTick;

    // Use this for initialization
    void Start () {
        masterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();

        //masterTick.onDelayComplete += onDelayFinish;

        onDelayFinish();

        //masterTick.RunTick();

    }

    void onDelayFinish()
    {
        DOTween.ToAlpha(() => black.color, x => black.color = x, 0, 2).onComplete += () => black.gameObject.SetActive(false);
        audio.DOFade(1, 2);
    }

    public void onFinish()
    {
        black.gameObject.SetActive(true);

        audio.DOFade(0, 2);
        DOTween.ToAlpha(() => black.color, x => black.color = x, 1, 2).onComplete += saveConfiguration.saveConf;
    }
}
