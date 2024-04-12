using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseBehavior : MonoBehaviour
{
    private Vector3 mouse_offset = Vector3.zero;
    private float mouse_z_coordinate = 0;
    private List<Transform> selection_pool;
    private List<Vector3> selection_mouse_offset_pool;
    private Transform onHover_obj;
    private RaycastHit raycastHit;
    private bool isMouse_hold = false;

    void Start()
    {
        selection_mouse_offset_pool = new List<Vector3>();
        selection_pool = new List<Transform>();
    }
    void FixedUpdate()
    {
        _ObjectDetect();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))//select obj
        {
            if (onHover_obj)
            {
                var onHover_basic_obj = onHover_obj.GetComponent<BasicObject>();
                onHover_basic_obj.SetSelected();
                if (!selection_pool.Contains(onHover_basic_obj.transform)) selection_pool.Add(onHover_basic_obj.transform);
            }
        }
        if (Input.GetMouseButtonDown(1))//unselect obj
        {
            if (onHover_obj)
            {
                var onHover_basic_obj = onHover_obj.GetComponent<BasicObject>();
                onHover_basic_obj.SetUnselected();
                selection_pool.Remove(onHover_basic_obj.transform);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            selection_mouse_offset_pool.Clear();
            var mouse_position = _GetMouseWorldPosition();
            foreach (Transform item in selection_pool)
            {
                var mouse_offset = item.position - mouse_position;
                item.transform.position = mouse_position + mouse_offset;
                selection_mouse_offset_pool.Add(mouse_offset);
            }
        }
        if (Input.GetMouseButton(0))
        {
            isMouse_hold = true;
            if (onHover_obj == null) return;
            var mouse_position = _GetMouseWorldPosition();
            for (int i = 0; i < selection_pool.Count; i++)
            {
                mouse_offset = selection_mouse_offset_pool[i];
                var item = selection_pool[i];
                item.transform.position = mouse_position + mouse_offset;
            }
        }

        if (Input.GetMouseButtonUp(0)) isMouse_hold = false;
    }
    Vector3 _GetMouseWorldPosition()
    {
        if (onHover_obj == null) return Vector3.zero;
        mouse_z_coordinate = Camera.main.WorldToScreenPoint(onHover_obj.transform.position).z;

        Vector3 mouse_point = Input.mousePosition;
        mouse_point.z = mouse_z_coordinate;
        return Camera.main.ScreenToWorldPoint(mouse_point);
    }
    void _ObjectDetect()
    {
        if (isMouse_hold) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Physics.Raycast(ray, out raycastHit);
            if (raycastHit.transform == null) // mouse ray hit nothing
            {
                if (onHover_obj)
                {
                    onHover_obj.GetComponent<BasicObject>().SetUnHover();
                    onHover_obj = null;
                }
                return;
            }

            if (Physics.Raycast(ray, out raycastHit)) // mouse ray hit something
            {
                if (onHover_obj != null)// previous onHover_obj won't be reset if rapid hover to next
                {
                    onHover_obj.GetComponent<BasicObject>().SetUnHover();
                }

                onHover_obj = raycastHit.transform;
                if (onHover_obj.GetComponent<BasicObject>().isSelected) return;
                onHover_obj.GetComponent<BasicObject>().SetHovered();
                return;
            }
        }

    }
}
