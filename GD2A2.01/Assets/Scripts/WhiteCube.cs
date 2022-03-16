using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class WhiteCube : MonoBehaviour
{
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

    // Update is called once per frame
    public void Update()
    {
        cubecell = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cubecell);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            whiteCube.transform.position = cellCenterPos + new Vector3Int(1, 1, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            whiteCube.transform.position = cellCenterPos + new Vector3Int(-1, 1, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            whiteCube.transform.position = cellCenterPos + new Vector3Int(0, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            whiteCube.transform.position = cellCenterPos + new Vector3Int(0, 1, -1);
        }
    }
}
