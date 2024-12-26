using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    public UnityEvent OnGrab;       //총을 잡을 때 이벤트
    public UnityEvent OnRelease;    //총을 놓을 때 이벤트

    public void Grab(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject;
        //interactor 타입이 XRDirectInteractor 이면
        if (interactor is XRDirectInteractor)
            OnGrab?.Invoke(); //OnGrab 이벤트가 있으면 실행
    }

    public void Release(SelectExitEventArgs args)
    {
        var interactor = args.interactorObject;
        //interactor 타입이 XRDirectInteractor 이면
        if(interactor is XRDirectInteractor)
            OnRelease?.Invoke(); //OnRelease 이벤트가 있으면 실행
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
