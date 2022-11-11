using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem.iOS;

public class SpatulaController : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] private float distance = 0.3f;
    [SerializeField] private Transform liquidSurface;
    [SerializeField] private Transform target;
    [SerializeField] private bool doLerpPosition;
    [SerializeField] private float lerpPositionSpeed = 5f;
    [SerializeField] private float planeDepth = 0.02f;
    [SerializeField] private float radius = 0.115f;
    
    [Header("Liquid")]
    [SerializeField] private MeshRenderer potLiquid;
    [SerializeField] private float angularVelocityLerpSpeed = 5f;
    [SerializeField] private float maxAngularSpeed = 10f;
    
    
    private InputManager inputManager;

    private Vector3 previousPos;
    private Vector3 currentPos;
    private float currentVelocity;
    
    private Camera camera;
    private Plane plane;
    private Vector3 desiredPos;
    private Vector3 planeCenterPos;
    private void Awake()
    {
        inputManager = InputManager.instance;
        camera = Camera.main;
        planeCenterPos = liquidSurface.position + Vector3.down * planeDepth;
        plane = new Plane(Vector3.up, planeCenterPos);
    }

    private void Update()
    {
        MoveSpatula();
        UpdateLiquid();
    }

    private float AngleBetweenPointsArround(Vector3 pointA, Vector3 pointB, Vector3 center)
    {
        Vector3 CA = pointA - center;
        Vector3 CB = pointB - center;
        Quaternion angleA = Quaternion.LookRotation(CA,Vector3.up);
        Quaternion angleB = Quaternion.LookRotation(CB,Vector3.up);
        return Quaternion.Angle(angleA, angleB)*Mathf.Deg2Rad;
    }

    private void UpdateLiquid()
    {
        
        float distanceToCenter = (currentPos - planeCenterPos).magnitude;
        float angle = AngleBetweenPointsArround(previousPos, currentPos, planeCenterPos);
        float angularVelocity = angle / Time.deltaTime;
        float linearVelocity = angularVelocity * distanceToCenter;
        Debug.Log("angular : " + angularVelocity);
        Debug.Log("linear : " + linearVelocity);
        currentVelocity = Mathf.Lerp(currentVelocity,linearVelocity*8f, angularVelocityLerpSpeed * Time.deltaTime);
        currentVelocity = Mathf.Min(Mathf.Max(currentVelocity,-maxAngularSpeed), maxAngularSpeed);//limiting the velocity
        Debug.Log("currentVelocity : " + currentVelocity);
        float height = currentVelocity / maxAngularSpeed;
        UpdateLiquidMaterial(currentVelocity, height);
        
        /*Vector3 direction = (currentPos - planeCenterPos);
        float magnitude = direction.magnitude/radius;
        direction = direction.normalized;

        float x = direction.x;// - previousDirection.x;
        float y = direction.z;// - previousDirection.z;
        
        float angle = Mathf.Atan2(y,x);// [-PI;PI]

        Debug.Log("angleBetween = " + GetAngleDifference(angle, previousAngle));
        float rawAngularVelocity = GetAngleDifference(angle, previousAngle)/Time.deltaTime;
        testVelocity = rawAngularVelocity;
        
        previousAngularVelocity = Mathf.Lerp(previousAngularVelocity, rawAngularVelocity,Time.deltaTime * angularVelocityLerpSpeed);
        
        previousAngularVelocity = Mathf.Min(Mathf.Max(previousAngularVelocity,-maxAngularSpeed), maxAngularSpeed);
        //Debug.Log(rawAngularVelocity);
        float height = previousAngularVelocity / maxAngularSpeed;
        UpdateLiquidMaterial(currentVelocity, height);
        */
        
    }

    private float GetAngleDifference(float angle, float previous)
    {
        float a = angle;// + (float)Math.PI;
        float b = previous;// + (float)Math.PI;
        float diff = a - b;
        float other = 2*(float)Math.PI - diff;

        if (diff > 0)//Counter clockwise
        {
            Debug.Log("Clockwise");
            return diff >= other ? diff : -other;
        }
        //Clockwise
        Debug.Log("Counter clockwise");
        return diff <= other ? diff : -other;
    }

    private void UpdateLiquidMaterial(float angularVelocity, float height)
    {
        potLiquid.material.SetFloat("_RotateSpeed", angularVelocity);
        potLiquid.material.SetFloat("_Height", height);
    }

    private void MoveSpatula()
    {
        previousPos = currentPos;
        
        Vector3 pos = inputManager.PrimaryScreenPosition();
        if (Utils.PositionIsOnScreen(pos))
        {
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
                currentPos = Vector3.Lerp(currentPos, desiredPos, Time.deltaTime * lerpPositionSpeed);
            }
            else
            {
                currentPos = desiredPos;
            }

            currentPos = Utils.ConstraintPosToCircle(currentPos, planeCenterPos, radius);
            target.position = currentPos;
        }
        else
        {
            Debug.Log("pas bon");
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 tangentVector = Vector3.Cross(currentPos-planeCenterPos, Vector3.up);
        
        Gizmos.DrawRay(currentPos,tangentVector*currentVelocity*0.01f);

    }
}
