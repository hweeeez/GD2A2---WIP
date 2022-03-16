using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class RedCube : MonoBehaviour
{
    public bool canMove;
    public GameObject player;
    public GameObject redCube;
    Vector3Int cubecell;
    public Tilemap tilemap;
    // Start is called before the first frame update
  /*  void Start()
    {
        Vector3 worldPos = redCube.transform.position;
        cubecell = tilemap.WorldToCell(worldPos);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cubecell);
        redCube.transform.position = cellCenterPos + new Vector3Int(0, 1, 0);
    }*/

    // Update is called once per frame
    public void Update()
    {
        cubecell = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cubecell);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            redCube.transform.position = cellCenterPos + new Vector3Int(2, 1, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            redCube.transform.position = cellCenterPos + new Vector3Int(-2, 1, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            redCube.transform.position = cellCenterPos + new Vector3Int(0, 1, 2);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            redCube.transform.position = cellCenterPos + new Vector3Int(0, 1, -2);
        }
    }
    // Update is called once per frame
 
}
