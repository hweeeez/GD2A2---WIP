using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    private bool selecting = true;
    public GameObject[] Cubes;
    public bool isSelected;
    private GameObject Cube;
    public int currentCube;
    // Start is called before the first frame update
    void Start()
    {
        Cube = Cubes[0];
        foreach(GameObject Cube in Cubes){
            Cube.GetComponent<Outline>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (selecting)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentCube += 1;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentCube -= 1;
            }
        }
        Cube = Cubes[currentCube];
        Cube.GetComponent<Outline>().enabled = true;
        GameObject ChildGameObject = Cube.transform.GetChild(0).gameObject;
        print(ChildGameObject);
        foreach (GameObject Cube in Cubes)
        {if(Cube != Cubes[currentCube])
            Cube.GetComponent<Outline>().enabled = false;
        }
       if (selecting == true && (Input.GetKeyDown(KeyCode.Space)))
        {
                ChildGameObject.SetActive(true);
            selecting = false;

        }
     else if (selecting == false && (Input.GetKeyDown(KeyCode.Space))) {
        
            ChildGameObject.SetActive(false);
            selecting = true;

        }
    }
}
