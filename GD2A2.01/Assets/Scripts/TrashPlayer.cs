using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashPlayer : MonoBehaviour
{
    Vector3 targetDir;
    Vector3 newDir;
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
    Transform[] cubePos;
    List<GameObject> cubes;
    [SerializeField]
    public List<Transform> wayPoints;
    public LayerMask platform;
    // Start is called before the first frame update
    void Start()
    {
       // wayPoints = new List<Transform>();
        //transform.position = waypoints[waypointIndex].transform.position;
        cubes = new List<GameObject>();
        purpleCubes = new List<GameObject>();
        multiTag = GameObject.FindObjectsOfType<MultiTag>();
        for (int i = 0; i < multiTag.Length; i++)
        {
            if (multiTag[i].HasTag("MoveCube"))
            {
                cubes.Add(multiTag[i].gameObject);
            }
                if (multiTag[i].HasTag("Purple"))
            {
                purpleCubes.Add(multiTag[i].gameObject);
            }
        }
        cubePos = new Transform[cubes.Count];
        for (int i = 0; i < cubes.Count; i++)
        {
            cubePos[i] = cubes[i].transform;
            
        }
       /* foreach (GameObject go in GameObject.FindGameObjectsWithTag("waypoint"))
        {
            wayPoints.Add(go.GetComponent<Transform>());
 
     }*/
        //transform.position = wayPoints[0].position;
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
    Transform GetClosestEnemy(Transform[] cubePos)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in cubePos)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    
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
       for(int i = 0; i < wayPoints.Count; i++)
        {
            Physics.IgnoreCollision(wayPoints[i].GetComponent<BoxCollider>(), this.GetComponent<CapsuleCollider>());

            if (this.transform.position == wayPoints[i].position)
            {
                print("atpos");
                wayPoints[i].gameObject.SetActive(false);

            }
        }
            RaycastHit hit;
         
        // float step = speed * Time.deltaTime;
        // transform.position = Vector3.MoveTowards(transform.position, endPos, step);
        if (movePlayer)
        {
           /* if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, ~platform))
            {//front

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.red);
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.tag == "waypoint")
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position,
                                           hit.collider.transform.position,
                                           speed * Time.deltaTime);
                }
                else if (hit.collider.gameObject.GetComponent<MultiTag>() != null)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position,
                                           hit.collider.transform.position,
                                           speed * Time.deltaTime);
                }
                 targetDir = hit.collider.transform.position - transform.position;
                 newDir = Vector3.RotateTowards(transform.forward, targetDir, 5f * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }*/
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {

                Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.tag == "waypoint")
                {
                   
                    this.transform.position = Vector3.MoveTowards(this.transform.position,
                                           hit.collider.transform.position,
                                           speed * Time.deltaTime);
 
                        Physics.IgnoreCollision(hit.collider.gameObject.GetComponent<BoxCollider>(), this.GetComponent<CapsuleCollider>());
                    
                }
            /*    else if (hit.collider.gameObject.GetComponent<MultiTag>() != null)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position,
                                           hit.collider.transform.position,
                                           speed * Time.deltaTime);
                }*/
                targetDir = hit.collider.transform.position - transform.position;
                newDir = Vector3.RotateTowards(transform.forward, targetDir, 5f * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity, ~platform))
            {

                Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.green);
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.tag == "waypoint")
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position,
                                           hit.collider.transform.position,
                                           speed * Time.deltaTime);
                }
            /*    else if (hit.collider.gameObject.GetComponent<MultiTag>() != null)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position,
                                           hit.collider.transform.position,
                                           speed * Time.deltaTime);
                }*/
                targetDir = hit.collider.transform.position - transform.position;
                newDir = Vector3.RotateTowards(transform.forward, targetDir, 5f * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.left), out hit, Mathf.Infinity, ~platform))
            {

                Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.green);
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.tag == "waypoint")
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position,
                                           hit.collider.transform.position,
                                           speed * Time.deltaTime);
                }
           /*     else if (hit.collider.gameObject.GetComponent<MultiTag>() != null)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position,
                                           hit.collider.transform.position,
                                           speed * Time.deltaTime);
                }*/
                targetDir = hit.collider.transform.position - transform.position;
                newDir = Vector3.RotateTowards(transform.forward, targetDir, 5f * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
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
        transform.rotation = Quaternion.LookRotation(transform.position - otherPurpleCube.transform.position);
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
