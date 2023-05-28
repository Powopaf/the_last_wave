using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using CodeMonkey.Utils;
using UnityEngine;


public class Pointer : MonoBehaviour
{
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;
    public Camera camera;

    private void Awake()
    {
        targetPosition = GameObject.FindWithTag("Core").transform.position;
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        StartCoroutine(TimerRoutine());
        Vector3 toPosition = targetPosition;
        Vector3 formPosition = camera.transform.position;
        formPosition.z = 0f;
        Vector3 dir = (toPosition - formPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    private IEnumerator TimerRoutine()
    {
        yield return new WaitForSeconds(1);
    }
}
