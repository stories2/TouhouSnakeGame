using UnityEngine;
using System.Collections;

public class FileIOManager : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}

    public void SaveTextFile(string fileName, string data)
    {
        if(RuntimePlatform.Android == Application.platform || RuntimePlatform.TizenPlayer == Application.platform ||
            RuntimePlatform.IPhonePlayer == Application.platform)
        {
            fileName = Application.persistentDataPath + "/" + fileName;
        }
        System.IO.File.WriteAllText(fileName, data);
    }

    public string LoadTextFile(string fileName)
    {
        if (RuntimePlatform.Android == Application.platform || RuntimePlatform.TizenPlayer == Application.platform ||
            RuntimePlatform.IPhonePlayer == Application.platform)
        {
            fileName = Application.persistentDataPath + "/" + fileName;
        }
        return System.IO.File.ReadAllText(fileName);
    }
	
    Texture2D GetTargetPathTexture2D(string path)
    {
        return (Texture2D)Resources.Load(path) as Texture2D;
    }

    public Texture2D[] GetTargetPathTexture2DArray(string path, int startPoint, int length)
    {
        if(length <= 0)
            return null;
        int i;
        Texture2D[] spriteResource = new Texture2D[length];
        for(i = 0; i < length; i += 1)
        {
            spriteResource[i] = GetTargetPathTexture2D(path + startPoint);
            startPoint = startPoint + 1;
        }
        return spriteResource;
    }
}
