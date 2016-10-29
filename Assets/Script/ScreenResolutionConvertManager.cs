using UnityEngine;
using System.Collections;

public class ScreenResolutionConvertManager : MonoBehaviour 
{
    float resizeScaleWidth, resizeScaleHeight;
	// Use this for initialization
	void Start () 
    {
        resizeScaleWidth = Screen.width / DefineManager.standardScreenWidth;
        resizeScaleHeight = Screen.height / DefineManager.standardScreenHeight;
	}

    public Vector2 BigToSmallConvert(Vector2 targetData)
    {
        return new Vector2((targetData.x * resizeScaleWidth) / DefineManager.standardScreenWidth, (targetData.y * resizeScaleHeight) / DefineManager.standardScreenHeight);
    }

    public Vector2 SmallToBigConvert(Vector2 targetData)
    {
        return new Vector2((targetData.x * resizeScaleWidth) * DefineManager.standardScreenWidth, (targetData.y * resizeScaleHeight) * DefineManager.standardScreenHeight);
    }

    public Vector2 BigResizeConvert(Vector2 targetData)
    {
        return new Vector2(targetData.x * resizeScaleWidth, targetData.y * resizeScaleHeight);
    }
}
