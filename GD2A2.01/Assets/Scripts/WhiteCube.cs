using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class WhiteCube : MonoBehaviour
{
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

    private void Start()
    {
        cubes = GameObject.FindGameObjectsWithTag("Cube");
        cubePositions = new Vector3[cubes.Length];
    }
    public void Update()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            cubePositions[i] = cubes[i].transform.position;

        }

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
            if (cubePositions[i] != moveleft) { canLeft = true; } else { canLeft = false; }
            if (cubePositions[i] != moveRight) { canRight = true; } else { canRight = false; }
            if (cubePositions[i] != moveUp) { canUp = true; } else { canUp = false; }
            if (cubePositions[i] != moveDown) { canDown = true; } else { canDown = false; }


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
