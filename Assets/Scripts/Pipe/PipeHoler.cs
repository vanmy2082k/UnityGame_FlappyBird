using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeHoler : MonoBehaviour {


    public static PipeHoler pipeHolerInstance;
    void Start () {
        _MakeInstance();
    }
	
    void _MakeInstance()
    {
        if(pipeHolerInstance == null)
        {
            pipeHolerInstance = this;
        }
    }
	void Update () {
        if(GamePlayController.instance.birdLive)
            _PipeMove();
        //goi instance de lay bien
        if(BirdControler.instance != null)
        {
            if(BirdControler.instance.flag == 1)
            {
                Destroy(GetComponent<PipeHoler>());
            }
        }
	}

    void _PipeMove()
    {
        Vector3 temp = transform.position;
        temp.x -= GamePlayController.instance.speedPipe * Time.deltaTime;
        transform.position = temp;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pipe_destroy")
        {
            Destroy(gameObject);
        }
    }
}
