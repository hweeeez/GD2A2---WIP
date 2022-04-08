using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[] cubes;
    private Vector3[] cubePositions;
    private List<GameObject> moveCubes;
    private Vector3[] clearPositions;
    private GameObject[] clearCubes;
    private Undo undo;
    private bool selecting = true;
    public GameObject[] Cubes;
    public bool isSelected;
    private GameObject Cube;
    public int currentCube;

    public GameObject clearSFX;
    // Start is called before the first frame update
    void Start()
    {
        clearCubes = GameObject.FindGameObjectsWithTag("Clear");
        cubes = GameObject.FindGameObjectsWithTag("Cube");
        clearPositions = new Vector3[clearCubes.Length];
        cubePositions = new Vector3[Cubes.Length];

        undo = this.GetComponent<Undo>();
        Cube = Cubes[0];
        foreach (GameObject Cube in Cubes)
        {
            Cube.GetComponent<Outline>().enabled = false;

        }
        for (int i = 0; i < clearCubes.Length; i++)
        {
            clearPositions[i] = clearCubes[i].transform.position;

        }
    }

    // Update is called once per frame
    void Update()
    {
        bool atLeastOneCubeInClearPosition = false;
        for (int i = 0; i < Cubes.Length; i++)
        {
            Vector3 cubePos = Cubes[i].transform.position;
            for (int j = 0; j < clearPositions.Length; j++)
            {/*if(clearPositions[j] != cubePos)
                {
                    print("clear");
                    StartCoroutine(clearPlay());
                }*/
                if (clearPositions[j] == cubePos)
                {
                    atLeastOneCubeInClearPosition = true;
                    //  print("wrong!");

                    break;
                }
            }
            if (atLeastOneCubeInClearPosition) break;

        }
        //print(atLeastOneCubeInClearPosition);
           bool clear = false;
           if (!atLeastOneCubeInClearPosition && !clear)
           {
            print(atLeastOneCubeInClearPosition);
            StartCoroutine(clearPlay());
   clear = true;
           }

        /*  Cube = Cubes[currentCube];
          Cube.GetComponent<Outline>().enabled = true;
          GameObject ChildGameObject = Cube.transform.GetChild(0).gameObject;

          foreach (GameObject Cube in Cubes)
          {
              if (Cube != Cubes[currentCube])
                  Cube.GetComponent<Outline>().enabled = false;
          }

          if (Input.GetKeyDown(KeyCode.Z))
          {
              print("undo");
              int v = undo.UndoCommand();
          }*/
    }
    IEnumerator clearPlay()
    {
        clearSFX.SetActive(true);
       yield return new WaitForSeconds(2.5f);
        clearSFX.SetActive(false);
    }

    private void AddCommand(Command command)
    {
        var idx = undo.ExecuteCommand(command);

    }
}
