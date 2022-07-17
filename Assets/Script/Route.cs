using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>Bezier curve<summary>
[Serializable, CreateAssetMenu]
public class Route : ScriptableObject
{
    [SerializeReference]
    /// <summary> List of points</summary>
    private List<Vector3> controlPoints;
    /// <summary>Scale for initial points relative to the center</summary>
    private int scale = 7;

    /// <summary>Creates a Route from a center point</summary>
    public Route(Vector3 center)
    {
        controlPoints = new List<Vector3>{
					            // punto  A
                        center + Vector3.forward * scale,
								// Handler del punto A en diagonal hacia arriba
                        center + Vector3.left * scale + Vector3.forward,
								// Handler del punto B en diagonal hacia abajo
                        center + Vector3.right * scale + Vector3.back,
								// point B
                        center + Vector3.back * scale
        };

    }

    public Vector3 this[int i]
    {
        get => controlPoints[i];
    }

    /// <summary> Number of Points </summary>
    public int len => controlPoints.Count;

    /// <summary> Adds a segment using 3 points:
    ///   <list>
    ///   a new handler for the last point
    ///   handler of the new point (anchor)
    ///   the new point
    ///   </list>
    ///   </summary>
    public void AddSegment(Vector3 anchor)
    {
        var last = controlPoints[len - 1];
        var secondLast = controlPoints[len - 2];

        // Use the last point as midpoint and the second last for the direction
        controlPoints.Add(last * 2 - secondLast);
        // The midpoint between the last point and the new one
        controlPoints.Add(last + anchor * .5f);
        // The new point
        controlPoints.Add(anchor);
    }

    /// <summary> Number of segments </summary>
    public int segmentsLen
    {
        get
        {
            // No se cuentan el punto final ni el ultimo incluyendo sus handlers
            var total = len - 4;
            // Every three points counts as a segment
            return total / 3 + 1;
        }
    }

    /// <summary> Move the selected Point </summary>
    public void movePoint(int i, Vector3 pos)
    {
        Vector3 deltaMove = pos - controlPoints[i];
        controlPoints[i] = pos;
        // Is a Handler
        if (i % 3 == 0)
        {
            if (i + 1 < len)
            {
                controlPoints[i + 1] += deltaMove;
            }

            if (i - 1 >= 0)
            {
                controlPoints[i - 1] += deltaMove;
            }
        }
    }

    public void alignHorizontal()
    {

        var sum = 0f;
        for (int i = 0; i < len; i++)
        {
            sum += controlPoints[i].y;
        }

        sum = sum / len;

        for (int i = 0; i < len; i++)
        {
            var current = controlPoints[i];
            movePoint(i, new Vector3(current.x, sum, current.z));
        }
    }

    // <summary> Get current position from t value t E [0,1] </summary>
    public Vector3 getPosition(float input)
    {
        /* int segmentIndex = Mathf.FloorToInt(input); */
        /* int pointIndex = segmentIndex * 3; */
        /* float t = input - segmentIndex; */
        var pointIndex = 0;
        var t = input;

        float c = 1.0f - t;

        float bb0 = c * c * c;
        float bb1 = 3 * t * c * c;
        float bb2 = 3 * t * t * c;
        float bb3 = t * t * t;

        Vector3 point = controlPoints[pointIndex + 0] * bb0 + controlPoints[pointIndex + 1] * bb1 + controlPoints[pointIndex + 2] * bb2 + controlPoints[pointIndex + 3] * bb3;


        return point;
    }

    /// <summary>1st Derivate of bezier curve</summary>
    /// <returns> Speed <c>Vector3</c> at given point t E [1,0] </returns>
    public Vector3 getTanget(float input)
    {

        /* int segmentIndex = Mathf.FloorToInt(input); */
        /* int pointIndex = segmentIndex * 3; */
        /* float t = input - segmentIndex; */
        var pointIndex = 0;
        var t = input;

        Vector3 q0 = controlPoints[pointIndex] + ((controlPoints[pointIndex + 1] - controlPoints[pointIndex]) * t);
        Vector3 q1 = controlPoints[pointIndex + 1] + ((controlPoints[pointIndex + 2] - controlPoints[pointIndex + 1]) * t);
        Vector3 q2 = controlPoints[pointIndex + 2] + ((controlPoints[pointIndex + 3] - controlPoints[pointIndex + 2]) * t);

        Vector3 r0 = q0 + ((q1 - q0) * t);
        Vector3 r1 = q1 + ((q2 - q1) * t);

        Vector3 tangent = r1 - r0;

        return tangent;
    }

    public Vector3[] getSegmentFromIndex(int i)
    {

        return new Vector3[]{
                    controlPoints[i*3 + 0],
                    controlPoints[i*3 + 1],
                    controlPoints[i*3 + 2],
                    controlPoints[i*3 + 3]
        };
    }
}
