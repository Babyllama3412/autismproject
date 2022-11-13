using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    public Transform startingGround;
    public float speed;
    public Vector2 spawnTimeRange;
    public ObstacleProperties[] obstacles;

    [HorizontalLine(height: 2, color: EColor.Black)]
    [ReadOnly] public float score;
    public TMP_Text scoreText;

    [ReadOnly] public List<Transform> grounds = new List<Transform>();

    public static RunnerManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        grounds.Add(startingGround);
        StartCoroutine(StartGeneration());
        StartCoroutine(StartObstacleSpawn());
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
        for (int i = 0; i < grounds.Count; i++)
        {
            if(grounds[i] != null)
            {
                if(grounds[i].position.x <= -24)
                    Destroy(grounds[i].gameObject);
                grounds[i].Translate(Vector3.left *Time.deltaTime * speed);
            }
            

            if(grounds[i] == null)
                grounds.RemoveAt(i);

        }
    }

    IEnumerator StartGeneration()
    {
        yield return null;
        
        if(startingGround.position.x <= 0)
        {
            Transform frontLastBlock = startingGround.GetChild(startingGround.childCount - 1);
            Vector3 frontLastBlockPos = new Vector3(frontLastBlock.position.x, 
                                                    frontLastBlock.position.y, 
                                                    frontLastBlock.position.z);

            Transform newGround = Instantiate(startingGround, 
            new Vector3(frontLastBlockPos.x + 12.5f, frontLastBlockPos.y, 0), 
            Quaternion.identity);

            startingGround = newGround;
            grounds.Add(startingGround);
        }
        StartCoroutine(StartGeneration());
    }

    IEnumerator StartObstacleSpawn()
    {
        float rand = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
        int randObstacle = Random.Range(0, obstacles.Length);
        yield return new WaitForSeconds(rand);
        Instantiate(obstacles[randObstacle].obstacle.gameObject,
                    new Vector3(12.5f, obstacles[randObstacle].startingY, 0),
                    Quaternion.identity);

        StartCoroutine(StartObstacleSpawn());
    }
    
}

[System.Serializable]
public class ObstacleProperties
{
    public float startingY;
    public RunnerObstacle obstacle;
}
