using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class WalkingCamera : MonoBehaviour
{
    private GameObject floor;

    //Arrows input
    public float verticalInput;
    public float horizontalInput;
    public float scrollInput;
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 150.0f;
    public float scrollSpeed = 400.0f;


    // Start is called before the first frame update
    void Start()
    {
        if (floor == null)
        {
            floor = GameObject.FindWithTag("Floor");
        }
        if (floor != null)
        {
            transform.position = new Vector3(floor.transform.position.x, 1.5f, floor.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical"); 
        horizontalInput = Input.GetAxis("Horizontal"); 
        scrollInput = Input.GetAxis("Mouse ScrollWheel");

        //Position
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * verticalInput);
        
        //Rotation
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * horizontalInput);
        transform.Rotate(Vector3.right, Time.deltaTime * scrollSpeed * scrollInput);
        transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0); 
        
    }
}

