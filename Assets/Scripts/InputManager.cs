using UnityEngine;
public class InputManager : MonoBehaviour
{
    public GameObject _exampleGraph;
    private float speed = 0.1f;

    [System.Obsolete]
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnMouseDrag();
        }
        else
        {
            return;
        }
    }

    [System.Obsolete]
    void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * speed; 
        _exampleGraph.transform.RotateAround(Vector3.up, -rotX);
    }
}
