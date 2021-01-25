using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KarakterKontrol : MonoBehaviour
{
    public ParticleSystem dust;
    public Sprite[] idle;
    public Sprite[] jump;
    public Sprite[] walk;
    public Sprite[] dead;

    
    SpriteRenderer spriteRenderer;

    int idleanimcount = 0;
    int walkanimcount = 0;
    int climbanimcount = 0;
    int dieanimcount = 0;
    public float speed = 1;
    public int jumpsp = 180;

    Rigidbody2D rb2d;

    float horizontal = 0;
    float vertical = 0;

    float idletime = 0;
    float walktime = 0;
    public int kalp = 3;
    public Camera mcam;
    Vector3 vecx;
    Vector3 vecy;
    Vector3 cameralastPos;
    Vector3 camerafirstPos;

    public GameObject Top;
    public Vector2 velocity;
    public float cooldown = 1f;
    bool canShoot = true;
    bool isSnow = false;
    bool spacejump = true;
    public Text text;
    GameObject camera;
    GameObject slow;
    bool reborn = false;
    KarakterKontrol2 and;
    GameObject ss;
    GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        ss = GameObject.FindGameObjectWithTag("sCharacter");
        and = GameObject.Find("SecondCharacter").GetComponent<KarakterKontrol2>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.transform.position = new Vector3(transform.position.x,transform.position.y,-10);
        camerafirstPos = camera.transform.position - transform.position;
        slow = GameObject.FindGameObjectWithTag("slow");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (spacejump)
            {
                rb2d.AddForce(new Vector2(0, jumpsp));
                spacejump = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && GameObject.Find("SecondCharacter").GetComponent<Anim>().istabled)
        {
            and.enabled = true;
            ss.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            ss.GetComponent<Anim>().mY.SetActive(false);
            ss.GetComponent<Anim>().enabled = false;
            gameObject.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.F) && canShoot)
        {
            GameObject go = (GameObject)Instantiate(Top, (Vector2)transform.position + new Vector2(0.03f,0.01f)* transform.localScale.x, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x*3,0);
            Destroy(go, 1.5f);
            StartCoroutine(CanShoot());
        }
        
    }
    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;

    }
    void LateUpdate()
    {
        camCont();
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        jumpsp = 0;
        spacejump = true;
        if(col.collider.tag == "die")
        {
            GameControl gm = GameObject.FindGameObjectWithTag("Control").GetComponent<GameControl>();
            gm.sayac--;
        }
        if (col.collider.tag == "zemin")
        {
            enemy.GetComponent<Animator>().SetBool("IsShot", false);
        }
        if (col.collider.tag == "slow")
        { speed = 0.5f; jumpsp = 150; isSnow = true; }
        else
        { speed = 1; jumpsp = 180; isSnow = false; }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "ladder")
        {
            enemy.GetComponent<Animator>().SetBool("IsShot", true);
            ClimbLadder();
        }
        if (col.tag == "die")
        {
            GameControl gm = GameObject.FindGameObjectWithTag("Control").GetComponent<GameControl>();
            //spriteRenderer.sortingOrder = 0;
            spriteRenderer.sprite = dead[dieanimcount++];
            if (spriteRenderer.sprite == dead[7])
                dieanimcount = 7;
            transform.position = new Vector3(-6.61f, -4.24f, 0);
        }
        if(col.tag == "end")
        {
            StartCoroutine(EndingPart());
            col.isTrigger = false;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Kalp")
        {
            GameControl gm = GameObject.FindGameObjectWithTag("Control").GetComponent<GameControl>();
            if(gm.sayac != 3)
            {
                gm.sayac++;
            }
            Destroy(collision.gameObject);
            AudioSource gmx = GameObject.FindGameObjectWithTag("Kalp").GetComponent<AudioSource>();
            gmx.Play(0);
        }
    }
    IEnumerator EndingPart()
    {
        
        string str1 = "SONUNDA GUCUNE KAVUSTUN";
        text.text = str1;
        yield return new WaitForSeconds(3);
        text.text = "";
        yield return new WaitForSeconds(2);
        text.text = "Bu oyunun basa sarmasi gerekiyor";
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Final1");
    }
    IEnumerator YenidenDogma()
    {
        yield return new WaitForSeconds(2);
    }
    void camCont()
    {
        cameralastPos = camerafirstPos + transform.position;
        //camera.transform.position = cameralastPos;
        camera.transform.position = Vector3.Lerp(camera.transform.position, cameralastPos, 0.02f);
    }
    void FixedUpdate()
    {
        
        Animation();
        CharacterMovement();
    }
    void Animation()
    {
        if(spacejump)
        {
            if (horizontal == 0)
            {
                idletime += Time.deltaTime;
                if (idletime > 0.05f)
                {
                    spriteRenderer.sprite = idle[idleanimcount++];
                    if (idleanimcount == idle.Length)
                        idleanimcount = 0;
                    idletime = 0;
                }
            }
            else if (horizontal > 0)
            {
                if(isSnow)
                    CreateDust();
                walktime += Time.deltaTime;
                if (walktime > 0.075f)
                {
                    spriteRenderer.sprite = walk[walkanimcount++];
                    if (walkanimcount == walk.Length)
                        walkanimcount = 0;
                    walktime = 0;
                }
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                if(isSnow)
                    CreateDust();
                walktime += Time.deltaTime;
                if (walktime > 0.075f)
                {
                    spriteRenderer.sprite = walk[walkanimcount++];
                    if (walkanimcount == walk.Length)
                        walkanimcount = 0;
                    walktime = 0;
                }
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (rb2d.velocity.y > 0)
            { spriteRenderer.sprite = jump[0];}
            else
            { spriteRenderer.sprite = jump[1];}
            if(horizontal > 0)
               transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
        
    }
    void CharacterMovement()
    {
        
        horizontal = Input.GetAxisRaw("Horizontal");
        vecx = new Vector3(horizontal * speed , rb2d.velocity.y, 0);
        rb2d.velocity = vecx;
        
    }
    void ClimbLadder()
    {
        vertical = Input.GetAxisRaw("Vertical");
        vecy = new Vector3(rb2d.velocity.x, vertical * speed, 0);
        rb2d.velocity = vecy;
    }
    void CreateDust()
    {
        dust.Play();
    }
}
