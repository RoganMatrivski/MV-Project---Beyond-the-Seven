using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[RequireComponent(typeof(AudioSource))]
public class MasterTick : MonoBehaviour
{
    public event Action onTickEvent;
    public event Action onDelayComplete;

    [Header("Play Option")]
    [Tooltip("Play After Wake")]
    [SerializeField]
    bool playAfterWake = true;

    [Header("Time Settings")]
    [Tooltip("Use Standard Time Instead of Music Time")]
    [SerializeField]
    bool useStandardTime = false;

    [Header("BPM Settings")]
    [Tooltip("Beats Per Minutes")]
    [Range(30, 240)]
    [SerializeField]
    public double bpm = 140.0F;

    //[Space]
    [Tooltip("Sub-Beats per Beat")]
    [Range(1, 16)]
    [SerializeField]
    public int Subdivide = 1;

    [Tooltip("Offsets")]
    [Range(-500, 500)]
    [SerializeField]
    //[HideInInspector]
    public int offset = 0;

    [SerializeField]
    AudioSource song;

    private bool running = false;

    [HideInInspector]
    public double timePerTick;

    [HideInInspector]
    public double timePerBeat;

    [SerializeField]
    [Range(0, 5)]
    public float audioRate = 1;

    //[HideInInspector]
    public int tick = 32;

    [SerializeField]
    bool songPlayed = false;

    public float time;
    public int timeSample;

    private double nextTick;

    //private float tempoffset;

    //==============================================================

    private float tempAudioRate = 1;
    public void PauseTick()
    {
        tempAudioRate = audioRate;
        audioRate = 0;
    }

    public void RunTick()
    {
        audioRate = tempAudioRate;
    }

    public void InitRunTick()
    {
        song.Play();
    }

    // Use this for initialization
    void Start()
    {
        if (!useStandardTime)
        {
            song = GetComponent<AudioSource>();

            onTickEvent += onTick;

            //================
            if (playAfterWake)
                song.Play();
        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.timeScale);
        if (!useStandardTime)
        {
            Time.timeScale = audioRate;
            song.pitch = audioRate;
        }
        else
        {
            ticker();
        }
    }

    void ticker() // The same as the one implemented below, but for standard timing
    {
        time += Time.deltaTime;

        timePerBeat = 60.0f / bpm;
        timePerTick = timePerBeat / (float)Subdivide;

        if (time > nextTick + (float)offset / 1000)
        {
            nextTick += timePerTick;
            tick++;
            onTickEvent?.Invoke();
        }
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        //tempoffset = (float)offset / 1000;
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            timeSample = song.timeSamples;
            time = song.time + (float)offset / 1000;
        });

        if (audioRate > 0)
            timePerBeat = 60.0f / bpm;
        else
            timePerBeat = 60.0f / bpm;

        timePerTick = timePerBeat / (float)Subdivide;

        if (time > nextTick)
        {
            nextTick += timePerTick;

            tick++;

            onTickEvent?.Invoke();
        }
    }

    void onTick()
    {
        Debug.Log("tick");
    }
}
