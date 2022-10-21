using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MazeManager : MonoBehaviour
{
    public MazeSpawner mazeSpawner;
    public Camera myCamera;
    public MazeGoal mazeGoal;
    public Material endPlatformMaterial;
    
    [Space(20)]
    public MazeDifficulty[] mazeDifficulties;
    public string currentDifficulty;


    MazeGoal goal;
    MazeDifficulty currentMaze;

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
                    currentMaze = mazeDifficulties[i];

                    mazeSpawner.Rows = currentMaze.rows;
                    mazeSpawner.Columns = currentMaze.columns;
                    myCamera.transform.position =currentMaze.cameraPosition;
                    myCamera.orthographicSize = currentMaze.cameraSize;

                    goal = Instantiate(mazeGoal.gameObject, mazeDifficulties[i].mazeGoalPosition, Quaternion.identity).GetComponent<MazeGoal>();
                    
                } else
                {
                    currentMaze = null;
                    goal = null;

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

    void Start()
    {
        RaycastHit hit;
                    
        if(Physics.Raycast(goal.transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            hit.transform.GetComponent<MeshRenderer>().material = endPlatformMaterial;
        }
    }
    
    void Update()
    {
        if(goal != null && currentMaze != null && goal.finished)
            currentMaze.onFinishEvent.Invoke();
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
    public UnityEvent onFinishEvent;
}