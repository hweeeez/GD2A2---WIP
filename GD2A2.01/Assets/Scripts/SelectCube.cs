using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SelectCube : MonoBehaviour
{
    private GameObject closestRight;
    private GameObject closestLeft;
    private GameObject closestUp;
    private GameObject closestDown;
    public LayerMask playerCube;
    private Undo undo;
    bool canLeft;
    bool canRight;
    bool canUp;
    bool canDown;
    List<Vector3> cubePositions;
    private List<GameObject> cubes;
    private GameObject Cube;
    //public int currentCube;
    bool canMove;
    public GameObject whiteCube;
    Vector3Int cubecell;
    public Tilemap tilemap;
    GameObject[] go;
    List<GameObject> barriers;
    List<Vector3> barrierPos;
    bool onCube;
    bool selecting = true;
    public GameObject currentCube;
    bool firstSpace = true;
    float minDist = Mathf.Infinity;
    // Start is called before the first frame update
    void Start()
    {
        cubePositions = new List<Vector3>();
        cubes = new List<GameObject>();
        barriers = new List<GameObject>();
        barrierPos = new List<Vector3>();
        go = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in go)
        {
            if (go.GetComponent<MultiTag>() != null && go.GetComponent<MultiTag>().HasTag("MoveCube"))
            {
                cubes.Add(go);
                cubePositions.Add(go.transform.position);
            }
            if (go.layer == 7)
            {
                barriers.Add(go);
            }
            if (go.GetComponent<Outline>() != null)
            {
                go.GetComponent<Outline>().enabled = false;
            }
        }
        foreach (GameObject go in barriers)
        {
            barrierPos.Add(go.transform.position);

        }
        /* foreach (GameObject Cube in cubes)
         {
             Cube.GetComponent<Outline>().enabled = false;
         }
 */
    }
    public Vector3 FindNearestPointOnLine(Vector3 origin, Vector3 end, Vector3 point)
    {
        //Get heading
        Vector3 heading = (end - origin);
        float magnitudeMax = heading.magnitude;
        heading.Normalize();

        //Do projection from the point but clamp it
        Vector3 lhs = point - origin;
        float dotP = Vector2.Dot(lhs, heading);
        dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
        return origin + heading * dotP;
    }
    // Update is called once per frame
    void Update()
    {
  

        cubecell = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cubecell);
        Vector3 thispos = this.transform.position;
        var hitColliders = Physics.OverlapSphere(thispos, 0.5f);

        Vector3 moveLeft = cellCenterPos + new Vector3Int(1, 1, 0);
        Vector3 moveRight = cellCenterPos + new Vector3Int(-1, 1, 0);
        Vector3 moveDown = cellCenterPos + new Vector3Int(0, 1, 1);
        Vector3 moveUp = cellCenterPos + new Vector3Int(0, 1, -1);
        for (int i = 0; i < barrierPos.Count; i++)
        {
            if (Vector3.Distance(barrierPos[i], moveUp) < 0.0001) { canUp = false; }
            if (Vector3.Distance(barrierPos[i], moveDown) < 0.0001) { canDown = false; }
            if (Vector3.Distance(barrierPos[i], moveRight) < 0.0001) { canRight = false; }
            if (Vector3.Distance(barrierPos[i], moveLeft) < 0.0001) { canLeft = false; }
        }
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, playerCube)){
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log(hit.collider.gameObject.name);
            closestDown = hit.collider.gameObject;
            if (closestDown == null) { canDown = false; } else { canDown = true; }
        }
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, playerCube))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.green);
            Debug.Log(hit.collider.gameObject.name);
            closestUp = hit.collider.gameObject;
            if (closestUp == null) { canUp = false; } else { canUp = true; }
        }
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.left), out hit, Mathf.Infinity, playerCube))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.yellow);
            Debug.Log(hit.collider.gameObject.name);
            closestRight = hit.collider.gameObject;
            if (closestRight == null) { canRight = false; } else { canRight = true; }
        }
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity, playerCube))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.blue);
            Debug.Log(hit.collider.gameObject.name);
            closestLeft = hit.collider.gameObject;
            if (closestLeft == null) { canLeft = false; } else { canLeft = true; }
        }
      /*  Vector3 upDir = this.transform.position - new Vector3(0, 0, 10f);
        Vector3 downDir = this.transform.position + new Vector3(0, 0, 10f);
        Vector3 leftDir = this.transform.position + new Vector3(10f, 0, 0);
        Vector3 rightDir = this.transform.position - new Vector3(10f, 0, 0);
        float closestDist = Mathf.Infinity;*/
        /*        foreach (GameObject cube in cubes)
                {
                    GameObject closestCube = null;
                    Vector3 dirtoCube = cube.transform.position - this.transform.position;
                    float dSqr = dirtoCube.sqrMagnitude;
                    if (dSqr < closestDist)
                    {
                        closestDist = dSqr;
                        closestCube = cube;
                        print(closestCube.name);
                    }
                }*/
       /* for (int i = 0; i < cubes.Count; i++)
        {
            FindNearestPointOnLine(this.transform.position, upDir, cubes[i].transform.position);
            Debug.Log(FindNearestPointOnLine(this.transform.position, upDir, cubes[i].transform.position));
*/
            /*            if (!closestUp | !closestDown | !closestLeft | !closestRight)
                        {
                            closestUp = cubes[i];
                            closestDown = cubes[i];
                            closestLeft = cubes[i];
                            closestRight = cubes[i];
                        }

                        if ((Vector3.Distance(upDir, cubes[i].transform.position) <= Vector3.Distance(upDir, closestUp.transform.position)))
                        {
                            closestUp = cubes[i];
                        }
                        if ((Vector3.Distance(downDir, cubes[i].transform.position) <= Vector3.Distance(downDir, closestDown.transform.position)))
                        {
                            closestDown = cubes[i];
                        }
                        if ((Vector3.Distance(leftDir, cubes[i].transform.position) <= Vector3.Distance(leftDir, closestLeft.transform.position)))
                        {
                            closestLeft = cubes[i];
                        }
                        if ((Vector3.Distance(rightDir, cubes[i].transform.position) <= Vector3.Distance(rightDir, closestRight.transform.position)))
                        {
                            closestRight = cubes[i];
                        }*/
      //  }
        if (canLeft && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.transform.position = closestLeft.transform.position;

        }
        if (canRight && (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            this.transform.position = closestRight.transform.position;
        }
        if (canDown && Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.transform.position = closestDown.transform.position;
        }
        if (canUp && Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.transform.position = closestUp.transform.position;
            //whiteCube.transform.position = cellCenterPos + new Vector3Int(0, 1, -1);
        }
        print("right" + closestRight);
        print("left" + closestLeft);
        print("up" + closestUp);
        print("down" + closestDown);
        if (firstSpace == true && (Input.GetKeyDown(KeyCode.Space)))
        {
            canRight = false;
            canUp = false;
            canDown = false;
            canLeft = false;
            selecting = false;
            firstSpace = false;
        }
        else if (firstSpace == false && (Input.GetKeyDown(KeyCode.Space)))
        {
            canRight = true;
            canUp = true;
            canDown = true;
            canLeft = true;
            selecting = true;
            firstSpace = true;
        }
        if (!selecting) { this.transform.position = currentCube.transform.position; }
    }

    private void OnTriggerStay(Collider collision)
    {
        //print(collision.name);
        var multiTag = collision.gameObject.GetComponent<MultiTag>();
        if (multiTag != null && multiTag.HasTag("MoveCube"))
        {
            currentCube = collision.gameObject;
            onCube = true;
            collision.gameObject.GetComponent<Outline>().enabled = true;
            if (!selecting)
            {
                currentCube.transform.GetChild(0).gameObject.SetActive(true);
            }
            if (selecting)
            { currentCube.transform.GetChild(0).gameObject.SetActive(false); }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Outline>() != null)
        { other.GetComponent<Outline>().enabled = false; }

    }
    // GameObject GetClosestObject()
    //{
    /*        GameObject closest = null;
            for (int i = 0; i < cubes.Count; i++)
            {
                GameObject obj = cubes[i];
                Vector3 dir = obj.transform.position - this.transform.forward;
                //Vector3 dir1 = closest.transform.position - this.transform.position;
                if (!closest)
                {
                    closest = obj;
                    dir1 = closest.transform.position - this.transform.position;
                } 
                else
                {
                    if (dir1.sqrMagnitude > dir.sqrMagnitude)
                    {
                        closest = obj;
                    }
                }
            }
            print("Var "+closest.name);

            return closest;*/
    /* for (int i = 0; i < cubes.Count; i++)
     {
         if (!closestObject)
         {
             closestObject = cubes[i];
         }
         if ((Vector3.Distance(this.transform.position, cubes[i].transform.forward) <= Vector3.Distance(this.transform.position, closestObject.transform.position)))
         {
             closestObject = cubes[i];
         }

     }
     return closestObject;
 }
GameObject CheckObjects()
{
    List<GameObject> InAngle = new List<GameObject>();

    for (int i = 0; i < cubes.Count; i++)
    {
        GameObject tested = cubes[i];

        Vector3 dir = tested.transform.position - this.transform.forward;



            InAngle.Add(tested);

    }

    GameObject closest = null;

    for (int j = 0; j < InAngle.Count; j++)
    {
        GameObject tested = InAngle[j];

        Vector3 dir1 = tested.transform.position - this.transform.position;
        Vector3 dir2 = closest.transform.position - this.transform.position;

        if (!closest)
        {
            closest = tested;
        }
        else
        {
            if (dir2.sqrMagnitude > dir1.sqrMagnitude)
            {
                closest = tested;
            }
        }
    }

    return closest;
}
*/
}

