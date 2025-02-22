using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CameraHandler : MonoBehaviour
{
    public Transform targetTransform; 
    public Transform cameraTransform;
    public Transform cameraPivotTransform;//Camera position
    private Transform myTransform;
    private Vector3 cameraTransformPosition;
    private LayerMask ignoreLayers;
    private Vector3 cameraFollowVelocity = Vector3.zero;

   

    public static CameraHandler singleton;

    public float lookspeed = 0.1f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.03f;

    private float targetPosition;
    private float defaultPosition;
    private float lookAngel;
    private float pivotAngel;
    public float minimumPivot =-35;
    public float maximumPivot = 35;
    
    public float cameraSphereRadius = 0.2f;
    public float cameraCollisionOffset = 0.2f;
    public float minimumCollisionoffset =0.2f;
    

    void Awake()
    {
        singleton=this;
        myTransform=transform;//Configure myTransform variable to Game object Transform
        defaultPosition = cameraTransform.localPosition.z;
        ignoreLayers = ~(1 << 8 | 1 <<9 | 1<<10);
    }
    
    public void FollowTarget(float delta)//Make the camer follow player
    {
        Vector3 targetPosition = Vector3.SmoothDamp(myTransform.position,targetTransform.position, ref cameraFollowVelocity,delta /followSpeed);
        myTransform.position = targetPosition;

        HandleCameraCollisions(delta);
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
    
    private void HandleCameraCollisions(float delta)//Method for avoiding camera hitting objects
    {
        targetPosition = defaultPosition;
        RaycastHit hit;//Sphere object
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();

        if(Physics.SphereCast(cameraPivotTransform.position ,cameraSphereRadius,direction,out hit,Mathf.Abs(targetPosition),ignoreLayers))//if it's hit game environment return true
        {
            float dis = Vector3.Distance(cameraPivotTransform.position,hit.point);//distance between camera position and hit object position
            targetPosition = -(dis - cameraCollisionOffset);
        }   
        if(Mathf.Abs(targetPosition)< minimumCollisionoffset)
        {
            targetPosition = -minimumCollisionoffset;
        }
        cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition,delta/0.2f);//Camera move to target position smoothly
        cameraTransform.localPosition = cameraTransformPosition;
    }
}
