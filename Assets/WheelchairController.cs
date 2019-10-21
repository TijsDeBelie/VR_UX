using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class WheelchairController : MonoBehaviour
{
    public enum Mode
    {
        Manual,
        Electric
    }

    public Mode controlMode = Mode.Electric;
    public byte speedSetting;

    private TextMesh _textDisplay;

    // Start is called before the first frame update
    void Start()
    {
        //cache this for performance
        _textDisplay = GetComponentInChildren<COMTextTest>().GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlMode == Mode.Manual)
        {
            StartCoroutine(
                COMunicator.COMunicate
                (
                    "SPEEDS",
                    UpdateSpeeds
                )
            );
        }
    }


    void UpdateSpeeds(String rawSpeeds)
    {
        _textDisplay.text = rawSpeeds;
    }
}