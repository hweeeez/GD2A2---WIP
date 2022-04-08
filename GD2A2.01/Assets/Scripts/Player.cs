using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject winSFX;

    private GameObject otherPurpleCube;
    List<GameObject> purpleCubes;
    private bool movePlayer = false;
    private float speed = 2f;
    Animator animator;
    public Transform target;
    MultiTag[] multiTag;
    // Start is called before the first frame update
    void Start()
    { purpleCubes = new List<GameObject>();
        multiTag = GameObject.FindObjectsOfType<MultiTag>();
        for (int i=0; i<multiTag.Length; i++)
        {
            if (multiTag[i].HasTag("Purple"))
            {
                purpleCubes.Add(multiTag[i].gameObject);
            }
        }
        
        animator = gameObject.GetComponent<Animator>();
        otherPurpleCube = GameObject.FindGameObjectWithTag("Purple");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 endPos = target.position + new Vector3(0, -0.5f, 1);
        if (Input.GetKeyDown(KeyCode.Return))
        {
            animator.SetBool("Walking", true);
            movePlayer = true;

        }
        if (movePlayer)
        {

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);
        }
        else { animator.SetBool("Walking", false); }
    }

    void OnCollisionEnter(Collision collision)
    {
        var multiTag = collision.gameObject.GetComponent<MultiTag>();

        if (multiTag != null && multiTag.HasTag("Purple"))
        {
            foreach(GameObject go in purpleCubes)
            {
                if(go != collision.gameObject)
                {
                    otherPurpleCube = go;
                }
            }
            Debug.Log("COLLIDED");
            StartCoroutine(teleport());
        }
        if (collision.gameObject.tag == "Green")
        {
            animator.SetTrigger("Win");
            movePlayer = false;
            StartCoroutine(LoadNextLevel());
        }

        if (multiTag != null && multiTag.HasTag("Red"))
        {
            movePlayer = false;
            animator.SetTrigger("Death");
            StartCoroutine(DeathAnim());
        }
    }
    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(4f);
        this.transform.localScale = new Vector3(0f, 0f, 0f);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    IEnumerator teleport()
    {
        movePlayer = false;
        animator.SetTrigger("Jump");
        yield return new WaitForSeconds(0.5f);
        this.transform.position = otherPurpleCube.transform.position + new Vector3(0, -0.5f, -1f);
        yield return new WaitForSeconds(0.2f);
        animator.GetComponent<Animator>().ResetTrigger("Jump");
        movePlayer = true;
    }

    IEnumerator LoadNextLevel()
    {
        winSFX.SetActive(true);
        yield return new WaitForSeconds(1f);
        //This will load the next scene in the buildIndex, e.g if in scene 3, go to scene 4
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
