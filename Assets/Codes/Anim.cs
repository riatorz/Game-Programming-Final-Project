using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    public GameObject mY;
    SpriteRenderer spriteRenderer;
    int idleanimcount = 0;
    float idletime = 0;
    public bool ischaracter = true;
    public Sprite[] idle;
    public bool istabled;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ischaracter)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-5,0), 1f, LayerMask.GetMask("SC"));
            if (hit)
            {
                mY.SetActive(true);
                istabled = true;
            }
        }
        idletime += Time.deltaTime;
        if (idletime > 0.05f)
        {
            spriteRenderer.sprite = idle[idleanimcount++];
            if (idleanimcount == idle.Length)
                idleanimcount = 0;
            idletime = 0;
        }
    }
}
