﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tags
{
    public const string PLAYER = "Player";
    public const string ENEMY = "Enemy";
    public const string PICKUP = "Pick-up";
    public const string MAINCAMERA = "MainCamera";
    public const string PLATFORM = "Platform";
    public const string UNTAGGED = "Untagged";

    static string[] allTags = new string[] { PLAYER, ENEMY, PICKUP, MAINCAMERA, PLATFORM, UNTAGGED };
}
