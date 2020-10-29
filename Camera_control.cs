using UnityEngine;

public class Camera_control : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;
    public float minZ = -1;
    public float maxZ = -40;

    public bool drag = true;
    public bool keyboard_and_mouse = false;
 
    private Vector3 mouseOrigin;
    private bool isPanning;


    public float scrollSpeed = 30f;

    Vector3 lastDragPosition;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (keyboard_and_mouse) {
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                pos.y += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
            {
                pos.x -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            {
                pos.y -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                pos.x += panSpeed * Time.deltaTime;
            }
            

            pos.x = Mathf.Clamp(pos.x, 0, panLimit.x);
            pos.y = Mathf.Clamp(pos.y, 0, panLimit.y);
            
        

            
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.z += scroll * scrollSpeed * Time.deltaTime;        
        pos.z = Mathf.Clamp(pos.z, maxZ, minZ);

        transform.position = pos;

        if (drag) {
            UpdateDrag();
        }
        
    }
    void UpdateDrag()
    {
        if (Input.GetMouseButtonDown(0))
            lastDragPosition = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            var delta = lastDragPosition - Input.mousePosition;
            transform.Translate(delta * Time.deltaTime * 1f);
            lastDragPosition = Input.mousePosition;
        }

    }
}
