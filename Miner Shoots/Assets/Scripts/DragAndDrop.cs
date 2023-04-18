using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector2 offset;
    private bool isDragging = false;

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos + offset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
}
