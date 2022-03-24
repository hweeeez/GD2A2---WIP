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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator promptUp()
    {
        upArrow.SetActive(true);
        downArrow.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        upArrow.SetActive(false);
        downArrow.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        spaceBar.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        spaceBar.SetActive(false);
        upArrow.SetActive(true);
        downArrow.SetActive(true);
        leftArrow.SetActive(true); 
        rightArrow.SetActive(true);
        yield return new WaitForSeconds(2.5f); 
        upArrow.SetActive(false); 
        leftArrow.SetActive(false); 
        rightArrow.SetActive(false);
        spaceBar.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        spaceBar.SetActive(false);
    }
}
