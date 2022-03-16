using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class StartCube : MonoBehaviour
{
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
    void Update()
    {
        
    }
}
