﻿using UnityEngine;
using System.Collections;

public class DefineManager : MonoBehaviour 
{
    public static string developTargetTouhouVersion = "th06", playingLanguage = "ko-kr";
    public static float standardScreenWidth = 750.0F, standardScreenHeight = 1334.0F, touchMovementRangeLength = 0.0F;
    public static int touchedSomewhere = 1, notTouched = -1, gestureTouchedDirectionLeftToRight = 2,
        gestureTouchedDirectionRightToLeft = 3, gestureTouchedDirectionBottomToTop = 4,
        gestureTouchedDirectionTopToBottom = 5;
}
