using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLinePosition : MonoBehaviour
{
    public int index;
    private LineRenderer target;

    private void Awake()
    {
        target = GetComponent<LineRenderer>();
    }

    public void Call(Vector3 worldPosition)
    {
        if(target.useWorldSpace) //월드 좌표계라면
        {
            //target 번호 & 위치 지정
            target.SetPosition(index, worldPosition);
        }
        else
        {
            //로컬 위치 계산
            var localPosition = transform.InverseTransformPoint(worldPosition);
            //target 번호 & 위치 지정
            target.SetPosition(index, localPosition);
        }
    }

}
