using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController2D
{
    float MoveHorizontal();
    float MoveVertical();
    bool Jump();
    bool ContinuousJump();
    bool EndJump();
    bool Dash();
    bool Sprint();
    bool FlashReverse();
    bool SlowReverse();
}
