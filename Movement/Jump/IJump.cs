using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJump
{
    void Grounded(bool jump, bool sprint);
    void Airborne(bool jump);
    void RightWall(bool jump);
    void LeftWall(bool jump);
}
