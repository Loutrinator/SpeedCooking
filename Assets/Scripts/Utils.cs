using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = 0.3f;//camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }

    public static bool PositionIsOnScreen(Vector3 pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x <= Camera.main.pixelWidth && pos.y <= Camera.main.pixelHeight;
    }

    public static Vector3 ConstraintPosToCircle(Vector3 pos, Vector3 circleCenter, float radius)
    {
        Vector3 A = new Vector3(pos.x, 0, pos.z);
        Vector3 B = new Vector3(circleCenter.x, 0, circleCenter.z);
        Vector3 direction = A - B;
        float dist = direction.magnitude;
        dist = Mathf.Min(dist, radius);
        Vector3 newPos = direction.normalized * dist;
        newPos.y = pos.y;
        return newPos;
    }
}
