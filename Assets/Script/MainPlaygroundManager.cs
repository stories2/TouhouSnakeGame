using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainPlaygroundManager : MonoBehaviour 
{
    GraphicResourceManager graphicResourceManager;
    FileIOManager fileIOManager;
    bool resetFlag;

	// Use this for initialization
	void Start ()
    {
        graphicResourceManager = gameObject.AddComponent<GraphicResourceManager>();
        fileIOManager = gameObject.AddComponent<FileIOManager>();

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
        float width = graphicResourceManager.storySprite[0, 0].width, height = graphicResourceManager.storySprite[0, 0].height;

        DrawPartOfImage(graphicResourceManager.storySprite[0, 0], 0.0F, 0.0F, width, height, 1.0F, 1.0F, width - 1.0F, height - 1.0F);

    }

    void DrawPartOfImage(Texture2D image, float pos_x, float pos_y, float scale_x, float scale_y, float x, float y, float width, float height)
    {
        float texture_width = image.width, texture_height = image.height;
        Graphics.DrawTexture(new Rect(pos_x, pos_y, scale_x, scale_y), image, new Rect(x / texture_width, (texture_height - height - y) / texture_height, width / texture_width,
            height / texture_height), 0, 0, 0, 0);
    }
}
