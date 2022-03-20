using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class Timer : MonoBehaviour
{
    private Stopwatch watch;
    public TextMeshProUGUI timeText;
    public Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        watch = new Stopwatch();
        watch.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan ts = watch.Elapsed;
        
        timeText.text = "Time: " + ts.ToString();

        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.R))
        {
            restart();
        }
    }

    void restart()
    {
        Debug.Log("I should be strying to restart");
        player.deathAmount = 0;
        player.jumpAmount = 0;
        watch.Restart();
        Scene scene = SceneManager.GetActiveScene();
        //SceneManager.UnloadSceneAsync(scene.name);
        SceneManager.LoadScene(scene.name);
        
    }
    public void stopCounting()
    {
        watch.Stop();
    }
    public void resumeCounting()
    {
        watch.Start();
    }
}
