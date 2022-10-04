using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCarousel : MonoBehaviour
{
    public float movingSpeed = 5f;
    public float rotationPercent = 1f;
    public float rotationScreenPercent = 1f;
    public float offsetScreenPercent = 1f;
    public RectTransform target;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }
    
    void Update()
    {
        float elapsed = Time.time - startTime;
        float animation = Mathf.Sin(elapsed * movingSpeed)*Mathf.PI*rotationPercent + Mathf.PI/2f;
        Vector3 previousPos = target.anchoredPosition;
        float size = rotationScreenPercent * Screen.height;
        float offset = offsetScreenPercent * Screen.height;
        previousPos.x = Mathf.Cos(animation)*size;
        previousPos.y = Mathf.Sin(animation)*size - offset;
        target.anchoredPosition = previousPos;
        //225,96,40
        //ScreenPointToWorldPointInRectangle(rect: RectTransform, screenPoint: Vector2, cam: Camera, worldPoint: Vector3): bool;
        //Vector2 myV2 = new Vector2(0,24);
        //Debug.Log (RectTransformUtility.ScreenPointToWorldPointInRectangle(myRectT, myV2, MainCam, out result));
    }
}
