using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPipe : MonoBehaviour {

    [SerializeField]
    private GameObject[] pipeHolderLevel;
    public float minY = -1, maxY = 2;
    int levelPipe;
    public IEnumerator Spawner()
    {
        yield return new WaitForSeconds(GamePlayController.instance.timeDelayInstanPipe);  //delayma
        if (GamePlayController.instance.birdLive)
        {
            levelPipe = PlayerPrefsControll.getLevelPipe();
            Vector3 temp = pipeHolderLevel[levelPipe].transform.position;
            Debug.Log("Spawner -> "+levelPipe+"=>" + name);
            temp.y = Random.Range(minY, maxY);
            //doi trong 1s
            GameObject objPipe = Instantiate(pipeHolderLevel[levelPipe], temp, Quaternion.identity) as GameObject;
            StartCoroutine(Spawner());
        }
    }
}
