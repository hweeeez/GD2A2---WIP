using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (collision.gameObject.tag == "Red")
        {
            animator.SetTrigger("Death");
            StartCoroutine(DeathAnim());
        }
        if (collision.gameObject.tag == "Purple")
        {
            Debug.Log("COLLIDED");
            StartCoroutine(teleport());
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
        this.transform.position = otherPurpleCube.transform.position + new Vector3(0, -0.5f, 1f);
        animator.GetComponent<Animator>().ResetTrigger("Jump");
        movePlayer = true;

    }

}
