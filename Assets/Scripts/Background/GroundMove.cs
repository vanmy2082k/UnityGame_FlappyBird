using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour {

    private Vector3 olPosition;
    private GameObject obj;

    public float speed;
    public float moveRange;

    public bool checkGround;
    void Start()
    {
        obj = gameObject;
        olPosition = gameObject.transform.position;
    }

    void Update()
    {
        if (GamePlayController.instance.birdLive)
        {
            if (checkGround)
            {
                transform.Translate(new Vector3(-1 * Time.deltaTime * GamePlayController.instance.speedPipe, 0, 0));
                if (Vector3.Distance(olPosition, obj.transform.position) > moveRange)
                {
                    obj.transform.position = olPosition;
                }
            }
            else
                 transform.Translate(new Vector3(-1 * Time.deltaTime * speed, 0, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pipe_destroy" && gameObject.tag == "bg0")
        {
            GamePlayController.instance.instanceGround(0);
            //Debug.Log("OnTriggerEnter2D " + collision.tag);
        }
        if (collision.tag == "Pipe_destroy" && gameObject.tag == "bg1")
        {
            GamePlayController.instance.instanceGround(1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Pipe_destroy" && gameObject.tag == "bg0")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Pipe_destroy" && gameObject.tag == "bg1")
        {
            Destroy(gameObject);
        }
    }
}
