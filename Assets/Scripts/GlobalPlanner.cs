using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPlanner : MonoBehaviour
{
    public GameObject[] paths;
    public GameObject[] car;
    public GameObject[] pedestrian;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("FixPaths", 0.5f);        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixPaths()
    {

        for (int i = 0; i < paths.Length; i++)
        {
            if (paths[i].gameObject.name == "Path1" && paths[i].gameObject.transform.childCount > 0)
            {
                paths[i].gameObject.transform.position += new Vector3(-0.13f, 0, -0.13f);
                //Debug.Log(paths[i].gameObject.name); 
            }

            else if (paths[i].gameObject.name == "Path2" && paths[i].gameObject.transform.childCount > 0)
            {
                paths[i].gameObject.transform.position += new Vector3(-0.13f, 0, 0.13f);
                //Debug.Log(paths[i].gameObject.name); 
            }

            else if (paths[i].gameObject.name == "Path3" && paths[i].gameObject.transform.childCount > 0)
            {
                paths[i].gameObject.transform.position += new Vector3(-0.13f, 0, 0);
                //Debug.Log(paths[i].gameObject.name); 
            }
            else if (paths[i].gameObject.name == "Walk1" && paths[i].gameObject.transform.childCount > 0)
            {
                paths[i].gameObject.transform.position += new Vector3(0, 0, 0.35f);
                //Debug.Log(paths[i].gameObject.name); 
            }
            else if (paths[i].gameObject.name == "Walk2" && paths[i].gameObject.transform.childCount > 0)
            {
                paths[i].gameObject.transform.position += new Vector3(0, 0, -0.35f);
                //Debug.Log(paths[i].gameObject.name); 
            }
            else if (paths[i].gameObject.name == "Walk3" && paths[i].gameObject.transform.childCount > 0)
            {
                paths[i].gameObject.transform.position += new Vector3(-0.35f, 0, 0);
                //Debug.Log(paths[i].gameObject.name); 
            }
            else if (paths[i].gameObject.name == "Walk4" && paths[i].gameObject.transform.childCount > 0)
            {
                paths[i].gameObject.transform.position += new Vector3(0.35f, 0, 0);
                //Debug.Log(paths[i].gameObject.name); 
            }

        }

        StartCoroutine(Simulation());
    }

    IEnumerator Simulation()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < 10; i++)
        {
            int randomNumber = Random.Range(0, 2);
            //Debug.Log(randomNumber);
            int randomizer = Random.Range(0, 4);
            //Debug.Log(randomizer);



            if (randomNumber == 0)
            {
                int randomcar = Random.Range(0, 3);
                if (randomizer == 0)
                {
                    GameObject car_instantiated = Instantiate(car[randomcar]);
                    car_instantiated.transform.position = paths[0].transform.GetChild(paths[0].transform.childCount - 1).transform.position;
                    car_instantiated.GetComponent<mover>().pathholder = paths[0];
                    car_instantiated.GetComponent<mover>().enabled = true;
                }

                else if (randomizer == 1)
                {
                    GameObject car_instantiated = Instantiate(car[randomcar]);
                    car_instantiated.transform.position = paths[1].transform.GetChild(paths[1].transform.childCount - 1).transform.position;
                    car_instantiated.GetComponent<mover>().pathholder = paths[1];
                    car_instantiated.GetComponent<mover>().enabled = true;
                }

                else if (randomizer == 2)
                {
                    GameObject car_instantiated = Instantiate(car[randomcar]);
                    car_instantiated.transform.position = paths[2].transform.GetChild(paths[2].transform.childCount - 1).transform.position;
                    car_instantiated.GetComponent<mover>().pathholder = paths[2];
                    car_instantiated.GetComponent<mover>().enabled = true;
                }


            }

            else
            {
                if (randomizer == 0)
                {
                    GameObject pedestrian_instantiated = Instantiate(pedestrian[0]);
                    pedestrian_instantiated.transform.position = paths[3].transform.GetChild(paths[3].transform.childCount - 1).transform.position;
                    pedestrian_instantiated.GetComponent<mover>().pathholder = paths[3];
                    pedestrian_instantiated.GetComponent<mover>().enabled = true;
                }

                else if (randomizer == 1)
                {
                    GameObject pedestrian_instantiated = Instantiate(pedestrian[1]);
                    pedestrian_instantiated.transform.position = paths[4].transform.GetChild(paths[4].transform.childCount - 1).transform.position;
                    pedestrian_instantiated.GetComponent<mover>().pathholder = paths[4];
                    pedestrian_instantiated.GetComponent<mover>().enabled = true;
                }

                else if (randomizer == 2)
                {
                    GameObject pedestrian_instantiated = Instantiate(pedestrian[1]);
                    pedestrian_instantiated.transform.position = paths[5].transform.GetChild(paths[5].transform.childCount - 1).transform.position;
                    pedestrian_instantiated.GetComponent<mover>().pathholder = paths[5];
                    pedestrian_instantiated.GetComponent<mover>().enabled = true;
                }

                else if (randomizer == 3)
                {
                    GameObject pedestrian_instantiated = Instantiate(pedestrian[0]);
                    pedestrian_instantiated.transform.position = paths[6].transform.GetChild(paths[6].transform.childCount - 1).transform.position;
                    pedestrian_instantiated.GetComponent<mover>().pathholder = paths[6];
                    pedestrian_instantiated.GetComponent<mover>().enabled = true;
                }


            }

            yield return new WaitForSeconds(0.5f);

        }
    }
}
