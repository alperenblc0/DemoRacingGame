using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeedMeter : MonoBehaviour
{
    public Rigidbody target;

    public float maxSpeed = 0.0f;

    public float minSpeedArrowAngle;
    public float maxaSpeedArrowAngle;

    [Header("UI")]
    public RectTransform arrow;

    private float speed = 0.0f;

    private void Update()
    {
        speed = target.velocity.magnitude * 3.6f;
        if (arrow != null)
        {
            arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxaSpeedArrowAngle, speed / maxSpeed));

        }
    }
}
