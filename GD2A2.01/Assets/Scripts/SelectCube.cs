using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SelectCube : MonoBehaviour
{

    public GameObject gameman;
    private Undo undo;
    bool canLeft;
    bool canRight;
    bool canUp;
    bool canDown;
    Vector3[] cubePositions;
    private GameObject[] cubes;
    private GameObject Cube;
    public int currentCube;
    bool canMove;
    public GameObject whiteCube;
    Vector3Int cubecell;
    public Tilemap tilemap;
    GameObject[] go;
    List<GameObject> barriers;
    List<Vector3> barrierPos;
    // Start is called before the first frame update
    void Start()
    {

        barriers = new List<GameObject>();
        barrierPos = new List<Vector3>();
        go = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in go)
        {
            if (go.layer == 7)
            {
                barriers.Add(go);
            }
        }
        foreach (GameObject go in barriers)
        {
            barrierPos.Add(go.transform.position);

        }

    }

    // Update is called once per frame
    void Update()
    {
        canLeft = true;
        canRight = true;
        canUp = true;
        canDown = true;

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
    private void OnTriggerEnter(Collider collision)
    {
        var multiTag = collision.gameObject.GetComponent<MultiTag>();

        if (multiTag != null && multiTag.HasTag("MoveCube"))
        {
            Debug.Log("COLLIDED");
            collision.gameObject.GetComponent<Outline>().enabled = true;
        }
    }
}
