using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RectTransform))]
public class ItemCarrouselHolder : MonoBehaviour
{
    [SerializeField] private float respawnthreshold = 0.125f;
    public RectTransform rectTransform { get; private set; }
    public float currentRadialPosition;
    public void Set(float pos)
    {
        currentRadialPosition = pos;
    }
    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void UpdatePosition(float offset, float circleDiameter, float circleOffset, float spacing)
    {
        currentRadialPosition += offset;
        if (currentRadialPosition > respawnthreshold)// && currentRadialPosition < 1f - respawnthreshold)
        {
            Debug.Log("LA COUY");
            currentRadialPosition = -respawnthreshold;
        }
        float elapsed = 0f;//Time.time - startTime;
        float animation = currentRadialPosition*2f*Mathf.PI + Mathf.PI / 2f;
        Vector3 previousPos = rectTransform.anchoredPosition;
        previousPos.x = Mathf.Cos(animation)*circleDiameter;
        previousPos.y = Mathf.Sin(animation)*circleDiameter - circleOffset;
        rectTransform.anchoredPosition = previousPos;
    }
}
