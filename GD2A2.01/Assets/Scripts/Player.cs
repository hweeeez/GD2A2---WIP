using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject winSFX;

    [SerializeField]
    Transform[] waypoints;
    int waypointIndex = 0;

    private GameObject otherPurpleCube;
    List<GameObject> purpleCubes;
    private bool movePlayer = false;

    private float speed = 2f;
    Animator animator;
    public Transform target;
    MultiTag[] multiTag;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("firsttime", 1);


        //transform.position = waypoints[waypointIndex].transform.position;
        animator = gameObject.GetComponent<Animator>();
        purpleCubes = new List<GameObject>();
        multiTag = GameObject.FindObjectsOfType<MultiTag>();
        for (int i = 0; i < multiTag.Length; i++)
        {
            if (multiTag[i].HasTag("Purple"))
            {
                purpleCubes.Add(multiTag[i].gameObject);
            }
        }
        otherPurpleCube = GameObject.FindGameObjectWithTag("Purple");

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 endPos = target.position + new Vector3(0, -0.5f, 1);
        if (Input.GetKeyDown(KeyCode.Return))
        {
            print("walk");

            movePlayer = true;

        }
        if (movePlayer)
        {
            animator.SetBool("Walking", true);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);

            // float step = speed * Time.deltaTime;
            // transform.position = Vector3.MoveTowards(transform.position, endPos, step);
        }
        else { animator.SetBool("Walking", false); }

    }

    void OnCollisionEnter(Collision collision)
    {
        var multiTag = collision.gameObject.GetComponent<MultiTag>();

        if (multiTag != null && multiTag.HasTag("Purple"))
        {
            foreach (GameObject go in purpleCubes)
            {
                if (go != collision.gameObject)
                {
                    otherPurpleCube = go;
                    if (otherPurpleCube.transform.GetChild(0).gameObject.GetComponent<WhiteCube>().occupied == true)
                    {
                        StartCoroutine(reloadscene());
                        movePlayer = false;
                        print("cannotmove");
                        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                    else { StartCoroutine(teleport()); }
                }

            }
        }

        if (collision.gameObject.tag == "Green")
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("L10"))
            {
                StartCoroutine(loadHome());
            }
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
    IEnumerator reloadscene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(3.5f);
        this.transform.localScale = new Vector3(0f, 0f, 0f);
        yield return new WaitForSeconds(2f);
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
        yield return new WaitForSeconds(2.5f);
        //This will load the next scene in the buildIndex, e.g if in scene 3, go to scene 4
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //( if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("scene1"))  
        PlayerPrefs.SetInt("lastscene", SceneManager.GetActiveScene().buildIndex + 1);

    }
    IEnumerator loadHome()
    {
        winSFX.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Home");
    }




}

