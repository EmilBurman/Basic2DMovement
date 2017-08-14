using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerController2D : MonoBehaviour, IController2D
{
    //Interface----------------------------
    public bool Dash()
    {
        return false;
    }

    public bool FlashReverse()
    {
        return false;
    }

    public bool Jump()
    {
        return false;
    }

    public float Move()
    {
        return direction;
    }

    public bool SlowReverse()
    {
        return false;
    }

    public bool Sprint()
    {
        return false;
    }
    //End interface-------------------------

    private ITerrainState stateMachine;
    public CrawlerState crawlState;                     // Shows the current state of dashing.
    float direction;
    // Use this for initialization
    void Start()
    {
        stateMachine = GetComponent<ITerrainState>();
    }
    private void Update()
    {
        CrawlStateCheck();
    }

    private void CrawlStateCheck()
    {
        switch (crawlState)
        {
            case CrawlerState.CrawlRight:
                if (!stateMachine.WallRight() && stateMachine.Grounded())
                {
                    direction = 1;
                }
                else
                    crawlState = CrawlerState.CrawlLeft;
                break;
            case CrawlerState.CrawlLeft:
                if (!stateMachine.WallLeft() && stateMachine.Grounded())
                {
                    direction = -1;
                }
                else
                    crawlState = CrawlerState.CrawlRight;
                break;
        }
    }
}

public enum CrawlerState
{
    CrawlRight,
    CrawlLeft,
    Stop
}
