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
                player.transform.position = otherPurpleCube.transform.position + new Vector3(0, -0.5f, 1f);
            }
        }
    
}
