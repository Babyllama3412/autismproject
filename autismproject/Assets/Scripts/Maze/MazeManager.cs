using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public MazeSpawner mazeSpawner;
    public Camera myCamera;
    public MazeGoal mazeGoal;
    
    [Space(20)]
    public MazeDifficulty[] mazeDifficulties;
    public string currentDifficulty;

    void Awake()
    {
        if(mazeDifficulties.Length >= 1 && mazeSpawner != null && myCamera != null)
        {
            myCamera.orthographic = true;
            myCamera.transform.rotation = Quaternion.Euler(90,0,0);
            for (int i = 0; i < mazeDifficulties.Length; i++)
            {
                if(mazeDifficulties[i].difficulty == currentDifficulty)
                {
                    mazeSpawner.Rows = mazeDifficulties[i].rows;
                    mazeSpawner.Columns = mazeDifficulties[i].columns;
                    myCamera.transform.position = mazeDifficulties[i].cameraPosition;
                    myCamera.orthographicSize = mazeDifficulties[i].cameraSize;
                } else
                {
                    mazeSpawner.Rows = 5;
                    mazeSpawner.Columns = 5;
                    myCamera.transform.position = new Vector3(10,35,10);
                    myCamera.orthographicSize = 12.5f;
                }
            }
        } else 
        {
            Debug.LogWarning("To use the maze properly. Please add maze difficulties, a maze spawner, and a camera and add it to the Maze Manager script.");
        }
    }
}

[System.Serializable]
public class MazeDifficulty
{
    public string difficulty;
    public int rows, columns;

    [Space(20)]
    public Vector3 cameraPosition;
    public float cameraSize;

    [Space(20)]
    public Vector3 mazeGoalPosition;
}