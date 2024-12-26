using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//총알이 감소할수록 총의 밝기가 어두워짐

public class ChangeEmissionIntensity : MonoBehaviour
{
    public float min = 0f; //최소값
    public float max = 3f; //최대값

    private Renderer target; // 렌더링 컴포넌트 

    private void Awake()
    {
        target = GetComponent<Renderer>();
    }

    public void Call(float ratio)
    {
        //ratio에 따라 명도지정
        var intensity = Mathf.Lerp(min, max, ratio);
        //총 내부의 명도 적용
        target.material.SetColor(
            "_EmissionColor", target.material.color * intensity
            );
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
