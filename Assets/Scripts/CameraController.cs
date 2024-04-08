using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    [FormerlySerializedAs("smoothSpeed")] public float SmoothSpeed = 0.125f; 
    public Vector3 offset; 

    void LateUpdate()
    {
        if (Target != null)
        {
            Vector3 desiredPosition = Target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
            transform.position = smoothedPosition;

            Vector3 direction = Target.position - transform.position;
            direction.Normalize();
            
        }
    }
}