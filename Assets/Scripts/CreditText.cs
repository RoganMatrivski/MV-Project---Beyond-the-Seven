using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

//[ExecuteInEditMode]
public class CreditText : MonoBehaviour {

    [SerializeField] bool DEBUG_ShowFinalPosition;

    public AudioSource audio;

    public GameObject img;

    public float finalPosition;
    public float timeInSecondsToComplete;

    private float time;

    private float tempPos;

    private bool triggered;

    private void Start()
    {
        triggered = false;
        tempPos = GetComponent<RectTransform>().localPosition.y;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        if (audio.time > audio.clip.length - audio.clip.length / 3 && !triggered) //yeah i know this is the roundabout way, right?
        {
            Debug.Log("Triggered");
            img.BlackFlashFade(0, 1, audio.clip.length / 3).onComplete += () =>
            {
                img.BlackDuplicate(1);
                Application.Quit();
            };
            triggered = true;
        }

        GetComponent<RectTransform>().localPosition = new Vector3(0, tempPos + Mathf.Lerp(0, finalPosition, audio.time / audio.clip.length));
    }
}
