using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocations
{
    public int x;
    public int z;

    public MapLocations(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    public Vector2 ToVector()
    {
        return new Vector2(x, z);
    }

    public static MapLocations operator +(MapLocations a, MapLocations b)
       => new MapLocations(a.x + b.x, a.z + b.z);

    public override bool Equals(object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            return false;
        else
            return x == ((MapLocations)obj).x && z == ((MapLocations)obj).z;
    }


}

public class GridCreator : MonoBehaviour
{
    public List<MapLocations> directions = new List<MapLocations>() {
                                            new MapLocations(1,0),
                                            new MapLocations(0,1),
                                            new MapLocations(-1,0),
                                            new MapLocations(0,-1) };
    public int width = 30; //x length
    public int depth = 30; //z length
    public byte[,] map;
    public int scale = 6;
    // Start is called before the first frame update
    void Start()
    {
        map = new byte[width, depth];
        Initializer();
        
        DrawMap();

    }

    // Update is called once per frame
    void Initializer()
    {
        RaycastHit hit;
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {

                Vector3 pos = new Vector3(x * scale, 0, z * scale);                

                Vector3 rayOrigin = pos;
                Vector3 rayDirection = transform.up;

                if (Physics.Raycast(rayOrigin, rayDirection, out hit))
                {
                    // Hit something within stopDistance, check if it's a car
                    if (hit.collider.CompareTag("roads"))
                    {

                        //Debug.Log(x + " " + z);
                        map[x, z] = 0;  // 0 implies open and 1 implies closed

                    }
                }

                else
                {
                    map[x, z] = 1;           
                }
            }
        }
    }

    void DrawMap()
    {
        for (int z = 0; z < depth; z++)
            for (int x = 0; x < width; x++)
            {
                if (map[x, z] == 1)
                {
                    Vector3 pos = new Vector3(x * scale, -2, z * scale);
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.GetComponent<BoxCollider>().enabled = false;
                    wall.GetComponent<Renderer>().enabled = false;
                    wall.transform.localScale = new Vector3(scale, scale, scale);
                    wall.transform.position = pos;

                    wall.transform.SetParent(this.transform);
                }
            }
    }

}
