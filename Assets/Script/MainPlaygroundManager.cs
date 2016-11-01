using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainPlaygroundManager : MonoBehaviour 
{
    GraphicResourceManager graphicResourceManager;
    ScreenResolutionConvertManager screenResolutionConvertManager;
    FileIOManager fileIOManager;
    ScreenIOManager screenIOManager;
    ScreenEffectManager screenEffectManager;
    GUIStyle guiStyleManager;
    PlayMapManager playMapManager;
    GameAdsManager gameAdsManager;
    bool resetFlag;
    string readFile, readStoryFile, adsWatchedStatus;
    int touchEvent;

	// Use this for initialization
	void Start ()
    {
        graphicResourceManager = gameObject.AddComponent<GraphicResourceManager>();
        fileIOManager = gameObject.AddComponent<FileIOManager>();
        screenResolutionConvertManager = gameObject.AddComponent<ScreenResolutionConvertManager>();
        screenIOManager = gameObject.AddComponent<ScreenIOManager>();
        screenEffectManager = gameObject.AddComponent<ScreenEffectManager>();
        playMapManager = gameObject.AddComponent<PlayMapManager>();
        gameAdsManager = gameObject.AddComponent<GameAdsManager>();

        guiStyleManager = new GUIStyle();

        resetFlag = true;
        touchEvent = DefineManager.notTouched;
        adsWatchedStatus = "not watched";
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(resetFlag)
        {
            resetFlag = false;

            graphicResourceManager.SetFileIOManager(fileIOManager);
            graphicResourceManager.LoadSpriteResourceToPlayGame();
            
            fileIOManager.SaveTextFile("test.txt", "helloworld");
            readFile = fileIOManager.LoadTextFile("test.txt");

            readStoryFile = fileIOManager.LoadTextFileFromResource("story1");

            screenIOManager.SetScreenResolutionConvertManager(screenResolutionConvertManager);
        }

        touchEvent = screenIOManager.GetMouseEvent();
        if (touchEvent != DefineManager.notTouched)
        {
            Debug.Log("touch event: " + touchEvent);
            Debug.Log("touch before pos: " + screenIOManager.GetMousePressedPosition());
            Debug.Log("touch after pos: " + screenIOManager.GetMouseReleasedPosition());

            screenEffectManager.SetEffectTimeAndQuality(1.0F, 10.0F);
        }
	}

    void OnGUI()
    {
        //draw map
        DrawMapBasedOnPlayMapManager();

        //draw character
        float width = graphicResourceManager.bossSprite[0].width, height = graphicResourceManager.bossSprite[0].height;
        Vector2 drawPoint = screenResolutionConvertManager.SmallToBigConvert(new Vector2(0.1F, 0.1F));
        Vector2 drawScale = screenResolutionConvertManager.BigResizeConvert(new Vector2(width, height));
        //DrawPartOfImageEasier(graphicResourceManager.bossSprite[0], drawPoint.x, drawPoint.y, drawScale.x, drawScale.y, 1.0F, 1.0F, width - 1.0F, height - 1.0F);
        DrawPartOfImageWithAutoCalculate(graphicResourceManager.bossSprite[0], drawPoint.x, drawPoint.y, drawScale.x, drawScale.y, 4.0F, 4.0F, 5);

        //draw text
        guiStyleManager.fontSize = screenResolutionConvertManager.CalculateBasedOnScreenResolutionFontSize(50.0F);
        GUI.Label(new Rect(screenResolutionConvertManager.SmallToBigConvert(new Vector2(0.1F, 0.2F)), 
                            screenResolutionConvertManager.SmallToBigConvert(new Vector2(0.3F, 0.1F))), readFile, guiStyleManager);

        GUI.Label(new Rect(screenResolutionConvertManager.SmallToBigConvert(new Vector2(0.1F, 0.3F)),
                            screenResolutionConvertManager.SmallToBigConvert(new Vector2(0.3F, 0.1F))), readStoryFile, guiStyleManager);

        //ads 

        GUI.Label(new Rect(screenResolutionConvertManager.SmallToBigConvert(new Vector2(0.1F, 0.4F)),
                            screenResolutionConvertManager.SmallToBigConvert(new Vector2(0.3F, 0.1F))), adsWatchedStatus, guiStyleManager);

        if(GUI.Button(new Rect(screenResolutionConvertManager.SmallToBigConvert(new Vector2(0.1F, 0.5F)),
                            screenResolutionConvertManager.SmallToBigConvert(new Vector2(0.3F, 0.1F))), "Show Ads", guiStyleManager))
        {
            adsWatchedStatus = "not watched";
            gameAdsManager.ShowUnityAdsToClient();
        }

        if(gameAdsManager.IsClientWatchedAds())
        {
            adsWatchedStatus = "watched";
        }
    }

    void DrawMapBasedOnPlayMapManager()
    {
        int[,] mapData = playMapManager.GetNowPlayGroundEarthMap();
        int i, t;
        float drawStartPointX = (DefineManager.standardScreenWidth - DefineManager.mapScale * DefineManager.eachBlockScale) / 2,
                drawStartPointY = (DefineManager.standardScreenHeight - DefineManager.mapScale * DefineManager.eachBlockScale) / 2;
        for(i = 0; i < DefineManager.mapScale; i += 1)
        {
            for(t = 0; t < DefineManager.mapScale; t += 1)
            {
                Vector2 drawPoint = screenResolutionConvertManager.BigResizeConvert(new Vector2(drawStartPointX + DefineManager.eachBlockScale * t, 
                    drawStartPointY + DefineManager.eachBlockScale * i));
                Vector2 drawScale = screenResolutionConvertManager.BigResizeConvert(new Vector2(DefineManager.eachBlockScale, DefineManager.eachBlockScale));
                DrawPartOfImageWithAutoCalculate(graphicResourceManager.mapSprite[0], drawPoint.x, drawPoint.y, drawScale.x, drawScale.y, 8.0F, 13.0F, mapData[i, t]);
            }
        }
    }

    void DrawPartOfImageWithAutoCalculate(Texture2D image, float drawPositionX, float drawPositionY, float drawScaleWidth,
        float drawScaleHeight, float imageWidthSplitSize, float imageHeightSplitSize, int imageSplitNumber)
    {
        //imageSplitSize: 0 ~ n
        if (image == null)
            return;
        float imageSplitWidth = image.width / imageWidthSplitSize, imageSplitHeight = image.height / imageHeightSplitSize,
            foundTargetImageSplitX = 0.0F, foundTargetImageSplitY = 0.0F, imageWidth = image.width, imageHeight = image.height;

        foundTargetImageSplitX = imageSplitWidth * (imageSplitNumber % imageWidthSplitSize);
        foundTargetImageSplitY = imageSplitHeight * Mathf.Floor((imageSplitNumber / imageHeightSplitSize));

        DrawPartOfImageEasier(image, drawPositionX, drawPositionY, drawScaleWidth, drawScaleHeight, foundTargetImageSplitX,
            foundTargetImageSplitY, imageSplitWidth, imageSplitHeight - 1.0F);
    }

    void DrawPartOfImageEasier(Texture2D image, float pos_x, float pos_y, float scale_x, float scale_y, float x, float y, float width, float height)
    {
        float texture_width = image.width, texture_height = image.height;
        Graphics.DrawTexture(new Rect(pos_x, pos_y, scale_x, scale_y), image, new Rect(x / texture_width, (texture_height - height - y) / texture_height, width / texture_width,
            height / texture_height), 0, 0, 0, 0);
    }
}
