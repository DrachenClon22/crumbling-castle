using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static bool _active = true;
    private static SoundManager instance;
    public Transform obj;
    [HideInInspector]
    public List<AudioSource> sources = new List<AudioSource>();
    public int maxClipsToPlay = 10;

    private void Start()
    {
        instance = this;
    }
    public static AudioSource PlayAudio(AudioClip clip)
    {
        if (_active)
        {
            var src = instance.obj.AddComponent<AudioSource>();
            src.clip = clip;
            src.Play();
            return src;
        } else
        {
            return null;
        }
    }

    public static void SetActived(bool state)
    {
        _active = state;
        if (_active)
        {
            foreach(var item in instance.sources)
            {
                item.Stop();
                Destroy(item);
            }
        }
    }

    private void Update()
    {
        foreach (var item in sources)
        {
            if (!item.isPlaying)
            {
                Destroy(item);
            }
        }
    }
}
