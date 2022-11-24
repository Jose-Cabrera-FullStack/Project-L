using UnityEngine;
using UnityEditor;

public class WedgeTrigger : MonoBehaviour
{
    public Transform target;

    public float radius = 1;
    public float height = 1;
    [Range(0, 180)]
    public float fovDeg = 45;

    float FovRad => fovDeg * Mathf.Deg2Rad;
    float AngThresh => Mathf.Cos(FovRad / 2);

    /// <summary>
    /// This code is Cheaper than code commented below.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;
        Gizmos.color = Handles.color = isContains(target.position) ? Color.white : Color.red;

        Vector3 top = new Vector3(0, height, 0);

        float p = AngThresh;
        float x = Mathf.Sqrt(1 - p * p);

        Vector3 vLeft = new Vector3(-x, 0, p) * radius;
        Vector3 vRight = new Vector3(x, 0, p) * radius;

        Handles.DrawWireArc(default, Vector3.up, vLeft, fovDeg, radius);
        Handles.DrawWireArc(top, Vector3.up, vLeft, fovDeg, radius);

        Gizmos.DrawRay(default, vLeft);
        Gizmos.DrawRay(default, vRight);
        Gizmos.DrawRay(top, vLeft);
        Gizmos.DrawRay(top, vRight);

        Gizmos.DrawLine(default, top);
        Gizmos.DrawLine(vLeft, top + vLeft);
        Gizmos.DrawLine(vRight, top + vRight);
    }

    public bool isContains(Vector3 position)
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

        if (flatDirToTarget.z < AngThresh) return false; // outside the angular wedge

        if (flatDitance > radius) return false; // outside the angular wedge 

        return true;
    }
}
