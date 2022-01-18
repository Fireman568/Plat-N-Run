using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataHolder : MonoBehaviour
{
    public static GlobalDataHolder Instance;

    public int currentLevelCollectibleAmount;
    public int currentLevelDeaths;
    public int currentLevelTime;
    public int overallLevelCollectibleAmount;
    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
