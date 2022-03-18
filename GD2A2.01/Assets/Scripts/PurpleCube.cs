using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PurpleCube : MonoBehaviour
{
    public GameObject purpleCube;
    public GameObject otherPurpleCube;
    public GameObject player;

    Vector3Int cubecell;
    public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 worldPos = transform.position;
        cubecell = tilemap.WorldToCell(worldPos);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cubecell);
        transform.position = cellCenterPos + new Vector3Int(0, 1, 0);
    }

    // Update is called once per frame
    public void Update()
    {
        cubecell = tilemap.WorldToCell(purpleCube.transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cubecell);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            purpleCube.transform.position = cellCenterPos + new Vector3Int(2, 1, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            purpleCube.transform.position = cellCenterPos + new Vector3Int(-2, 1, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            purpleCube.transform.position = cellCenterPos + new Vector3Int(0, 1, 2);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            purpleCube.transform.position = cellCenterPos + new Vector3Int(0, 1, -2);
        }
    }

}
