using UnityEngine;
using System.Collections;

public class ScreenIOManager : MonoBehaviour 
{
    ScreenResolutionConvertManager screenResolutionConvertManager;
    int lastInputVarious;
    Vector2 touchPressedVector, touchReleasedVector;
	// Use this for initialization
    void Start()
    {
        touchPressedVector = new Vector2();
        touchReleasedVector = new Vector2();

        lastInputVarious = DefineManager.notTouched;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Input.GetMouseButtonDown(0))
        {
            //lastInputVarious = DefineManager.touchedSomewhere;
            touchPressedVector = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0))
        {
            touchReleasedVector = Input.mousePosition;
            lastInputVarious = RecognizeTouchEvent(touchPressedVector, touchReleasedVector);
        }
	}

    public void SetScreenResolutionConvertManager(ScreenResolutionConvertManager screenResolutionConvertManager)
    {
        this.screenResolutionConvertManager = screenResolutionConvertManager;
    }

    int RecognizeTouchEvent(Vector2 beforeTouchPosition, Vector2 afterTouchPosition)
    {
        float xMovementRange = beforeTouchPosition.x - afterTouchPosition.x,
            yMovementRange = beforeTouchPosition.y - afterTouchPosition.y;
        if(Mathf.Abs(xMovementRange) > Mathf.Abs(yMovementRange))//x moved
        {
            if (xMovementRange > DefineManager.touchMovementRangeLength)
                return DefineManager.gestureTouchedDirectionRightToLeft;
            else if (xMovementRange < DefineManager.touchMovementRangeLength)
                return DefineManager.gestureTouchedDirectionLeftToRight;
            else
                return DefineManager.touchedSomewhere;
        }
        else//y moved
        {
            if (yMovementRange > DefineManager.touchMovementRangeLength)
                return DefineManager.gestureTouchedDirectionTopToBottom;
            else if (yMovementRange < DefineManager.touchMovementRangeLength)
                return DefineManager.gestureTouchedDirectionBottomToTop;
            else
                return DefineManager.touchedSomewhere;
        }
    }

    public int GetMouseEvent()
    {
        int eventNum = lastInputVarious;
        lastInputVarious = DefineManager.notTouched;
        return eventNum;
    }

    public Vector2 GetMousePressedPosition()
    {
        Vector2 tempPosition = touchPressedVector;
        touchPressedVector = new Vector2();
        return screenResolutionConvertManager.BigResizeConvert(tempPosition);
    }

    public Vector2 GetMouseReleasedPosition()
    {
        Vector2 tempPosition = touchReleasedVector;
        touchReleasedVector = new Vector2();
        return screenResolutionConvertManager.BigResizeConvert(tempPosition);
    }
}
