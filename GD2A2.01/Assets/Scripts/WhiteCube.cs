using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class WhiteCube : MonoBehaviour
{
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject upWall;
    public GameObject downWall;

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

    private void AddCommand(Command command)
    {
        var idx = undo.ExecuteCommand(command);

    }
    private void Start()
    {
        undo = gameman.GetComponent<Undo>();
        cubes = GameObject.FindGameObjectsWithTag("Cube");
        cubePositions = new Vector3[cubes.Length];
        positions = new[] { new Vector3(-0.5f, 1, 4.5f), new Vector3(-0.5f, 1, 5.5f), new Vector3(-0.5f, 1, 1.5f), new Vector3(-0.5f, 1, 6.5f), new Vector3(-0.5f, 1, 7.5f) };
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
            if (Vector3.Distance(cubePositions[i], moveRight) < 1.7) { canRight = false; } else { canRight = true; }
            if (Vector3.Distance(cubePositions[i], moveLeft) < 0.0001) { canLeft = false; }
        }

        if ((leftWall.transform.position.x - moveLeft.x) < 0.0001) { canLeft = false; }
        if ((rightWall.transform.position.x - moveRight.x) > 0.4) { canRight = false; } else { canRight = true; }
        if ((upWall.transform.position.z - moveUp.z) > 0.0001) { canUp = false; }
        if ((downWall.transform.position.z - moveDown.z) < 0.0001) { canDown = false; }
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
