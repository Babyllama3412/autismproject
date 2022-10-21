using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePlayer : MonoBehaviour
{
    public float speed;
    public Vector2 mousePositions;

    void Update()
    {
        mousePositions = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        GetComponent<Rigidbody>().AddForce(new Vector3(mousePositions.x-(Screen.width/2),0,mousePositions.y-(Screen.height/2)) * 
        Time.deltaTime * speed);
    }
}
