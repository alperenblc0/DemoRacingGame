using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carmovement : MonoBehaviour
{
    private float hInput;
    private float vInput;
    private float angle;


    public WheelCollider frw, flw;
    public WheelCollider brw, blw;


    public Transform frwt, flwt;
    public Transform brwt, blwt;


    public float maxSteerAngle = 30f;
    public float maxMotorForce = 300f;
    public float maxBreakForce = 500f;


    public AudioSource accelerateSound;
    public AudioSource BreakSound;
    public AudioSource revarseSound;
    public AudioClip breakClip;


    public Light breakLightLeft;
    public Light breakLightRight;
    public Light revarseLightLeft;
    public Light revarseLightRight;
    public Light LeftSpot;
    public Light RightSpot;


    public ParticleSystem accelerationEffect;



    void Start()
    {

    }


    void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPosition();

        #region Break
        if (Input.GetKey(KeyCode.Space))
        {
            frw.brakeTorque = maxBreakForce;
            flw.brakeTorque = maxBreakForce;
            brw.brakeTorque = maxBreakForce;
            blw.brakeTorque = maxBreakForce;

            breakLightLeft.enabled = true;
            breakLightRight.enabled = true;
        }
        else
        {
            frw.brakeTorque = 0;
            flw.brakeTorque = 0;
            brw.brakeTorque = 0;
            blw.brakeTorque = 0;

            breakLightLeft.enabled = false;
            breakLightRight.enabled = false;
        }
        #endregion

        #region spotlight
        if (Input.GetKey(KeyCode.L))
        {
            LeftSpot.enabled = true;
            RightSpot.enabled = true;
        }
        else
        {
            LeftSpot.enabled = false;
            RightSpot.enabled = false;
        }
        #endregion

        #region accelerateSound
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            accelerateSound.enabled = true;

            accelerationEffect.Play();
        }
        else
        {
            accelerateSound.enabled = false;

            accelerationEffect.Stop();
        }
        #endregion

        #region BreakSound
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BreakSound.PlayOneShot(breakClip, 1f);
        }
        #endregion

        #region RevarseSound
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            revarseSound.enabled = true;

            revarseLightLeft.enabled = true;
            revarseLightRight.enabled = true;
        }
        else
        {
            revarseSound.enabled = false;

            revarseLightLeft.enabled = false;
            revarseLightRight.enabled = false;
        }
        #endregion


    }

    private void GetInput()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.R))
        {
            resetCarRotation();
        }
    }
    private void Steer()
    {
        angle = maxSteerAngle * hInput;
        frw.steerAngle = angle;
        flw.steerAngle = angle;
    }
    private void Accelerate()
    {
        frw.motorTorque = maxMotorForce * vInput;
        flw.motorTorque = maxMotorForce * vInput;
    }
    private void UpdateWheelPosition()
    {
        UpdateWheelPos(frw, frwt);
        UpdateWheelPos(flw, flwt);
        UpdateWheelPos(brw, brwt);
        UpdateWheelPos(blw, blwt);
    }
    private void UpdateWheelPos(WheelCollider col, Transform trs)
    {
        Vector3 pos = trs.position;
        Quaternion quat = trs.rotation;

        col.GetWorldPose(out pos, out quat);

        trs.position = pos;
        trs.rotation = quat;

    }

    private void resetCarRotation()
    {
        Quaternion rotation = transform.rotation;
        rotation.z = 0f;
        rotation.x = 0f;
        transform.rotation = rotation;
    }
}
