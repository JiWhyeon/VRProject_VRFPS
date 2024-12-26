using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVFXColor : MonoBehaviour
{
    public float arrangeRange = 0.5f;
    ParticleSystem target;

    private void Awake()
    {
        target = GetComponent<ParticleSystem>();
    }

    public void Call(Color color)
    {
        var main = target.main;
        main.startColor = new ParticleSystem.MinMaxGradient(
            color, color * Random.Range(1 - arrangeRange, 1 + arrangeRange) );
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
