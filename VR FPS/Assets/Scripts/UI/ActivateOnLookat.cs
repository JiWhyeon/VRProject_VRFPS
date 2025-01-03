using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnLookat : MonoBehaviour
{
    public new Camera camera;
    public Behaviour target;

    public float thresholdAngle = 30f;
    public float thresholdDuration = 2f;

    private bool isLooking = false;
    private float showingTime;

    void Awake()
    {
        target.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var dir = target.transform.position - camera.transform.position;
        var angle = Vector3.Angle(camera.transform.forward, dir);

        if(angle < thresholdAngle)
        {
            if (isLooking)
            {
                if (!target.enabled && Time.time > showingTime)
                    target.enabled = true;
            }
            else
            {
                isLooking = true;
                showingTime = Time.time + thresholdDuration;
            }
        }
        else if(isLooking)
        {
            isLooking = false;
            target.enabled = false;
        }
    }
}
