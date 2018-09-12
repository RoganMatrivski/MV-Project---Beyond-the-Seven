using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerObject : MonoBehaviour {

    public float spinningSpeed = 1f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        //float rot = transform.rotation.eulerAngles.y + spinningSpeed;
        //transform.Rotate(new Vector3());
        transform.Rotate(new Vector3(spinningSpeed, 0, 0));

    }
}
