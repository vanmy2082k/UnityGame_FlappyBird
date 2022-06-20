using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    float time = 0.5f;
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //transform.position = BirdControler.instance.transform.position;
        transform.localScale = BirdControler.instance.transform.localScale;
	}
	
	void Update () {
        spriteRenderer.sprite = BirdControler.instance.getSpritePlayer();
        spriteRenderer.color = new Vector4(50, 50, 50, 0.2f);
        time -= Time.deltaTime;
        if (time <= 0)
            Destroy(gameObject);
	}
}
