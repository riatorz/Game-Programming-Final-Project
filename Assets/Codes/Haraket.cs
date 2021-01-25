using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haraket : MonoBehaviour
{
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}
