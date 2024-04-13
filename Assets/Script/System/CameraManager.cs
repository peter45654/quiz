using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<Camera> cameras;
    public float max_zoom_in=5.0f;
    public float max_zoom_out=90f;
    public float zoom_step_scale=0.5f;
    public bool is_mousewheel_hold = false;
    // Start is called before the first frame update
    private Vector3 mouse_offset = Vector3.zero;
    private float mouse_z_coordinate = 0;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mouseScrollDelta.y!=0){
            Camera.main.fieldOfView-=Input.mouseScrollDelta.y*zoom_step_scale;
            if (Camera.main.fieldOfView>max_zoom_out)Camera.main.fieldOfView=max_zoom_out;
            if (Camera.main.fieldOfView<max_zoom_in)Camera.main.fieldOfView=max_zoom_in;
        }
        if (Input.GetMouseButtonDown(2))
        if (Input.GetMouseButton(2))
        {
            is_mousewheel_hold = true;
        }
        if (Input.GetMouseButtonUp(2))
        {
            is_mousewheel_hold = false;
        }
    }

       Vector3 _GetMouseWorldPosition()
    {
        
        mouse_z_coordinate = Camera.main.transform.position.z;

        Vector3 mouse_point = Input.mousePosition;
        mouse_point.z = mouse_z_coordinate;
        return Camera.main.ScreenToWorldPoint(mouse_point);
    }
}
