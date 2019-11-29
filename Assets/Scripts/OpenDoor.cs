using Boo.Lang;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;
 
 
 public class OpenDoor : MonoBehaviour
 {
  public SteamVR_Input_Sources Hand;
  public SteamVR_Action_Boolean ToggleGripButton;
  private Rigidbody _rigidbody;
  public GameObject Door;
  
  //not sure if controller  will be updated by unity, needs testing
  public GameObject Controller;
  
  public List<GameObject> NearObjects = new List<GameObject>();
  
  
  private void Update()
  {
   
   if (ToggleGripButton.GetStateUp(Hand))// Check if we want to grab the object
   {
    
    
    //test if this will work, better way than the vector3 distance but depends if unity fills in the list of gameobjects or not. 
    //var local = ClosestGrabbable();
    //local.transform.localRotation = Controller.transform.localRotation;
    
    
    //this should work if Controller is updated, if it does no need to hide the door with setactive
    var dist = Vector3.Distance(Door.transform.position, Controller.transform.position);
    if (dist <= 5)
    {
     //remove setactive if localrotation works from controller
     Door.SetActive(false);
     Door.transform.localRotation = Controller.transform.localRotation;
    }
     print("Grab action");
   }
   
   if (ToggleGripButton.GetStateDown(Hand))// Check if we want to drop the object
   {
    Door.SetActive(true);
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
