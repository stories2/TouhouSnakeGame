using UnityEngine;
using System.Collections;

public class PlayMapManager : MonoBehaviour 
{

    int[,] playGroundEarthMap, playGroundCharacterMap;

	// Use this for initialization
	void Start () 
    {

        playGroundCharacterMap = new int[DefineManager.mapScale, DefineManager.mapScale];
        playGroundEarthMap = new int[, ]
        {
            {0, 3, 0, 0, 3, 0, 3, 0, 3, 3},
            {0, 0, 3, 3, 0, 3, 0, 3, 0, 0},
            {0, 0, 0, 3, 0, 0, 3, 0, 3, 3},
            {0, 0, 0, 0, 3, 0, 0, 3, 0, 0},
            {0, 0, 0, 0, 0, 3, 0, 0, 3, 0},
            {0, 0, 0, 0, 0, 0, 3, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 3, 3, 3},
            {0, 0, 0, 0, 0, 0, 0, 0, 3, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 3},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        };
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public int[, ] GetNowPlayGroundEarthMap()
    {
        return playGroundEarthMap;
    }

    public int[, ] GetNowPlayGroundCharacterMap()
    {
        return playGroundCharacterMap;
    }
}
