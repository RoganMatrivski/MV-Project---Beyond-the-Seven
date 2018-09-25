using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[RequireComponent(typeof(AudioSource))]
public class MasterTick : MonoBehaviour
{
    public event Action onTickEvent;
    public event Action onDelayComplete;

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

    //==============================================================

    private float tempAudioRate;
    public void PauseTick()
    {
        tempAudioRate = audioRate;
        audioRate = 0;
    }

    public void RunTick()
    {
        audioRate = tempAudioRate;
    }

    // Use this for initialization
    void Start()
    {
        song = GetComponent<AudioSource>();

        onTickEvent += onTick;

        //================
        song.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = audioRate;

        song.pitch = audioRate;
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            timeSample = song.timeSamples;
            time = song.time;
        });

        if (audioRate > 0)
            timePerBeat = 60.0f / bpm / audioRate;
        else
            timePerBeat = 60.0f / bpm / audioRate;

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
