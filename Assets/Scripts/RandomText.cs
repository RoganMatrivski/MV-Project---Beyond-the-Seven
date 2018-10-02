using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RandomText : MonoBehaviour {

    private string text;

    public string[] randomtext;

	// Use this for initialization
	void Start () {
        text = GetComponent<TextMeshProUGUI>().text;

        text = randomtext[Random.Range(0, randomtext.Length - 1)];
	}
}
