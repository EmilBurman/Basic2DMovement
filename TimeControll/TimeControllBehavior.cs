using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControllBehavior : MonoBehaviour {

    private IController2D controller;
    private ITimeControll timeControll;

    void Awake()
    {
        controller = GetComponent<IController2D>();
        timeControll = GetComponent<ITimeControll>();
    }

    void Update()
    {
        controller.SlowReverse();
        controller.FlashReverse();
    }

    void FixedUpdate()
    {
        timeControll.SlowReverse(controller.SlowReverse());
        timeControll.FlashReverse(controller.FlashReverse());
    }
}
