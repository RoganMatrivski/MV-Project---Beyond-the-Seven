﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class ConfigMenuInit : MonoBehaviour {

    public RawImage black;

    new public AudioSource audio;

    MasterTick masterTick;

    public double startADSP;

    public double delayADSP;

    // Use this for initialization
    void Start () {
        masterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();

        //masterTick.onDelayComplete += onDelayFinish;

        StartCoroutine(run());

    }

    private void OnEnable()
    {
        startADSP = AudioSettings.dspTime;

        SceneManager.sceneLoaded += onLoadFinish;
    }

    void onLoadFinish(Scene scene, LoadSceneMode mode)
    {
        delayADSP = AudioSettings.dspTime;

        onDelayFinish();
    }

    IEnumerator run()
    {

        masterTick.RunTick();

        yield return null;
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