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


// TODO: Comentar

// using UnityEngine;
// using UnityEditor;

// public class WedgeTrigger : MonoBehaviour
// {
//     // A public Transform variable representing the target to monitor
//     public Transform target;

//     // A public float variable representing the radius of the cone wedge
//     public float radius = 1;

//     // A public float variable representing the height of the cone wedge
//     public float height = 1;

//     // A public float variable representing the field of view angle (FOV) in degrees
//     [Range(0, 180)]
//     public float fovDeg = 45;

//     // A property that converts the FOV angle to radians
//     float FovRad => fovDeg * Mathf.Deg2Rad;

//     // A property that calculates the angle threshold from the FOV angle
//     float AngThresh => Mathf.Cos(FovRad / 2);

//     // This method is called when a gizmo is drawn in the Unity editor
//     private void OnDrawGizmos()
//     {
//         // Set the Gizmos and Handles drawing matrix to the transform matrix of the current object
//         Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;

//         // Set the color of Gizmos and Handles to white or red depending on whether the target is within the cone wedge or not
//         Gizmos.color = Handles.color = isContains(target.position) ? Color.white : Color.red;

//         // Calculate the position of the top of the cone wedge
//         Vector3 top = new Vector3(0, height, 0);

//         // Calculate the value p for the calculation of vectors vLeft and vRight later
//         float p = AngThresh;

//         // Calculate the value x for the calculation of vectors vLeft and vRight later
//         float x = Mathf.Sqrt(1 - p * p);

//         // Calculate the vLeft vector pointing left from the base of the cone wedge
//         Vector3 vLeft = new Vector3(-x, 0, p) * radius;

//         // Calculate the vRight vector pointing right from the base of the cone wedge
//         Vector3 vRight = new Vector3(x, 0, p) * radius;

//         // Draw a wireframe arc at the base of the cone wedge using the vLeft vector as the starting vector and the radius and FOV angle as arguments
//         Handles.DrawWireArc(default, Vector3.up, vLeft, fovDeg, radius);

//         // Draw a wireframe arc at the top of the cone wedge using the vLeft vector as the starting vector and the radius and FOV angle as arguments
//         Handles.DrawWireArc(top, Vector3.up, vLeft, fovDeg, radius);

//         // Draw a Gizmos ray from the base of the cone wedge to the vLeft vector
//         Gizmos.DrawRay(default, vLeft);

//         // Draw a Gizmos ray from the base of the cone wedge to the vRight vector
//         Gizmos.DrawRay(default, vRight);

//         // Draw a Gizmos ray from the top of the cone wedge to the vLeft vector
//         Gizmos.DrawRay(top, vLeft);

//         // Draw a Gizmos ray from the top of the cone wedge to the vRight vector
//         Gizmos.DrawRay(top, vRight);

//         // Draw a Gizmos line from the base of the cone wedge to the top
//         Gizmos.DrawLine(default, top);

//         // Draw a Gizmos line from the vLeft vector at the base of the cone wedge to the vLeft vector at the top
//         Gizmos.DrawLine(vLeft, top + vLeft);

//         // Draw a Gizmos line from the vRight vector at the base of the cone wedge to the vRight vector at the top
//         Gizmos.DrawLine(vRight, top + vRight);
//     }

//     // A method that takes a Vector3 position and returns a boolean indicating whether the position is within the cone wedge or not
//     public bool isContains(Vector3 position)
//     {
//         // Calculate the vector from the current object's position to the target position in world space
//         Vector3 vecToTargetWorld = (position - transform.position);

//         // Transform the vector from world space to the local space of the current object
//         Vector3 vecToTarget = transform.InverseTransformVector(vecToTargetWorld);

//         // Normalize the vector to get the direction to the target
//         Vector3 dirToTarget = vecToTarget.normalized;

//         // If the y-component of the vector is less than 0 or greater than the height of the cone wedge, return false (outside the height range)
//         if (vecToTarget.y < 0 || vecToTarget.y > height) return false;

//         // Calculate a flat version of the vector to the target by setting its y-component to 0
//         Vector3 flatDirToTarget = vecToTarget;
//         flatDirToTarget.y = 0;

//         // Calculate the flat distance to the target by getting the magnitude of the flat vector to the target
//         float flatDitance = flatDirToTarget.magnitude;

//         // Normalize the flat vector to the target
//         flatDirToTarget /= flatDitance;

//         // If the z-component of the normalized flat vector is less than the angle threshold, return false (outside the angular wedge)
//         if (flatDirToTarget.z < AngThresh) return false;

//         // If the flat distance to the target is greater than the radius of the cone wedge, return false (outside the angular wedge)
//         if (flatDitance > radius) return false;

//         // If none of the above conditions are met, return true (inside the cone wedge)
//         return true;
//     }

