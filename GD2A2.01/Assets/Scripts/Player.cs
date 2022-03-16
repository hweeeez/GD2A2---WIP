using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool movePlayer = false;
    private float speed = 4f;
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
            movePlayer = true;
        
    }
        if (movePlayer)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Red")
        {
            Debug.Log("COLLIDED");
            animator.SetTrigger("Death");
            StartCoroutine(DeathAnim());

        }
    }
    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
}
}
