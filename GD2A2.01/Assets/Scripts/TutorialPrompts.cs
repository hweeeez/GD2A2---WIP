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
        if (firstUp && (Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.DownArrow)))
        {
            upArrow.SetActive(false);
            downArrow.SetActive(false);
            spaceBar.SetActive(true);
            firstUp = false;
        }
        if (firstSpace && Input.GetKeyDown(KeyCode.Space))
        {
            spaceBar.SetActive(false);
            upArrow.SetActive(true);
            downArrow.SetActive(true);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            firstSpace = false; StartCoroutine(promptUp());

        }
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

    IEnumerator promptUp()
    {
        yield return new WaitForSeconds(4f);
        upArrow.SetActive(false);
        leftArrow.SetActive(false);
        downArrow.SetActive(false);
        rightArrow.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        spaceBar.SetActive(true);
        yield return new WaitForSeconds(3f);
        spaceBar.SetActive(false);
        yield return new WaitForSeconds(2f);
        enterKey.SetActive(true);
        yield return new WaitForSeconds(4f);
        enterKey.SetActive(false);
    }
}
