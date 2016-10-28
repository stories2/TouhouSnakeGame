using UnityEngine;
using System.Collections;

public class FileIOManager : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
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
