using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using DG.Tweening;

using Cinemachine;

public class sceneAnimator_REDUX : MonoBehaviour {

    MasterTick MasterTick;

    public TestSetter fx;

    public GameObject spawnedObject;

    public GameObject[] crystals = new GameObject[3];
    public GameObject diamonds;

    int tick = 32;

    public bool counterclockwise = false;

    bool elapsed;

    // Use this for initialization
    void Start()
    {
        //fx = GameObject.FindGameObjectWithTag("VCam").GetComponent<TestSetter>();

        MasterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();
        MasterTick.onTickEvent += onTick;

        fx.blackMix = 1;
    }

    // Update is called once per frame
    void onTick()
    {
        Tween tempCont = null;

        var dispatch = UnityMainThreadDispatcher.Instance();

        tick = MasterTick.tick;
        switch (tick)
        {
            case 64:
                {
                    //Debug.Log("tick at 64");
                    dispatch.Enqueue(() =>
                    {
                        Debug.Log("Faded");

                        //blackScreen2.CrossFadeAlpha(0, (float)MasterTick.timePerBeat * 8, false);

                        DOTween.To(() => fx.blackMix, x => fx.blackMix = x, 0, (float)MasterTick.timePerBeat * 8);

                        //virtualCamera.GetCinemachineComponent<NoiseSettings>().PositionNoise.
                    });

                    break;
                }

            case 880:
                {
                    //Debug.Log("tick at 928");
                    dispatch.Enqueue(() =>
                    {
                        GameObject.FindGameObjectWithTag("RewindControl").GetComponent<rewindControl>().rewind = true;

                        //Debug.Log("triggered at 928");
                    });
                    break;
                }

            case 888:
                {
                    //Debug.Log("tick at 936");
                    dispatch.Enqueue(() =>
                    {
                        GameObject.FindGameObjectWithTag("RewindControl").GetComponent<rewindControl>().rewind = false;

                        //Debug.Log("triggered at 936");
                    });
                    break;
                }

            case 1008:
                {
                    //Debug.Log("tick at 928");
                    dispatch.Enqueue(() =>
                    {
                        GameObject.FindGameObjectWithTag("RewindControl").GetComponent<rewindControl>().rewind = true;

                        //Debug.Log("triggered at 928");
                    });
                    break;
                }

            case 1016:
                {
                    //Debug.Log("tick at 936");
                    dispatch.Enqueue(() =>
                    {
                        GameObject.FindGameObjectWithTag("RewindControl").GetComponent<rewindControl>().rewind = false;

                        //Debug.Log("triggered at 936");
                    });
                    break;
                }

            case 1088:
                {
                    dispatch.Enqueue(() =>
                    {
                        fx.blackMix = 1f;

                        //while (spawnedObject.transform.childCount > 1)
                        //{
                        //    Destroy(spawnedObject.transform.GetChild(0));
                        //}

                        for (int i = 0; i < spawnedObject.transform.childCount; i++)
                        {
                            if (i > 100)
                                break;

                            Destroy(spawnedObject.transform.GetChild(i).gameObject);
                            Debug.Log("Deleted Child");
                        }

                        DOTween.To(() => fx.blackMix, x => fx.blackMix = x, 0, (float)MasterTick.timePerBeat * 8);
                        diamonds.SetActive(false);
                        crystals[0].SetActive(true);
                    });
                    break;
                }

            case 1344: //Glow Crystal
                {
                    dispatch.Enqueue(() =>
                    {
                        crystals[0].GetComponent<Renderer>().material.DOFloat(5, "_obj_glow", (float)MasterTick.timePerBeat * 7);
                    });
                    break;
                }

            case 1576:
                {
                    dispatch.Enqueue(() =>
                    {
                        fx.whiteMix = 1f;
                        DOTween.To(() => fx.whiteMix, x => fx.whiteMix = x, 0, (float)MasterTick.timePerBeat);
                        //DOTween.To(() => whiteScreen.color, x => whiteScreen.color = x, whiteTransparent, (float)MasterTick.timePerBeat / 2);

                        crystals[0].SetActive(false);
                        crystals[1].SetActive(true);
                        Debug.Log("Triggered 1576, triggered at " + tick);
                    });
                    break;
                }

            case 1584:
                {
                    dispatch.Enqueue(() =>
                    {
                        fx.whiteMix = 1f;
                        DOTween.To(() => fx.whiteMix, x => fx.whiteMix = x, 0, (float)MasterTick.timePerBeat);
                        //DOTween.To(() => whiteScreen.color, x => whiteScreen.color = x, whiteTransparent, (float)MasterTick.timePerBeat / 2);

                        crystals[1].SetActive(false);
                        crystals[2].SetActive(true);
                        Debug.Log("Triggered 1584, triggered at " + tick);
                    });
                    break;
                }

            case 1592:
                {
                    dispatch.Enqueue(() =>
                    {
                        DOTween.To(() => fx.whiteMix, x => fx.whiteMix = x, 1, (float)MasterTick.timePerBeat);
                    });
                    break;
                }

            case 1600:
                {
                    dispatch.Enqueue(() =>
                    {
                        DOTween.To(() => fx.whiteMix, x => fx.whiteMix = x, 0, (float)MasterTick.timePerBeat*2);

                        crystals[2].SetActive(false);
                        diamonds.SetActive(true);
                    });
                    break;
                }

            case 1856: //Fade from white
                {
                    dispatch.Enqueue(() =>
                    {
                        fx.whiteMix = 1;
                        DOTween.To(() => fx.whiteMix, x => fx.whiteMix = x, 0, (float)MasterTick.timePerBeat*4);
                    });
                    break;
                }

            case 2304: //Fade to black
                {
                    dispatch.Enqueue(() =>
                    {
                        DOTween.To(() => fx.blackMix, x => fx.blackMix = x, 1, (float)MasterTick.timePerBeat*4);
                    });
                    break;
                }
        }
    }
}
