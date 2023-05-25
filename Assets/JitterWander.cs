using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JitterWander 
{
    private static float distance = 5;//20
    private static float radius = 2.5f;//10
    private static float jitter = 25;//100

    private static Vector2 target;
    private static Vector2 worldTarget;

    public static Vector3 Calculate(Transform predatorTransform)
    {
        target = Random.insideUnitCircle * radius;

        Vector2 disp = jitter * Random.insideUnitCircle * Time.deltaTime;
        target += disp;

        target = Vector3.ClampMagnitude(target, radius);

        Vector2 localTarget = (Vector2.up * distance) + target;

        worldTarget = predatorTransform.TransformPoint(localTarget);
        worldTarget.y = 0;

        return worldTarget - (Vector2)predatorTransform.position;
    }
}
