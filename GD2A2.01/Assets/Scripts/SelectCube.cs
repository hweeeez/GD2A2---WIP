using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SelectCube : MonoBehaviour
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
    GameObject[] go;
    List<GameObject> barriers;
    List<Vector3> barrierPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { go = FindObjectsOfType(typeof(GameObject))as GameObject[];
        foreach(GameObject go in go)
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
        cubecell = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cubecell);
        Vector3 thispos = this.transform.position;
        var hitColliders = Physics.OverlapSphere(thispos, 0.5f);

        Vector3 moveLeft = cellCenterPos + new Vector3Int(1, 1, 0);
        Vector3 moveRight = cellCenterPos + new Vector3Int(-1, 1, 0);
        Vector3 moveDown = cellCenterPos + new Vector3Int(0, 1, 1);
        Vector3 moveUp = cellCenterPos + new Vector3Int(0, 1, -1);
        cubes = GameObject.FindGameObjectsWithTag("Cube");
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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Platform")
        {
            print("cannot move");
            canRight = false;
            canDown = false;
            canLeft = false;
            canUp = false;
        }
    }
}
