using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject otherPurpleCube;
    private bool movePlayer = false;
    private float speed = 2f;
    Animator animator;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
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
        print(collision.gameObject.name);
        var multiTag = collision.gameObject.GetComponent<MultiTag>();

        if (multiTag != null && multiTag.HasTag("Purple"))
        {
            Debug.Log("COLLIDED");
            StartCoroutine(teleport());
        }
        if (collision.gameObject.tag == "Green")
        {
            animator.SetTrigger("Win");
            movePlayer = false;

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
        Destroy(this.gameObject);
    }
    IEnumerator teleport()
    {
        movePlayer = false;
        animator.SetTrigger("Jump");
        yield return new WaitForSeconds(1.4f);
        this.transform.position = otherPurpleCube.transform.position + new Vector3(0, -0.5f, -1f);
        yield return new WaitForSeconds(0.2f);
        animator.GetComponent<Animator>().ResetTrigger("Jump");
        movePlayer = true;
    }

    private IEnumerator loadScene(string scene)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }
}
