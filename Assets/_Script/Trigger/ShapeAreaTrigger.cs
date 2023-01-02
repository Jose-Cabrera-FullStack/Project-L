using System;
using UnityEngine;
using UnityEditor;

public class ShapeAreaTrigger : MonoBehaviour
{
    public Shape shape;
    public enum Shape
    {
        Wedge,
        Cone,
        Circular
    }
    [SerializeField]
    Transform target;
    // Serialized fields for shape-specific parameters

    [SerializeField] float radiusInner;

    [SerializeField] float radius = 1;

    [SerializeField] float height = 1;

    [SerializeField] float radiusOuter;
    [Range(0, 180), SerializeField] float fovDeg = 45;

    // Calculate the field of view angle in radians
    float _fovRad => fovDeg * Mathf.Deg2Rad;

    // Calculate the cosine of half the field of view angle in radians
    float _angThresh => Mathf.Cos(_fovRad / 2);

    // Method to check if a position is contained within the trigger shape
    public bool IsContained(Vector3 position) =>
        shape switch
        {
            Shape.Circular => isCircularContained(position),
            Shape.Cone => isConeContained(position),
            Shape.Wedge => isWedgeContained(position),
            _ => throw new NotImplementedException(),
        };

    // Method to set the gizmo and handle matrices
    void setGizmoMatrix(Matrix4x4 m) => Gizmos.matrix = Handles.matrix = m;

    void OnDrawGizmos()
    {
        // Set the gizmo and handle matrices to the local-to-world matrix of the game object
        setGizmoMatrix(transform.localToWorldMatrix);
        Gizmos.color = Handles.color = IsContained(target.position) ? Color.white : Color.red;
        switch (shape)
        {
            case Shape.Circular:
                drawCircularGizmo();
                break;
            case Shape.Cone:
                drawConeGizmo();
                break;
            case Shape.Wedge:
                drawWedgeGizmo();
                break;
        }
    }

    bool isConeContained(Vector3 position)
    {
        // Check if the position is contained within the circular area of the cone
        if (isCircularContained(position) == false) return false;

        Vector3 dirToTarget = (position - transform.position).normalized;

        // Calculate the angle between the forward direction of the trigger and the direction to the position
        float angleRad = angleBetweenNormilizedVectors(transform.forward, dirToTarget);

        // Return true if the angle is within the field of view of the trigger
        return angleRad < _fovRad / 2;
    }

    static float angleBetweenNormilizedVectors(Vector3 a, Vector3 b)
    {
        // Clamp the result to the range [-1, 1] and return the arccosine of the dot product of the vectors
        return Mathf.Clamp(Mathf.Acos(Vector3.Dot(a, b)), -1, 1);
    }

    void drawConeGizmo()
    {
        Vector3 top = new Vector3(0, height, 0);

        // Calculate the x and y components of a point on the edge of the field of view circle
        float p = _angThresh;
        float x = Mathf.Sqrt(1 - p * p);

        // Calculate the direction vectors for the left and right points on the edge of the circle
        Vector3 vLeftDir = new Vector3(-x, 0, p);
        Vector3 vRightDir = new Vector3(x, 0, p);

        // Calculate the outer and inner points on the left and right sides of the cone
        Vector3 vLeftOuter = vLeftDir * radiusOuter;
        Vector3 vRightOuter = vRightDir * radiusOuter;
        Vector3 vLeftInner = vLeftDir * radiusInner;
        Vector3 vRightInner = vRightDir * radiusInner;

        // arcs
        void drawFlatWedge()
        {
            Handles.DrawWireArc(default, Vector3.up, vLeftOuter, fovDeg, radiusOuter);
            Handles.DrawWireArc(default, Vector3.up, vLeftOuter, fovDeg, radiusInner);
            Gizmos.DrawLine(vLeftInner, vLeftOuter);
            Gizmos.DrawLine(vRightInner, vRightOuter);

        }

        drawFlatWedge();
        Matrix4x4 prevMtrx = Gizmos.matrix;
        // Temporarily modify the matrix
        // draw the rotated gizmo
        setGizmoMatrix(Gizmos.matrix * Matrix4x4.TRS(default, Quaternion.Euler(0, 0, 90), Vector3.one));
        drawFlatWedge();
        setGizmoMatrix(prevMtrx);

        //rings
        void drawRings(float transformRadius)
        {
            // This is the angle formed by dividing the field of view (in radians) in half.
            float alpha = _fovRad / 2;
            // Calculates the distance from the center of the cone to the plane that cuts the cone
            // at a radius "radius". The cosine of the angle alpha and the transform radius are used.
            float distance = transformRadius * Mathf.Cos(alpha);
            // Calculates the radius of the circle that cuts the cone in the aforementioned plane.
            // The sine of the angle alpha and the transform radius are used.
            float radius = transformRadius * Mathf.Sin(alpha);
            Vector3 center = new Vector3(0, 0, distance);

            // Draws a wire circle in the scene space. The circle is drawn around
            // the "center" vector and has a radius of "radius".
            Handles.DrawWireDisc(center, Vector3.forward, radius);
        }

        drawRings(radiusOuter);
        drawRings(radiusInner);

    }

    bool isCircularContained(Vector3 position)
    {
        float distance = Vector3.Distance(transform.position, position);
        return distance >= radiusInner && distance <= radiusOuter;
    }

    void drawCircularGizmo()
    {
        Gizmos.DrawWireSphere(default, radiusInner);
        Gizmos.DrawWireSphere(default, radiusOuter);
    }

    bool isWedgeContained(Vector3 position)
    {
        Vector3 vecToTargetWorld = (position - transform.position);

        // inverse transform is world to local
        Vector3 vecToTarget = transform.InverseTransformVector(vecToTargetWorld);
        Vector3 dirToTarget = vecToTarget.normalized;

        if (vecToTarget.y < 0 || vecToTarget.y > height) return false; // outside the height range

        Vector3 flatDirToTarget = vecToTarget;
        flatDirToTarget.y = 0;
        float flatDitance = flatDirToTarget.magnitude;
        flatDirToTarget /= flatDitance;

        if (flatDirToTarget.z < _angThresh) return false; // outside the angular wedge

        if (flatDitance > radius) return false; // outside the angular wedge

        return true;
    }

    void drawWedgeGizmo()
    {
        Gizmos.color = Handles.color = isWedgeContained(target.position) ? Color.white : Color.red;

        Vector3 top = new Vector3(0, height, 0);

        float cosineThreshold = _angThresh;
        float sineValue = Mathf.Sqrt(1 - cosineThreshold * cosineThreshold);

        Vector3 vLeft = new Vector3(-sineValue, 0, cosineThreshold) * radius;
        Vector3 vRight = new Vector3(sineValue, 0, cosineThreshold) * radius;

        // Draw the circular arcs that make up the sides of the wedge
        Handles.DrawWireArc(default, Vector3.up, vLeft, fovDeg, radius);
        Handles.DrawWireArc(top, Vector3.up, vLeft, fovDeg, radius);

        // Draw the rays extending from the top and bottom of the wedge to the edges of the circular arcs
        Gizmos.DrawRay(default, vLeft);
        Gizmos.DrawRay(default, vRight);
        Gizmos.DrawRay(top, vLeft);
        Gizmos.DrawRay(top, vRight);

        // Draw the lines connecting the top and bottom of the wedge to the edges of the circular arcs
        Gizmos.DrawLine(default, top);
        Gizmos.DrawLine(vLeft, top + vLeft);
        Gizmos.DrawLine(vRight, top + vRight);
    }
}
