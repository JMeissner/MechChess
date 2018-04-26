using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    public float RotationSpeed = 2.0f;

    public void RotateRight()
    {
        StartCoroutine(RotateRightCoroutine());
    }

    public void RotateLeft()
    {
        StartCoroutine(RotateLeftCoroutine());
    }

    public IEnumerator RotateRightCoroutine() 
    {
        float endTime = Time.time + RotationSpeed;
        float step = 1f / RotationSpeed;
        Vector3 fromAngle = transform.eulerAngles;
        Vector3 targetRot = transform.eulerAngles + new Vector3(0, -90, 0);
        float t = 0;

        while(Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            yield return 0;
        }
    }

    public IEnumerator RotateLeftCoroutine()
    {
        float endTime = Time.time + RotationSpeed;
        float step = 1f / RotationSpeed;
        Vector3 fromAngle = transform.eulerAngles;
        Vector3 targetRot = transform.eulerAngles + new Vector3(0, 90, 0);
        float t = 0;

        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            yield return 0;
        }
    }
}
