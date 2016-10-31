using UnityEngine;
using System.Collections;

public class ScreenEffectManager : MonoBehaviour {

    GlitchEffect glitchEffectManager;
    float effectTime, effectRange;
	// Use this for initialization
	void Start () {

        glitchEffectManager = gameObject.GetComponent<GlitchEffect>();

        effectTime = 0.0F;
        effectRange = 0.0F;
	}
	
	// Update is called once per frame
	void Update () {
	
        if(effectTime > 0)
        {
            effectTime = effectTime - Time.deltaTime;
            effectRange = effectRange - effectRange * Time.deltaTime;
        }
        else
        {
            effectTime = 0.0F;
            effectRange = 0.0F;
        }
        glitchEffectManager.intensity = effectRange;
	}

    public void SetEffectTimeAndQuality(float effectTime, float effectRange)
    {
        this.effectTime = effectTime;
        this.effectRange = effectRange;
    }
}
