using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kursun : MonoBehaviour
{
    Rigidbody2D rgd;
    float hiz = 2f;
    GameObject gC;
    GameControl gxc;
    // Start is called before the first frame update
    void Start()
    {
        gC = GameObject.FindGameObjectWithTag("Control");
        rgd = GetComponent<Rigidbody2D>();
        rgd.velocity = (1)*transform.right * hiz;
        gxc = gC.GetComponent<GameControl>();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "sCharacter" || collision.tag == "mCharacter")
        {
            gxc.sayac--;
        }
    }
}
