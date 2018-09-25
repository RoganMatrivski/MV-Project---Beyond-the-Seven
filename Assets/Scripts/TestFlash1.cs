using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFlash1 : MonoBehaviour {
    MasterTick masterTick;

    public GameObject RawImg;

    void Start()
    {
        masterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();

        //masterTick.onDelayComplete += onDelayFinish;

        masterTick.onTickEvent += onTick;

    }

    void onTick()
    {
        if (masterTick.tick % 8 == 0)
            UnityMainThreadDispatcher.Instance().Enqueue(() => RawImg.WhiteFlashFade(1, 0, (float)masterTick.timePerBeat));
    }
}
