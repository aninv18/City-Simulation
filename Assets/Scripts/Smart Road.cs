using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartRoad : MonoBehaviour
{
    public GameObject[] paths;
    public GameObject[] cars_enable;
    Queue<GameObject> cars = new Queue<GameObject>();
    GameObject currentcar;
    
    // Start is called before the first frame update
    void Start()
    {
       paths = GameObject.FindGameObjectsWithTag("Player");
       currentcar = null;
    }

    // Update is called once per frame
    void Update()
    {

       
        if (currentcar == null && cars.Count > 0)
        {
            currentcar = cars.Dequeue(); // Get and remove the first car in the queue
            

            if(currentcar.GetComponent<CapsuleCollider>() != null) { currentcar.GetComponent<mover>().speed = 0.2f; }

            else { currentcar.GetComponent<mover>().speed = 1; }

        }


    }


    private void OnTriggerEnter(Collider other)
    {
        

            if (other.CompareTag("Player") || other.CompareTag("character"))
            {
                // Disable the car's movement as it enters the queue
                other.GetComponent <mover>().speed = 0;
                other.GetComponent <mover>().stopDistance = 0.0f;

            
                cars.Enqueue(other.gameObject); // Add the car to the queue

                
            }

            
    }

    private void OnTriggerExit(Collider other)
    {
       

        if (other.CompareTag("Player") || other.CompareTag("character") && other.gameObject == currentcar)
        {
            currentcar.GetComponent<mover>().stopDistance = 0.0f;           
            // Once the current car exits the trigger, allow the next car to move

            currentcar = null; // Reset currentCar allowing the next car in Update() to start moving

            
        }
    }

    
}
