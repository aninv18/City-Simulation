using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{

    public float stopDistance = 5f; // Minimum distance to keep from the car in front
    public LayerMask carLayerMask; 
    
    public List<GameObject> waypointsList;
    int current = 0;
    public bool createpath = true;
    public GameObject pathholder;

    public float speed = 10.0f;
    public float rot = 10.0f;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.CompareTag("character"))
        {
            if (speed == 0) gameObject.GetComponent<Animator>().SetBool("isWalking", false);
            else gameObject.GetComponent<Animator>().SetBool("isWalking", true);
        }      




        RaycastHit hit;
        // Cast a ray forward from the car
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, stopDistance, carLayerMask))
        {
            // Hit something within stopDistance, check if it's a car
            if (hit.collider.CompareTag(this.gameObject.tag))
            {
                // Stop or slow down the car
                //Debug.Log("Car in front detected, stopping or slowing down.");
                // For example, disable the movement script or set velocity to zero


                if (Vector3.Distance(this.transform.position, hit.collider.gameObject.transform.position) < stopDistance)
                {
                    
                    speed = 0;
                }

                else speed = 1;


               
            }

            else speed = 1;

        }

        // Optionally, visualize the raycast in the editor
        Debug.DrawRay(rayOrigin, rayDirection * stopDistance, Color.red);


        if (createpath)
        {           

            foreach (Transform child in pathholder.gameObject.transform)
            {

                waypointsList.Add(child.gameObject);
                //Debug.Log(waypointsList.Count);

            }

            current = waypointsList.Count - 1;
            createpath = false;

        }


        if (!createpath)
        {

            if (Vector3.Distance(this.transform.position, waypointsList[current].transform.position) < 0.2f && current > 0) current--;

            
            //Debug.Log(this.transform.rotation);
            if (current < 3) stopDistance = 0;

            
            Vector3 destination = waypointsList[current].transform.position;
            

            this.transform.LookAt(destination);
            Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            
            transform.position = newPos;
            

            float distance = Vector3.Distance(transform.position, destination);

            if (distance <= 0.05f)
            {
                if (current > 0)
                {
                    current--;
                }
                else
                {
                    current = 0;
                    Debug.Log("last");
                    this.gameObject.SetActive(false);
                }

            }
        }

    }
}
