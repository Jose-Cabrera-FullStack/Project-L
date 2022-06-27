using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    private Transform[] ControlPoints;

    private Vector2 gizmosPosition;

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * ControlPoints[0].position +
                    3 * Mathf.Pow(1 - t, 2) * t * ControlPoints[1].position +
                    3 * (1 - t) * Mathf.Pow(t, 2) * ControlPoints[2].position +
                    Mathf.Pow(t, 3) * ControlPoints[3].position;

            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector2(ControlPoints[0].position.x, ControlPoints[0].position.y), new Vector2(ControlPoints[1].position.x, ControlPoints[1].position.y));
        Gizmos.DrawLine(new Vector2(ControlPoints[2].position.x, ControlPoints[2].position.y), new Vector2(ControlPoints[3].position.x, ControlPoints[3].position.y));

    }
}
