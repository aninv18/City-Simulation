using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathMarker
{

    public MapLocations location;
    public float G, H, F;
    public GameObject marker;
    public PathMarker parent;

    public PathMarker(MapLocations l, float g, float h, float f, GameObject m, PathMarker p)
    {

        location = l;
        G = g;
        H = h;
        F = f;
        marker = m;
        parent = p;
    }

    public override bool Equals(object obj)
    {

        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            return false;
        else
            return location.Equals(((PathMarker)obj).location);
    }

   
}

public class FindPathAStar : MonoBehaviour
{

    public GridCreator maze;    
    public Material closedMaterial;
    public Material openMaterial;
    public GameObject start;
    public GameObject end;
    public GameObject pathP;
    public GameObject PathParent;

    public Vector3Int startloc;
    public Vector3Int endloc;

    PathMarker startNode;
    PathMarker goalNode;
    PathMarker lastPos;
    bool done = false;
    public bool hasStarted = false;
    public bool searchstart = true;

    List<PathMarker> open = new List<PathMarker>();
    List<PathMarker> closed = new List<PathMarker>();

    void RemoveAllMarkers()
    {

        GameObject[] markers = GameObject.FindGameObjectsWithTag("marker");

        foreach (GameObject m in markers) Destroy(m);
    }

    public void BeginSearch()
    {

        done = false;
        

        List<MapLocations> locations = new List<MapLocations>();

        //start with the grid and by raycasting determine what block is either closed or open and assign 0 or 1 needed for path-finding
        for (int z = 1; z < maze.depth - 1; ++z)
        {
            for (int x = 1; x < maze.width - 1; ++x)
            {

                if (maze.map[x, z] != 1)
                {
                    locations.Add(new MapLocations(x, z));
                }
            }
        }
     

       
        Vector3 startLocation = maze.scale * startloc;

        startNode = new PathMarker(new MapLocations(startloc.x, startloc.z),
            0.0f, 0.0f, 0.0f, Instantiate(start, startLocation, Quaternion.identity), null);

        Vector3 endLocation = maze.scale * endloc;

        goalNode = new PathMarker(new MapLocations(endloc.x, endloc.z),
            0.0f, 0.0f, 0.0f, Instantiate(end, endLocation, Quaternion.identity), null);

        open.Clear();
        closed.Clear();

        open.Add(startNode);
        lastPos = startNode;
    }

    void Search(PathMarker thisNode)
    {

        if (thisNode == null) return;
        if (thisNode.Equals(goalNode))
        {

            done = true;
            // Debug.Log("completed!");
            return;
        }

        foreach (MapLocations dir in maze.directions)
        {

            MapLocations neighbour = dir + thisNode.location;

            if (maze.map[neighbour.x, neighbour.z] == 1) continue;
            if (neighbour.x < 1 || neighbour.x >= maze.width || neighbour.z < 1 || neighbour.z >= maze.depth) continue;
            if (IsClosed(neighbour)) continue;

            float g = Vector2.Distance(thisNode.location.ToVector(), neighbour.ToVector()) + thisNode.G;
            float h = Vector2.Distance(neighbour.ToVector(), goalNode.location.ToVector());
            float f = g + h;

            GameObject pathBlock = Instantiate(pathP, new Vector3(neighbour.x * maze.scale, 0.0f, neighbour.z * maze.scale), Quaternion.identity);
            //pathBlock.GetComponent<Renderer>().enabled = false;

            TextMesh[] values = pathBlock.GetComponentsInChildren<TextMesh>();

            //update the f, g and h values. for demonstration purposes

            values[0].text = "G: " + g.ToString("0.0");
            values[1].text = "H: " + h.ToString("0.0");
            values[2].text = "F: " + f.ToString("0.0");

            if (!UpdateMarker(neighbour, g, h, f, thisNode))
            {

                open.Add(new PathMarker(neighbour, g, h, f, pathBlock, thisNode));
            }
        }
        open = open.OrderBy(p => p.F).ToList<PathMarker>();
        PathMarker pm = (PathMarker)open.ElementAt(0);

        closed.Add(pm);

        open.RemoveAt(0);
        pm.marker.GetComponent<Renderer>().material = closedMaterial;

        lastPos = pm;
    }

    bool UpdateMarker(MapLocations pos, float g, float h, float f, PathMarker prt)
    {

        foreach (PathMarker p in open)
        {

            if (p.location.Equals(pos))
            {

                p.G = g;
                p.H = h;
                p.F = f;
                p.parent = prt;
                return true;
            }
        }
        return false;
    }

    bool IsClosed(MapLocations marker)
    {

        foreach (PathMarker p in closed)
        {

            if (p.location.Equals(marker)) return true;
        }
        return false;
    }

    void GetPath()
    {
        //removes the unnecessary markers so the the final path can be shown
        RemoveAllMarkers();


        PathMarker begin = lastPos;

        while (!startNode.Equals(begin) && begin != null)
        {

            GameObject f1 = Instantiate(pathP, new Vector3(begin.location.x * maze.scale, 0.0f, begin.location.z * maze.scale), Quaternion.identity);
            f1.gameObject.tag = "path";
            f1.transform.SetParent(PathParent.transform);
            //f1.GetComponent<Renderer>().enabled = false;




            begin = begin.parent;
        }

        GameObject f = Instantiate(pathP, new Vector3(startNode.location.x * maze.scale, 0.0f, startNode.location.z * maze.scale), Quaternion.identity);
        f.gameObject.tag = "path";
        f.transform.SetParent(PathParent.transform);
        //f.GetComponent<Renderer>().enabled = false;

    }

    void Update()
    {


        if (searchstart)
        {

            BeginSearch();
            searchstart = false;


        }

        if (!done)
        {
            Search(lastPos);
            hasStarted = true;

        }

        if (done && hasStarted)
        {
            GetPath();
            hasStarted = false;


        }


        /* The code below is used for demonstartion purposes with keybindings. Otherwise the code above automates the process of A* path-finding*/

        //if (Input.GetKeyDown(KeyCode.P))
        //{

        //    BeginSearch();

        //    searchstart = false;
        //}



        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    Search(lastPos);
        //    hasStarted = true;
        //}


        //if (Input.GetKeyDown(KeyCode.M) && done)
        //{

        //    GetPath();
        //    hasStarted = false;
        //}
    }
}