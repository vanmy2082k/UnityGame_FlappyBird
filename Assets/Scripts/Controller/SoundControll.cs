using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControll : MonoBehaviour
{

    public static SoundControll instance;
    public AudioSource[] SoundAudio;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    public void _playSound(int index)
    {
        if (!SoundAudio[0].isPlaying)
            SoundAudio[index].Play();
        if (index != 0) SoundAudio[index].Play();
    }

    public void _stopSound(int index)
    {
        SoundAudio[(int)index].Stop();
    }

    public void _playSoundBird()
    {
        StartCoroutine(delaySoundBird());
    }
    public void _stopSoundBird()
    {
        StopAllCoroutines();
    }
    IEnumerator delaySoundBird()
    {
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        int rd = Random.Range(4, 9);
        _playSound(rd);
        StartCoroutine(delaySoundBird());
    }

    public void onSound(bool check)
    {
        for (int i = 0; i < SoundAudio.Length; i++)
        {
            SoundAudio[i].mute = !check;
        }
    }
}
