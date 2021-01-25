using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject kursun;
    public Transform kursunyeri;
    float ates = 0;
    Rigidbody2D rgd;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > ates && anim.GetBool("IsShot"))
        {
            ates = Time.time + 1.3f;
            GameObject cl = (GameObject)Instantiate(kursun, kursunyeri.position, Quaternion.identity);
            Destroy(cl, 1f);
        }
    }
}
