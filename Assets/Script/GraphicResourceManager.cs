using UnityEngine;
using System.Collections;

public class GraphicResourceManager : MonoBehaviour
{
    FileIOManager fileIOManager;

    public Texture2D[] itemSprite, mapSprite, bossSprite, enemySprite, playerSprite;
    public Texture2D[,] storySprite;
    public string[] th06StoryMemberData;

	// Use this for initialization
	void Start () 
    {
        th06StoryMemberData = new string[]
        {
            "reimu", "marisa", "lumia", "daiyousei", "cirno", "meirin", "koakuma", "patchouli", 
            "sakuya", "remiria", "flandore"
        };
	}

    public void SetFileIOManager(FileIOManager fileIOManager)
    {
        this.fileIOManager = fileIOManager;
    }

    void LoadSpriteAndSaveToArray(Texture2D[] targetArray, string path, int startPoint, int length)
    {
        targetArray = fileIOManager.GetTargetPathTexture2DArray(path, startPoint, length);
    }

    Texture2D[] LoadSpriteAndSaveToArray(string path, int startPoint, int length)
    {
        return fileIOManager.GetTargetPathTexture2DArray(path, startPoint, length);
    }

    public void LoadSpriteResourceToPlayGame()
    {
        itemSprite = LoadSpriteAndSaveToArray("item/", 1, 5);
        mapSprite = LoadSpriteAndSaveToArray("map/", 1, 4);
        bossSprite = LoadSpriteAndSaveToArray(DefineManager.developTargetTouhouVersion + "/boss/", 1, 9);
        enemySprite = LoadSpriteAndSaveToArray(DefineManager.developTargetTouhouVersion + "/enemy/", 1, 10);
        playerSprite = LoadSpriteAndSaveToArray(DefineManager.developTargetTouhouVersion + "/player/", 1, 4);

        storySprite = new Texture2D[th06StoryMemberData.Length, 10];
        int i, t;
        for(i = 0; i < th06StoryMemberData.Length; i += 1)
        {
            Texture2D[] temp;
            temp = LoadSpriteAndSaveToArray("story/" + th06StoryMemberData[i] + "/", 1, 10);
            for(t = 0; t < temp.Length; t += 1)
            {
                storySprite[i, t] = temp[t];
            }
        }
    }
}
