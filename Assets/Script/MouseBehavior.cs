using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseBehavior : MonoBehaviour
{
    public Material onHover_material;
    public Material highlight_material;
    public Material original_material;

    private Transform onHover_obj;
    private Transform selection_obj;
    private RaycastHit raycastHit;

    void FixedUpdate()
    {
        _ObjectDetect();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //select obj
        {
            if (onHover_obj)
            {
                onHover_obj.GetComponent<BasicObject>().SetSelected();
            }
        }
        if (Input.GetMouseButtonDown(1)) //unselect obj
        {
            if (onHover_obj)
            {
                var basic_obj = onHover_obj.GetComponent<BasicObject>();
                if (basic_obj.isSelected) basic_obj.SetUnselected();
            }
        }
    }
    void _ObjectDetect()
    {
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
                if (onHover_material) onHover_obj.GetComponent<BasicObject>().SetHovered();
                return;
            }
        }

    }
}
