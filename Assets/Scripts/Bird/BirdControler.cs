using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControler : MonoBehaviour
{
    public static BirdControler instance;
    private float boundForce;
    private Rigidbody2D myBody;
    private Animator anim;
    //ngung di chuyen ong
    public float flag = 0;
    public int score = 0;
    private GameObject spawner;

    private bool sDidFlap;
    CameraShake cameraShake;

    void Awake()
    {
        _MakeInstance();

        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boundForce = 8;
        //tim den game obj
        spawner = GameObject.Find("Spawner_pipe");
    }

    //tao instance
    void _MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void FixedUpdate()
    {
        _BirdMove();
    }

    void _BirdMove()
    {
        if (GamePlayController.instance.birdLive)
        {
            if (sDidFlap)
            {
                sDidFlap = false;
                //cho chim nhay len
                myBody.velocity = new Vector2(myBody.velocity.x, boundForce);
            }
        }
        //con chim nho dau len
        if (myBody.velocity.y > 0)
        {
            float angle = Mathf.Lerp(0, 45, myBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else if (myBody.velocity.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if ((myBody.velocity.y < 0))
        {
            float angle = 0;
            angle = Mathf.Lerp(0, -45, -myBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        //sinh bong ghost
        //if (!checkInstanceGhost)
        //{
        //    Instantiate(playerGhost, transform.position, transform.rotation);
        //    StartCoroutine(delayInstanceGhost());
        //}
            
    }

    public void _FlapBtn()
    {
        sDidFlap = true;
        SoundControll.instance._playSound(1);
    }

    //di qua tinh diem
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PipeHolder" && GamePlayController.instance.birdLive)
        {
            score++;
            if (GamePlayController.instance != null)
            {
                GamePlayController.instance._SetScore(score);
            }
            
            SoundControll.instance._playSound(2);
        }
    }

    //cham nen dat, ong nuoc
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pipe" || collision.gameObject.tag == "targetTop" ||
            collision.gameObject.tag == "Ground")
        {
            flag = 1;
            if (GamePlayController.instance.birdLive)
            {
                GamePlayController.instance.birdLive = false;
                Debug.Log("Bird die =>" + gameObject.name);
                SoundControll.instance._playSound(3);
                
                anim.SetTrigger("die");
                //co de destroy pipe holder
                Destroy(spawner);
            }
            StartCoroutine(cameraShake.Shake(.1f,.2f));
            GamePlayController.instance._BirdDiedShowPanel(score);

            ContactPoint2D contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            GamePlayController.instance.playEff(1, pos);
        }
    }

    public bool _getBirdLive()
    {
        return GamePlayController.instance.birdLive;
    }

    //lay anh hien thi de cho hinh anh mo
    public Sprite getSpritePlayer()
    {
        Sprite sp = null;
        foreach(Transform t in transform)
        {
            if (t.gameObject.activeInHierarchy)
                return t.GetComponent<SpriteRenderer>().sprite;
        }

        return sp;
    }

    bool checkInstanceGhost = false;
    IEnumerator delayInstanceGhost()
    {
        checkInstanceGhost = true;
        yield return new WaitForSeconds(0.1f);
        checkInstanceGhost = false;
    }
}
