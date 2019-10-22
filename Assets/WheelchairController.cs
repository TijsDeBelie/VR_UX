using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;

public class WheelchairController : MonoBehaviour
{
    public enum Mode
    {
        Manual,
        Electric
    }

    public enum SpeedSetting
    {
        Slowest = 1,
        Slow = 2,
        Medium = 3,
        Fast = 5,
        Faster = 7
        
        
    }
    
    //we have wheels
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider backLeftWheel;
    public WheelCollider backRightWheel;
    
    //base speed modifier
    public float baseSpeed = 10;
    public float turnAngle = 30;

    //wheelchair modes
    public Mode controlMode;
    public SpeedSetting speed;

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
        else
        {
            //electric controls, vroom vroom!
            var speed = Input.GetAxis("Vertical") * baseSpeed * (int) this.speed;
            backLeftWheel.motorTorque = speed;
            
            //for now just set steering heading from horizontal axes
            //TODO: CLAMP THIS
            var angle = Input.GetAxis("Horizontal");
            frontRightWheel.steerAngle = frontLeftWheel.steerAngle = angle * turnAngle;
        }
    }


    private void UpdateSpeeds(string rawSpeeds)
    {
        _textDisplay.text = rawSpeeds;
    }
}