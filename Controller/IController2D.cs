using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController2D
{
    float Move();
    bool Jump();
    bool Dash();
    bool Sprint();
    bool FlashReverse();
    bool SlowReverse();
    bool DisableInput();
}
