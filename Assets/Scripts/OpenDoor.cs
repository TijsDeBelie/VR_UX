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
    //public Rigidbody Door;
    private GameObject[] NearObjects;

    //not sure if controller  will be updated by unity, needs testing
    public GameObject Controller;

    private void Start()
    {
        NearObjects = GameObject.FindGameObjectsWithTag("Door");

        ToggleGripButton.AddOnStateDownListener((newState, source) => {
            print(NearObjects.Length);
            GameObject local = ClosestGrabbable();
            print(local);
            if (local == null) return;
            var dist = Vector3.Distance(local.transform.position, Controller.transform.position);
            print(dist);
            if (dist < 0.9)
            {
                
                local.gameObject.GetComponent<MeshRenderer>().enabled = !local.gameObject.GetComponent<MeshRenderer>().enabled;
            }
        }, Hand);

    }


    private void Update()
    {

        //ToggleGripButton.AddOnStateDownListener((newState, source) => {
        //    var dist = Vector3.Distance(Door.transform.position, Controller.transform.position);
        //    Door.gameObject.GetComponent<MeshRenderer>().enabled = !(dist < 1 && ToggleGripButton.GetLastStateDown(Hand));
        //}, Hand);

        //var dist = Vector3.Distance(Door.transform.position, Controller.transform.position);
        //print(dist);
        //Door.gameObject.GetComponent<MeshRenderer>().enabled = !(dist < 1 && ToggleGripButton.GetLastStateDown(Hand));

        //if (ToggleGripButton.GetStateUp(Hand))// Check if we want to grab the object
        //{


            //test if this will work, better way than the vector3 distance but depends if unity fills in the list of gameobjects or not. 
            //var local = ClosestGrabbable();
            //local.transform.localRotation = Controller.transform.localRotation;
            //print(Controller.transform.localRotation);

            //this should work if Controller is updated, if it does no need to hide the door with setactive
            
            //print(dist);
        //    if (dist <= 5)
        //    {
        //        //print("Grab action with distance less than 5");
        //        //remove setactive if localrotation works from controller
        //        //Door.SetActive(false);
        //        print(Controller.transform.localRotation.ToString());
        //        //Door.transform.localRotation = Controller.transform.localRotation;

        //        //float degrees = 90;
        //        //Vector3 to = new Vector3(1000, 1000, 1000);

        //        //Door.transform.Rotate(45.0f, 45.0f, 45.0f, Space.World);
        //        //Door.MovePosition(to);

        //        //Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 100, 0) * Time.deltaTime);
        //        //Door.MoveRotation(Door.rotation * deltaRotation);

        //        //Door.gameObject.SetActive(false);
        //        Door.gameObject.GetComponent<MeshRenderer>().enabled = false;
        //    }

        //}

        //if (ToggleGripButton.GetStateDown(Hand))// Check if we want to drop the object
        //{
        //    Door.gameObject.GetComponent<MeshRenderer>().enabled = true;
        //}



    }


    private GameObject ClosestGrabbable()
    {
        //find the object in our list of grabbable that is closest and return it.
        GameObject closestGameObj = null;
        var distance = float.MaxValue;
        foreach (var gameObj in NearObjects)
        {
            print((gameObj.transform.position - Controller.transform.position).sqrMagnitude);
            if (!((gameObj.transform.position - Controller.transform.position).sqrMagnitude < distance)) continue;
            closestGameObj = gameObj;
            distance = (gameObj.transform.position - transform.position).sqrMagnitude;
        }
        return closestGameObj;
    }


}
