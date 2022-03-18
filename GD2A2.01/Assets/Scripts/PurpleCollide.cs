using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleCollide : MonoBehaviour
{
    public GameObject otherPurpleCube;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Player")
            {
                Debug.Log("COLLIDED");
            StartCoroutine(teleport());           }
        }
    IEnumerator teleport()
    {
        player.GetComponent<Animator>().SetTrigger("Jump");
        yield return new WaitForSeconds(1.3f);
        player.transform.position = otherPurpleCube.transform.position + new Vector3(0, -0.5f, 1f);
        player.GetComponent<Animator>().ResetTrigger("Jump");

    }
}
