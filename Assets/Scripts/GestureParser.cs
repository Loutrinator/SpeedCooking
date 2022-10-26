using System;
using System.Collections;
using UnityEngine;

public class GestureParser : MonoBehaviour
{
    [SerializeField] private float minimumDistance = .2f;

    [SerializeField] private float maximumTime = 1f;

    
    private InputManager inputManager;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;
    private void Awake()
    {
        inputManager = InputManager.instance;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }
    

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }
    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        Debug.Log("Swipe tes morts");
        if (Vector2.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
        {
            Debug.DrawLine(startPosition,endPosition,Color.red,5f);
            Debug.Log("lets go");
        }
        
    }
}
