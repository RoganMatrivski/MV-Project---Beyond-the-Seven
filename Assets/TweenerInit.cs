using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class TweenerInit : MonoBehaviour
{
    void Start()
    {
        DOTween.Init().SetCapacity(5000, 50);
    }
}