using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class WhiteCube : MonoBehaviour
{
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

    // Start is called before the first frame update
    /*   void Start()
       {
           Vector3 worldPos = whiteCube.transform.position;
           cubecell = tilemap.WorldToCell(worldPos);
           Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cubecell);
           whiteCube.transform.position = cellCenterPos + new Vector3Int(0, 1, 0);
       }*/
    private void AddCommand(Command command)
    {
        var idx = undo.ExecuteCommand(command);

    }
    private void Start()
    {
        undo = gameman.GetComponent<Undo>();
        cubes = GameObject.FindGameObjectsWithTag("Cube");
        cubePositions = new Vector3[cubes.Length];
    }
    public void Update()
    {

        cubecell = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cubecell);
        Vector3 thispos = this.transform.position;
        var hitColliders = Physics.OverlapSphere(thispos, 0.5f);

        Vector3 moveleft = cellCenterPos + new Vector3Int(1, 1, 0);
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
            if (Vector3.Distance(cubePositions[i], moveleft) < 0.0001) { canLeft = false; }
        }
        
        /*  if (hitColliders.Length > 0)
          {
              print("bump");
          }*/


        if (canLeft && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            whiteCube.transform.position = cellCenterPos + new Vector3Int(1, 1, 0);
   
        }
        if (canRight && (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            whiteCube.transform.position = cellCenterPos + new Vector3Int(-1, 1, 0);
        }
        if (canDown && Input.GetKeyDown(KeyCode.DownArrow))
        {
            whiteCube.transform.position = cellCenterPos + new Vector3Int(0, 1, 1);
        }
        if (canUp && Input.GetKeyDown(KeyCode.UpArrow))
        {
            whiteCube.transform.position = cellCenterPos + new Vector3Int(0, 1, -1);
        }


    }
}
