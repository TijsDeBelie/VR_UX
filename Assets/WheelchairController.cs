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

    private Rigidbody body;

    private TextMesh _textDisplay;

    // Start is called before the first frame update
    void Start()
    {
        //cache this for performance
        //_textDisplay = GetComponentInChildren<COMTextTest>().GetComponent<TextMesh>();
        body = GetComponent<Rigidbody>();
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
            //float vertical = Input.GetAxis("Vertical");
            //float horizontal = Input.GetAxis("Horizontal");
            //            transform.Translate(vertical * Time.deltaTime * baseSpeed * (int)speed * Vector3.forward);
            //            transform.Rotate(Time.deltaTime * turnAngle * horizontal * Vector3.up);
            //if (Math.Abs(vertical) < 0.1)
            //{
            //    backLeftWheel.brakeTorque = backRightWheel.brakeTorque =
            //        frontLeftWheel.brakeTorque = frontRightWheel.brakeTorque = 3000;
            //}
            //else
            //{
            //    backLeftWheel.motorTorque = backRightWheel.motorTorque = frontLeftWheel.motorTorque = frontRightWheel.motorTorque= vertical * baseSpeed * (int)speed ;
            //    backLeftWheel.brakeTorque = backRightWheel.brakeTorque =
            //        frontLeftWheel.brakeTorque = frontRightWheel.brakeTorque = 0;
            //}



            ////for now just set steering heading from horizontal axes
            ////TODO: CLAMP THIS
            //float steering = turnAngle * Input.GetAxis("Horizontal");
            //frontRightWheel.steerAngle = frontLeftWheel.steerAngle = steering;

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            // float dr is declared outside
            /** There is no diagonal rotation, atm. **/
            float dr = 30f;
            var velocity = new Vector3(0, (dr * h), 0);
            // not normalized, yet.
            
            body.MovePosition(transform.position + (new Vector3(v, 0, v) + body.rotation.eulerAngles * Time.deltaTime));
            var rotation = Quaternion.Euler(velocity * Time.deltaTime);
            body.MoveRotation(body.rotation * rotation);
        }
    }


    private void UpdateSpeeds(string rawSpeeds)
    {
        _textDisplay.text = rawSpeeds;
    }
}