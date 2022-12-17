using System;
using UnityEngine;
using UnityEditor;

public class ShapeAreaTrigger : MonoBehaviour
{
    [SerializeField]
    Shape shape;
    [SerializeField]
    Transform target;

    [SerializeField]
    enum Shape
    {
        Wedge,
        Radial,
        Cone,
        Circular
    }
    [SerializeField]
    float radiusInner;
    // [SerializeField] TODO: se usa cuando se pase Wedge Trigger
    // float radius = 1;
    [SerializeField]
    float height = 1;
    [SerializeField]
    float radiusOuter;
    [SerializeField, Range(0, 180)]
    float fovDeg = 45;

    float FovRad => fovDeg * Mathf.Deg2Rad;

    float angThresh => Mathf.Cos(FovRad / 2);

    void setGizmoMatrix(Matrix4x4 m) => Gizmos.matrix = Handles.matrix = m;
    public bool IsContained(Vector3 position) =>
        shape switch
        {
            Shape.Circular => isCircularContained(position),
            Shape.Cone => isConeContained(position),
            _ => throw new NotImplementedException(),
        };

    void OnDrawGizmos()
    {
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
        }

    }

    bool isConeContained(Vector3 position)
    {
        if (isCircularContained(position) == false) return false;

        Vector3 dirToTarget = (position - transform.position).normalized;

        float angleRad = angleBetweenNormilizedVectors(transform.forward, dirToTarget);

        return angleRad < FovRad / 2;
    }

    static float angleBetweenNormilizedVectors(Vector3 a, Vector3 b)
    {
        return Mathf.Clamp(Mathf.Acos(Vector3.Dot(a, b)), -1, 1);
    }


    void drawConeGizmo()
    {
        Vector3 top = new Vector3(0, height, 0);

        float p = angThresh;
        float x = Mathf.Sqrt(1 - p * p);

        Vector3 vLeftDir = new Vector3(-x, 0, p);
        Vector3 vRightDir = new Vector3(x, 0, p);
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
            // Es el ángulo que se forma al dividir a la mitad el cono.
            float alpha = FovRad / 2;
            float distance = transformRadius * Mathf.Cos(alpha);
            float radius = transformRadius * Mathf.Sin(alpha);
            Vector3 center = new Vector3(0, 0, distance);

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

}
