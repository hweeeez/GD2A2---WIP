using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPrompts : MonoBehaviour
{
    public GameObject upArrow;
    public GameObject downArrow;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject spaceBar;
    public GameObject enterKey;
    bool firstSpace = true;
    bool firstUp = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (firstUp && (Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.DownArrow) | Input.GetKeyDown(KeyCode.LeftArrow) | Input.GetKeyDown(KeyCode.RightArrow)))
        {
            StartCoroutine(arrowsFalse());
            firstUp = false;
        }
        else if (!firstUp && (Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.DownArrow) | Input.GetKeyDown(KeyCode.LeftArrow) | Input.GetKeyDown(KeyCode.RightArrow)))
        {
            StartCoroutine(arrowsFalse());
        }
            if (firstSpace && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(spaceFalse());
            firstSpace = false; 
        }
            else if (!firstSpace && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(enterTrue());
        }
            if(!firstSpace && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(enterFalse())
;        }
        /*        if (!firstUp && (Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.DownArrow) | Input.GetKeyDown(KeyCode.RightArrow) | Input.GetKeyDown(KeyCode.LeftArrow)))
                {
                    upArrow.SetActive(false);
                    leftArrow.SetActive(false);
                    downArrow.SetActive(false);
                    rightArrow.SetActive(false);
                    spaceBar.SetActive(true);
                    StartCoroutine(promptUp());
                }*/

    }
    IEnumerator arrowsFalse()
    {
        yield return new WaitForSeconds(1f);
        upArrow.SetActive(false);
        rightArrow.SetActive(false);
        downArrow.SetActive(false);
        leftArrow.SetActive(false);
        spaceBar.SetActive(true);
    }
    IEnumerator spaceFalse()
    {
        yield return new WaitForSeconds(1f);
        spaceBar.SetActive(false);
        upArrow.SetActive(true);
        rightArrow.SetActive(true);
        downArrow.SetActive(true);
        leftArrow.SetActive(true);
    }
    IEnumerator enterTrue()
    {    
        yield return new WaitForSeconds(1f);
        spaceBar.SetActive(false);
        enterKey.SetActive(true);
       
    }
    IEnumerator enterFalse()
    {
        yield return new WaitForSeconds(1f);
        enterKey.SetActive(false);
    }
}
