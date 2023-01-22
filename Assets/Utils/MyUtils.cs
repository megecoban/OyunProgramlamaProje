using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyUtils
{
    /*
     * AÇISAL ÝÞLEMLER ÝÇÝN BURADAN YARDIM ALINDI: //https://forum.unity.com/threads/clamping-angle-between-two-values.853771/#post-7726923
     */

    public static float ClampAngle(float current, float min, float max)
    {
        float dtAngle = Mathf.Abs(((min - max) + 180) % 360 - 180);
        float hdtAngle = dtAngle * 0.5f;
        float midAngle = min + hdtAngle;
        float offset = Mathf.Abs(Mathf.DeltaAngle(current, midAngle)) - hdtAngle;
        if (offset > 0)
            current = Mathf.MoveTowardsAngle(current, midAngle, offset);
        return current;
    }

    public static float CalculateCurrentSpeed(Vector3 currentPos, Vector3 previousPos)
    {
        return ((currentPos - previousPos) / Time.deltaTime).magnitude;
    }
}
