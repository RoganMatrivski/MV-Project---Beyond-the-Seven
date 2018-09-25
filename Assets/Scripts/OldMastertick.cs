using UnityEngine;
using System.Collections;

using UnityEngine.Serialization;

using System;

using UnityEngine.SceneManagement;

// The code example shows how to implement a metronome that procedurally generates the click sounds via the OnAudioFilterRead callback.
// While the game is paused or the suspended, this time will not be updated and sounds playing will be paused. Therefore developers of music scheduling routines do not have to do any rescheduling after the app is unpaused

[RequireComponent(typeof(AudioSource))]
public class OldMasterTick : MonoBehaviour
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
    [Range(-1000, 1000)]
    [SerializeField]
    //[HideInInspector]
    public int offset = 0;

    //public double bpm = 140.0F;
    //public float gain = 0.5F;
    //public int signatureHi = 4;
    //public int signatureLo = 4;
    private double nextTick = 0.0F;
    private double sampleRate = 0.0F;

    private bool running = false;

    [HideInInspector]
    public double timePerTick;

    [HideInInspector]
    public double timePerBeat;

    [SerializeField]
    AudioSource song;

    [SerializeField]
    float audioDelay = 4; //to give chance to totally load things.

    [SerializeField]
    [Range(0, 5)]
    public float audioRate = 1;

    //[HideInInspector]
    public int tick;

    [SerializeField]
    bool songPlayed = false;

    float time;

    double audioDSPTime;
    double sampleTime;
    private double sampleDelay;

    private double startAudioDSPTime;
    private double delayedAudioDSPTime;

    public double startADSP;
    public double delayADSP;

    private void OnEnable()
    {
        startADSP = AudioSettings.dspTime;

        SceneManager.sceneLoaded += onLoadFinish;

        runTick();
    }

    void onLoadFinish(Scene scene, LoadSceneMode mode)
    {
        delayADSP = AudioSettings.dspTime;
    }

    public void pauseAudio()
    {
        song.Pause();
        running = false;
    }

    public void playAudio()
    {
        song.UnPause();
        running = true;
    }

    void Start()
    {
        tick = 32;

        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        running = true;

        song = GetComponent<AudioSource>();

        onTickEvent += onTick;
        startAudioDSPTime = AudioSettings.dspTime;
    }

    public void runTick()
    {
        delayedAudioDSPTime = AudioSettings.dspTime;

        sampleDelay = (delayedAudioDSPTime - startAudioDSPTime + ((float)offset / 1000)) * sampleRate;

        song.enabled = true;
        song.Play();

        running = true;

        songPlayed = true;

        onDelayComplete?.Invoke();

        tick = 32;
    }


    private void Update()
    {
        sampleDelay = (audioDelay + ((float)offset / 1000)) * sampleRate;

        time += Time.deltaTime;

        //if (time > audioDelay && !songPlayed) //have no time to refactor things up, orz
        //{
        //    delayedAudioDSPTime = AudioSettings.dspTime;

        //    Debug.Log(delayedAudioDSPTime - startAudioDSPTime);

        //    song.enabled = true;
        //    song.Play();

        //    running = true;

        //    songPlayed = true;

        //    onDelayComplete?.Invoke();

        //    tick = 32;
        //}
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        audioDSPTime = AudioSettings.dspTime;

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            song.pitch = audioRate;
            Time.timeScale = audioRate;
        });

        timePerBeat = 60.0f / bpm;

        timePerTick = 60.0f / bpm / (float)Subdivide;

        double samplesPerTick = sampleRate * 60.0F / bpm * (1.0f / (double)Subdivide) / audioRate; // change 1.0F to
        double sample = (AudioSettings.dspTime + ((float)offset / 1000)) * sampleRate - sampleDelay;
        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            while (sample + n >= nextTick)
            {
                sampleTime = sample;
                //==========================

                nextTick += samplesPerTick;

                tick++;

                //if (onTickEvent != null)
                //    onTickEvent();

                onTickEvent?.Invoke(); //the same as the above
            }
            n++;
        }
    }

    void onTick()
    {
        //Debug.Log("Tick");
    }
}