using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get input data from keyboard or controller
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        // update player position based on input
        Vector3 position = transform.position;
        position.x += moveHorizontal * 1* Time.deltaTime;
        position.z += moveVertical * 1 * Time.deltaTime;
        transform.position = position;
    }
}
