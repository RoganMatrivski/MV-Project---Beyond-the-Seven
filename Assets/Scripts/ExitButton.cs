using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour {

    [SerializeField]
    KeyCode exitKey = KeyCode.Escape;

    private void Start()
    {
        Application.quitting += () => Debug.Log("quitting");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(exitKey))
            Application.Quit();
	}
}
