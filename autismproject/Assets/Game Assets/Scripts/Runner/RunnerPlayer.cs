using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class RunnerPlayer : MonoBehaviour
{
    public UnityEvent OnDeath;
    public float jumpStrength;
    [ReadOnly] public bool grounded;
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && grounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.transform.tag.Equals("Ground")) grounded = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.transform.tag.Equals("Ground")) grounded = false;
    }
}
