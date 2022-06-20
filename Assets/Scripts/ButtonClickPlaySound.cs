using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickPlaySound : MonoBehaviour {

    public GameObject eff;
	void Start () {
        GetComponent<Button>().onClick.AddListener(() => playSoundBtn());
	}
	
	void playSoundBtn()
    {
        SoundControll.instance._playSound(0);
        if(eff != null)
        {
            GameObject e = Instantiate(eff, gameObject.transform.position, Quaternion.identity) as GameObject;
            Destroy(e, 1.5f);
        }else
            GamePlayController.instance.playEff(0, transform.position);
    }
}
