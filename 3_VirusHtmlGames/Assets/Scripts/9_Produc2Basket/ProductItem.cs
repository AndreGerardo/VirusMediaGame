using UnityEngine;

public class ProductItem : MonoBehaviour
{

    private bool isDragging = false;
    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePos);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(gameObject);
    }
}
