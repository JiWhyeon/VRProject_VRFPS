using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;

    AudioSource target;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<AudioSource>();
    }

    public void Call()
    {
        target.pitch = Random.Range(minPitch, maxPitch);
        target.Play();
    }
}
