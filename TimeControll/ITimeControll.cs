using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeControll
{
    float interpolation { get; set; }
    void SlowReverse(bool reversing);
    void FlashReverse(bool flashReverse);
    ArrayList GetPositionArray();
}
