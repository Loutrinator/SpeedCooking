using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIConstraint : MonoBehaviour
{
    public Vector2 scalePercent = Vector2.one;
    public Vector2 ratio = Vector2.one;
    public bool scaleOnX = false;
    public bool scaleOnY = false;
    public bool keepRatio = false;
    public bool overrideTargetToScreen = false;
    public bool runOnUpdate = false;
    public bool runOnStart = false;
    public RectTransform target;
    private RectTransform _rectT;
    void Start()
    {
        _rectT = GetComponent<RectTransform>();
        if(runOnStart)Constraint();
    }

    private void Update()
    {
        if(runOnUpdate)Constraint();
    }

    private void Constraint()
    {
        float width = 0;
        float height = 0;
        if (overrideTargetToScreen)
        {
            width = Screen.width;
            height = Screen.height;
        } else {
            if (target == null) return;
            width = target.rect.width;
            height = target.rect.height;
        }

        float finalWidth = scaleOnX ? width : keepRatio ? height * ratio.x / ratio.y : _rectT.sizeDelta.x;
        float finalHeight = scaleOnY ? height : keepRatio ? width * ratio.y / ratio.x : _rectT.sizeDelta.y;
        _rectT.sizeDelta = new Vector2(finalWidth, finalHeight) * scalePercent;
    }
}
