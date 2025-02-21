using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CameraHandler : MonoBehaviour
{
    public Transform targetTransform; 
    public Transform cameraTransform;
    public Transform cameraPivotTransform;//Center
    private Transform myTransform;
    private Vector3 cameraTransformPosition;
    private LayerMask ignoreLayers;
    public static CameraHandler singleton;

    public float lookspeed = 0.1f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.03f;

    private float defaultPosition;
    private float lookAngel;
    private float pivotAngel;
    public float minimumPivot =-35;
    public float maximumPivot = 35;

    void Awake()
    {
        singleton=this;
        myTransform=transform;//Configure myTransform variable to Game object Transform
        defaultPosition = cameraTransform.localPosition.z;
        ignoreLayers = ~(1 << 8 | 1 <<9 | 1<<10);
    }
    
    public void FollowTarget(float delta)
    {
        Vector3 targetPosition = Vector3.Lerp(myTransform.position,targetTransform.position,delta / followSpeed);
        myTransform.position = targetPosition;
    }

    public void HandleCameraRotation(float delta ,float mouseXInput,float mouseYInput)
    {
        lookAngel += (mouseXInput * lookspeed) / delta;
        pivotAngel -= (mouseYInput * pivotSpeed) /delta;
        pivotAngel = Mathf.Clamp(pivotAngel,minimumPivot,maximumPivot);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngel;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        myTransform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngel;

        targetRotation = Quaternion.Euler(rotation);
        cameraPivotTransform.localRotation = targetRotation;
        
    }
}
