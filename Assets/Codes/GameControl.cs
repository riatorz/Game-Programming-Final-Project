using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public Image img1;
    public Image img2;
    public Sprite[] characters;
    public Sprite[] sprites;
    public Sprite[] dieanim;

    GameObject krk;
    public int sayac = 3;
    SpriteRenderer xk1;
    GameObject kK1;
    SpriteRenderer xk2;
    GameObject kK2;

    AudioSource AS;
    float dieanimt = 0;
    int dieanimc = 0;
    void Start()
    {
        krk = GameObject.FindGameObjectWithTag("sCharacter");
        img1.sprite = characters[0];
        img2.sprite = sprites[0];
        kK1 = GameObject.FindGameObjectWithTag("mCharacter");
        xk1 = kK1.GetComponent<SpriteRenderer>();
        kK2 = GameObject.FindGameObjectWithTag("sCharacter");
        xk2 = kK2.GetComponent<SpriteRenderer>();
        AS = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (krk.GetComponent<KarakterKontrol2>().enabled)
        {
            img1.sprite = characters[1];
        }
        if(sayac == 2)
            img2.sprite = sprites[1];
        else if(sayac == 1)
            img2.sprite = sprites[2];
        else if(sayac == 0)
        {
            
            dieanimt += Time.deltaTime;
            if(dieanimt > 0.1f)
            {
                if (img1.sprite == characters[1])
                {
                    xk2.sprite = dieanim[dieanimc++];
                    if (dieanimc == dieanim.Length)
                    {
                        dieanimc = 8;
                        kK1.GetComponent<KarakterKontrol>().enabled = false;
                        SceneManager.LoadScene("Final1");
                    }
                    dieanimt = 0;
                }
                if (img1.sprite == characters[0])
                {
                    xk1.sprite = dieanim[dieanimc++];
                    if (dieanimc == dieanim.Length)
                    {
                        dieanimc = 8;

                        SceneManager.LoadScene("Final1");
                    }
                    dieanimt = 0;
                }
            }
            
            img2.sprite = sprites[3];
        }
    //Öldün yazısı
    }
}
