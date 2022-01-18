using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    Queue<GameObject> verticalWalls = new Queue<GameObject>();
    Queue<GameObject> horizontalWalls = new Queue<GameObject>();
    private GameObject wall1;
    private GameObject wall2;
    private GameObject horizontalWall1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        verticalWallManage();
        horWallManage();
        
       
    }
    

    public void AddVerticalWalls(GameObject go)
    {
        if(wall1 == null)
        {
            wall1 = go;
        }
        else if(wall2 == null)
        {
            wall2 = go;
        }
        verticalWalls.Enqueue(go);
    }

    public void AddHorizontalWalls(GameObject go)
    {
        if(horizontalWall1 == null)
        {
            horizontalWall1 = go;
        }
        horizontalWalls.Enqueue(go);

    }

    public void verticalWallManage()
    {
        if (verticalWalls.Count > 2)
        {
            wall1 = verticalWalls.Dequeue();
            Destroy(wall1);
            wall1 = wall2;
            wall2 = null;


        }
        if (Input.GetKeyDown(KeyCode.Q))
        {

            wall1 = verticalWalls.Dequeue();
            Destroy(wall1);
            wall1 = wall2;
            wall2 = null;


        }
    }
    public void horWallManage()
    {
        if (horizontalWalls.Count > 1)
        {
            horizontalWall1 = horizontalWalls.Dequeue();
            Destroy(horizontalWall1);
            horizontalWall1 = null;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            horizontalWall1 = horizontalWalls.Dequeue();
            Destroy(horizontalWall1);
            horizontalWall1 = null;
        }
    }
}


