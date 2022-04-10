using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class WhiteCube : MonoBehaviour
{
    public bool occupied;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject upWall;
    public GameObject downWall;
    bool isBlue;
    public GameObject gameman;
    private Undo undo;
    bool canLeft;
    bool canRight;
    bool canUp;
    bool canDown;
    Vector3[] cubePositions;
    private GameObject[] cubes;
    bool canMove;
    public GameObject whiteCube;
    Vector3Int cubecell;
    public Tilemap tilemap;
    Vector3[] positions;
    bool isPurple;
    public LayerMask cubeMask;
    public LayerMask greenMask;
    private void AddCommand(Command command)
    {
        var idx = undo.ExecuteCommand(command);
        
    }
    private void Start()
    {
        cubeMask = LayerMask.GetMask("PlayerCube") ;
        greenMask = LayerMask.GetMask("Green");

        var multiTag = this.gameObject.GetComponentInParent<MultiTag>();
        if (multiTag != null && multiTag.HasTag("Purple"))
        {         
            isPurple = true;
        }
        if (multiTag != null && multiTag.HasTag("Blue"))
        {
            print("blue");
            isBlue = true;
        }
        undo = gameman.GetComponent<Undo>();
        cubes = GameObject.FindGameObjectsWithTag("Cube");
        cubePositions = new Vector3[cubes.Length];
        //positions = new[] { new Vector3(-0.5f, 1, 4.5f), new Vector3(-0.5f, 1, 5.5f), new Vector3(-0.5f, 1, 1.5f), new Vector3(-0.5f, 1, 6.5f), new Vector3(-0.5f, 1, 7.5f) };
    }
    public void Update()
    {
        cubecell = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cubecell);
        Vector3 thispos = this.transform.position;
        var hitColliders = Physics.OverlapSphere(thispos, 0.5f);

        Vector3 moveLeft = cellCenterPos + new Vector3Int(1, 1, 0);
        Vector3 moveRight = cellCenterPos + new Vector3Int(-1, 1, 0);
        Vector3 moveDown = cellCenterPos + new Vector3Int(0, 1, 1);
        Vector3 moveUp = cellCenterPos + new Vector3Int(0, 1, -1);
        cubes = GameObject.FindGameObjectsWithTag("Cube");
        for (int i = 0; i < cubes.Length; i++)
        {
            cubePositions[i] = cubes[i].transform.position;

        }
        canLeft = true;
        canRight = true;
        canUp = true;
        canDown = true;
        for (int i = 0; i < cubes.Length; i++)
        {
            if (Vector3.Distance(cubePositions[i], moveUp) < 0.0001) { canUp = false; }
            if (Vector3.Distance(cubePositions[i], moveDown) < 0.0001) { canDown = false; }
            if (Vector3.Distance(cubePositions[i], moveRight) < 0.0001) { canRight = false; }
            if (Vector3.Distance(cubePositions[i], moveLeft) < 0.0001) { canLeft = false; }
        }
        LayerMask bothMask = greenMask | cubeMask;
        if (isPurple)
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.back), out hit, 1.4f, bothMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.red);
                Debug.Log(hit.collider.name);
                if (hit.collider.tag == "Green")
                {
                    occupied = false;
                    Debug.Log("green");
                    canUp = false;
                }
                if (hit.collider.tag != "Green")
                {
                    occupied = true;
                }
            }
            else { occupied = false; }
        }
        print(occupied);
        /*for (int i = 0; i < positions.Length; i++)
        { if ( cubePositions[i])
            {
                print("empty");
            }
        }*/
        /*  if (hitColliders.Length > 0)
          {
              print("bump");
          }*/


        if (canLeft && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isBlue)
            {
                whiteCube.transform.position = cellCenterPos + new Vector3Int(2, 1, 0);

            }
            else
            {
                whiteCube.transform.position = cellCenterPos + new Vector3Int(1, 1, 0);
            }
        }
        if (canRight && (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            if (isBlue)
            {
                whiteCube.transform.position = cellCenterPos + new Vector3Int(-2, 1, 0);

            }
            else {
                whiteCube.transform.position = cellCenterPos + new Vector3Int(-1, 1, 0);
            }
        }
        if (canDown && Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isBlue)
            {
                whiteCube.transform.position = cellCenterPos + new Vector3Int(0, 1, 2);

            }
            else {
                whiteCube.transform.position = cellCenterPos + new Vector3Int(0, 1, 1);
            }
        }
        if (canUp && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isBlue)
            {
                whiteCube.transform.position = cellCenterPos + new Vector3Int(0, 1, -2);

            }
            else { 
                whiteCube.transform.position = cellCenterPos + new Vector3Int(0, 1, -1);
        }}
     
       
    }
    }
