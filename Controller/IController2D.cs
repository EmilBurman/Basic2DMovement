using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController2D
{
    float MoveHorizontal();
    float MoveVertical();
    bool Jump();
    bool Dash();
    bool Sprint();
    bool FlashReverse();
    bool SlowReverse();
}
