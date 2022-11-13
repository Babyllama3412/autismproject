using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public class SortDragger : MonoBehaviour
{
    [ReadOnly] public string categoryName;
    public bool inStorage;
    bool dragging;

    void Update()
    {
        if(dragging)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }
    }

    void OnMouseDrag()
    {
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
    }
}
