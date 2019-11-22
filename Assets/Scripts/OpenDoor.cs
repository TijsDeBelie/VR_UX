using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
 
 /*
  * This class is attached to a door handle. The door handle is child of a door.
  */
 public class OpenDoor : MonoBehaviour
 {
     private Vector3 _force;

     private void Start()
     {
         _rigidbody = GetComponentInParent<Rigidbody>();
     }

     private Vector3 _cross;
     private bool _holdingHandle;
     private float _angle;
     private Rigidbody _rigidbody;
     private const float ForceMultiplier = 150f;
    
     private void HandHoverUpdate(Hand hand)
     {
         var startingGrabType = hand.GetGrabStarting();
         
         if (hand.IsGrabbingWithType(startingGrabType))
         {
             _holdingHandle = true;
 
             // Direction vector from the door's pivot point to the hand's current position
             var transform1 = hand.transform;
             var position = transform1.position;
             var transform2 = transform;
             Vector3 doorPivotToHand = position - transform2.parent.position;
 
             // Ignore the y axis of the direction vector
             doorPivotToHand.y = 0;  
 
             // Direction vector from door handle to hand's current position
             _force = position - transform2.position;
 
             // Cross product between force and direction. 
             _cross = Vector3.Cross(doorPivotToHand, _force);
             _angle = Vector3.Angle(doorPivotToHand, _force);
         }
         else if (hand.IsGrabEnding(_rigidbody.gameObject))
         {
             _holdingHandle = false;
         }
     }

     void Update()
     {
         if (_holdingHandle)
         {
             // Apply cross product and calculated angle to
             _rigidbody.angularVelocity = _cross * _angle * ForceMultiplier;
         }
     }
 
     private void OnHandHoverEnd()
     {
         // Set angular velocity to zero if the hand stops hovering
         GetComponentInParent<Rigidbody>().angularVelocity = Vector3.zero;
     }
 }
