using UnityEngine;

public class SpinArrow : MonoBehaviour
{
    [SerializeField] private SpinTheWheelScript SS;
    void OnTriggerEnter2D(Collider2D coll)
    {
        SS.selectedItem = int.Parse(coll.name);
    }

}
