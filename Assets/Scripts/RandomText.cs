using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RandomText : MonoBehaviour {

    public string[] randomtext;

	// Use this for initialization
	void Start () {
        GetComponent<TextMeshProUGUI>().SetText(randomtext[Random.Range(0, randomtext.Length - 1)]);
	}
}
