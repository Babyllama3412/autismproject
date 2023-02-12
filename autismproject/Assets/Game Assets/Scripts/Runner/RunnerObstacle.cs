using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerObstacle : MonoBehaviour
{
    public float speed;

    bool canGiveScore = true;

    void Update()
    {
        if(transform.position.x <= -12.5)
            Destroy(gameObject);

        transform.Translate(Vector3.left *Time.deltaTime * speed);

        if(transform.position.x <= -3f && canGiveScore)
        {
            RunnerManager.Instance.score++;
            canGiveScore = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "Player")
        {
            other.transform.GetComponent<RunnerPlayer>().OnDeath.Invoke();
            Time.timeScale = 0;
        }
    }
}
