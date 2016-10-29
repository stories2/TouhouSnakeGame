using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainPlaygroundManager : MonoBehaviour 
{
    GraphicResourceManager graphicResourceManager;
    ScreenResolutionConvertManager screenResolutionConvertManager;
    FileIOManager fileIOManager;
    bool resetFlag;

	// Use this for initialization
	void Start ()
    {
        graphicResourceManager = gameObject.AddComponent<GraphicResourceManager>();
        fileIOManager = gameObject.AddComponent<FileIOManager>();
        screenResolutionConvertManager = gameObject.AddComponent<ScreenResolutionConvertManager>();

        resetFlag = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(resetFlag)
        {
            resetFlag = false;

            graphicResourceManager.SetFileIOManager(fileIOManager);
            graphicResourceManager.LoadSpriteResourceToPlayGame();
        }
	}

    void OnGUI()
    {
        float width = graphicResourceManager.bossSprite[0].width, height = graphicResourceManager.bossSprite[0].height;
        Vector2 drawPoint = screenResolutionConvertManager.SmallToBigConvert(new Vector2(0.1F, 0.1F));
        Vector2 drawScale = screenResolutionConvertManager.BigResizeConvert(new Vector2(width, height));
        DrawPartOfImage(graphicResourceManager.bossSprite[0], drawPoint.x, drawPoint.y, drawScale.x, drawScale.y, 1.0F, 1.0F, width - 1.0F, height - 1.0F);

    }

    void DrawPartOfImage(Texture2D image, float pos_x, float pos_y, float scale_x, float scale_y, float x, float y, float width, float height)
    {
        float texture_width = image.width, texture_height = image.height;
        Graphics.DrawTexture(new Rect(pos_x, pos_y, scale_x, scale_y), image, new Rect(x / texture_width, (texture_height - height - y) / texture_height, width / texture_width,
            height / texture_height), 0, 0, 0, 0);
    }
}
