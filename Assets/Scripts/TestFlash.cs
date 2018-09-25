using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using DG.Tweening;

public class TestFlash : MonoBehaviour {

    public GameObject RawImg;

	// Use this for initialization
	void Start () {

    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Black.WhiteFlashFade(0, 1, 0.5f);
            RawImg.WhiteFlashFade(0, 1, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Black.WhiteFlashFade(0, 1, 0.5f);
            RawImg.BlackFlashFade(0, 1, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //Black.WhiteFlashFade(0, 1, 0.5f);
            RawImg.WhiteFlashFade(1, 0, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //Black.WhiteFlashFade(0, 1, 0.5f);
            RawImg.BlackFlashFade(1, 0, 0.5f);
        }
    }
}
