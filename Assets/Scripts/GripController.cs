using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

//These make sure that we have the components that we need
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(FixedJoint))]
public class GripController : MonoBehaviour
{
    public SteamVR_Input_Sources Hand;//these allow us to set our input source and action.

    private void Awake()
    {
        _interactable1 = ConnectedObject.GetComponent<Interactable>();
        _fixedJoint3 = GetComponent<FixedJoint>();
        _configurableJoint2 = GetComponent<ConfigurableJoint>();
        _isConnectedObjectNotNull = ConnectedObject != null;
        _interactable = ConnectedObject.GetComponent<Interactable>();
        _configurableJoint1 = GetComponent<ConfigurableJoint>();
        _fixedJoint2 = GetComponent<FixedJoint>();
        _fixedJoint1 = GetComponent<FixedJoint>();
        _rigidbody1 = ConnectedObject.GetComponent<Rigidbody>();
        _rigidbody = ConnectedObject.GetComponent<Rigidbody>();
        _configurableJoint = GetComponent<ConfigurableJoint>();
        _fixedJoint = GetComponent<FixedJoint>();
    }

    public SteamVR_Action_Boolean ToggleGripButton;

    private GameObject ConnectedObject;//our current connected object
    public List<GameObject> NearObjects =new List<GameObject>();//all objects that we could pick up
    private FixedJoint _fixedJoint;
    private ConfigurableJoint _configurableJoint;
    private Rigidbody _rigidbody;
    private Rigidbody _rigidbody1;
    private FixedJoint _fixedJoint1;
    private FixedJoint _fixedJoint2;
    private ConfigurableJoint _configurableJoint1;
    private Interactable _interactable;
    private bool _isConnectedObjectNotNull;
    private ConfigurableJoint _configurableJoint2;
    private FixedJoint _fixedJoint3;
    private Interactable _interactable1;

    private void Update()
    {
        if (_isConnectedObjectNotNull)//if we are holding somthing
        {
            if (_interactable.touchCount == 0)//if our object isn't touching anything
            {
                //first, we disconnect our object
                _configurableJoint1.connectedBody = null;
                _fixedJoint2.connectedBody = null;
                
                //now we step our object slightly towards the position of our controller, this is because the fixed joint just freezes the object in whatever position it currently is in relation to the controller so we need to move it to the position we want it to be in. We could just set position to the position of the controller and be done with it but I like the look of it swinging into position.
                ConnectedObject.transform.position = Vector3.MoveTowards(ConnectedObject.transform.position, transform.position, .25f);
                ConnectedObject.transform.rotation = Quaternion.RotateTowards(ConnectedObject.transform.rotation, transform.rotation, 10);

                //reconnect the body to the fixed joint
                _fixedJoint1.connectedBody = _rigidbody1;
            }
            else if(_interactable1.touchCount > 0)//if it is touching something 
            {
                //switch from fixed joint to configurable joint
                _fixedJoint.connectedBody = null;
                _configurableJoint.connectedBody = _rigidbody;
            }
            if (ToggleGripButton.GetStateDown(Hand))// Check if we want to drop the object
            {
                Release();
            }
        }
        else//if we aren't holding something
        {
            if (ToggleGripButton.GetStateDown(Hand))//cheack if we want to pick somthing up
            {
                Grip();
            }
        }
    }
    private void Grip()
    {
        var newObject = ClosestGrabbable();
        if(newObject != null)
        ConnectedObject = ClosestGrabbable();//find the Closest Grabbable and set it to the connected objectif it isn't null
    }
    private void Release()
    {
        //disconnect all joints and set the connected object to null
        _configurableJoint2.connectedBody = null;
        _fixedJoint3.connectedBody = null;
        ConnectedObject = null;
    }
    void OnTriggerEnter (Collider other)
    {
        //Add grabbable objects in range of our hand to a list
        if (other.CompareTag("Grabbable"))
        {
            NearObjects.Add(other.gameObject);
        }
        Debug.Log(NearObjects);
    }

    private void OnTriggerExit(Collider other)
    {
        //remove grabbable objects going out of range from our list
        if (other.CompareTag("Grabbable"))
        {
            NearObjects.Remove(other.gameObject);
        }
    }

    private GameObject ClosestGrabbable()
    {
       //find the object in our list of grabbable that is closest and return it.
        GameObject closestGameObj = null;
        var Distance = float.MaxValue;
        if (NearObjects == null) return closestGameObj;
        foreach (var GameObj in NearObjects)
        {
            if (!((GameObj.transform.position - transform.position).sqrMagnitude < Distance)) continue;
            closestGameObj = GameObj;
            Distance = (GameObj.transform.position - transform.position).sqrMagnitude;
        }
        return closestGameObj;
    }
}