using Boo.Lang;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;
 
 /*
  * This class is attached to a door handle. The door handle is child of a door.
  */
 public class OpenDoor : MonoBehaviour
 {
  public SteamVR_Input_Sources Hand;
  public SteamVR_Action_Boolean ToggleGripButton;
  private Rigidbody _rigidbody;
 
  
  public List<GameObject> NearObjects =new List<GameObject>();
  
  
  private void Update()
  {
   
   if (ToggleGripButton.GetStateUp(Hand))// Check if we want to grab the object
   {
     
    
     print("Grab action");
   }
   
   if (ToggleGripButton.GetStateDown(Hand))// Check if we want to drop the object
   {
     print("Drop action");
   }
  
   
   
  }

  private void OnTriggerEnter (Collider other)
  {
   //Add grabbable objects in range of our hand to a list
   if (other.CompareTag("Grabbable"))
   {
    NearObjects.Add(other.gameObject);
   }
   Debug.Log(NearObjects);
  }
  
  private GameObject ClosestGrabbable()
  {
   //find the object in our list of grabbable that is closest and return it.
   GameObject closestGameObj = null;
   var distance = float.MaxValue;
   if (NearObjects == null) return null;
   foreach (var gameObj in NearObjects)
   {
    if (!((gameObj.transform.position - transform.position).sqrMagnitude < distance)) continue;
    closestGameObj = gameObj;
    distance = (gameObj.transform.position - transform.position).sqrMagnitude;
   }
   return closestGameObj;
  }
 
 }
