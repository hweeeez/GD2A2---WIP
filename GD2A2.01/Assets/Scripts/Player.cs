using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Vector3 purpleTele;
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
        transform.position = waypoints[waypointIndex].transform.position;

        purpleCubes = new List<GameObject>();
        multiTag = GameObject.FindObjectsOfType<MultiTag>();
        for (int i = 0; i < multiTag.Length; i++)
        {
            if (multiTag[i].HasTag("Purple"))
            {
                purpleCubes.Add(multiTag[i].gameObject);
            }
        }

        animator = gameObject.GetComponent<Animator>();
        otherPurpleCube = GameObject.FindGameObjectWithTag("Purple");
    }
    void Move()
    {
        Vector3 targetDir = waypoints[waypointIndex].position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 5f * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
        this.transform.position = Vector3.MoveTowards(this.transform.position,
                                       waypoints[waypointIndex].transform.position,
                                       speed * Time.deltaTime);
        if (this.transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }

        if (waypointIndex == 1)//waypoints.Length)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            movePlayer = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        print(waypointIndex);
        Vector3 endPos = target.position + new Vector3(0, -0.5f, 1);
        if (Input.GetKeyDown(KeyCode.Return))
        {
            animator.SetBool("Walking", true);
            movePlayer = true;

        }
        if (movePlayer)
        {
            Move();
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
                }
            }
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
        this.transform.position = otherPurpleCube.transform.position + purpleTele;
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
        PlayerPrefs.SetInt("lastscene", SceneManager.GetActiveScene().buildIndex + 1);
    }
}
