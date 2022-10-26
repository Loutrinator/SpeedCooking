using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMouse : MonoBehaviour
{
    [SerializeField] private float distance = 0.3f;
    [SerializeField] private Vector3 planePos;
    [SerializeField] private Transform target;
    [SerializeField] private bool doLerpPosition;
    [SerializeField] private float lerpPositionSpeed = 5f;
    
    
    private InputManager inputManager;

    private Vector3 worldPos = new Vector3();
    private Camera camera;
    private Plane plane;

    private void Awake()
    {
        inputManager = InputManager.instance;
        camera = Camera.main;
        plane = new Plane(-Vector3.forward, planePos);
    }

    private void Update()
    {
        Vector3 pos = inputManager.PrimaryScreenPosition();
        Vector3 desiredPos = new Vector3();
        pos.z = distance;
        Ray cameraRay = camera.ScreenPointToRay(pos);
        float enter;
        if (plane.Raycast(cameraRay, out enter))
        {
            //Get the point that is clicked
            desiredPos = cameraRay.GetPoint(enter);
        }
        
        if (doLerpPosition)
        {
            worldPos = Vector3.Lerp(worldPos, desiredPos, Time.deltaTime * lerpPositionSpeed);
        }
        else
        {
            worldPos = desiredPos;
        }
        target.position = worldPos;
    }
}
