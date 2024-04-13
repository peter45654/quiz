using System.Collections.Generic;
using UnityEngine;

using Parabox.CSG;
using System.Linq;
using Unity.VisualScripting;

public class BooleanResultObject : BasicObject
{
    public List<BasicObject> boolean_obj_pool;
    public bool isNeed_update { get => _isNeed_update; }
    private bool _isNeed_update = false;
    private Mesh original_mesh;
    private bool is_triggerStay = false;
    private MeshFilter meshFilter;
    public GameObject unit_transform;
    Vector3[] new_vertices;

    // Start is called before the first frame update
    void Start()
    {

        meshRenderer = GetComponent<MeshRenderer>();
        boolean_obj_pool = new List<BasicObject>();
        meshFilter = GetComponent<MeshFilter>();
        original_mesh = meshFilter.mesh;
        SetUnselected();
        SetUnHover();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && is_triggerStay) UpdateMesh();
        if (_isNeed_update == false) return;
        UpdateMesh();
        _isNeed_update = false;
    }
    void UpdateMesh()
    {
        meshFilter.mesh = original_mesh;
        
        Model result = null;
        foreach (var item in boolean_obj_pool)
        {
            result = CSG.Subtract(gameObject, item.gameObject);
        }

        if (result == null) return;
        meshFilter.mesh = result.mesh;
        Vector3[] new_vertices = new Vector3[result.mesh.vertices.Length];
        for (int i = 0; i < result.mesh.vertices.Length; i++)
        {
            Vector3 inverse_transform_point = transform.InverseTransformPoint(result.mesh.vertices[i]);
            new_vertices[i] = inverse_transform_point;
        }
        meshFilter.mesh.vertices=new_vertices;
        
    }

    void ReverseTransform(Transform transform, ref Model model)
    {



    }

    public void SetIsNeedUpdate()
    {
        _isNeed_update = true;
    }
    public void OnTriggerStay(Collider other)
    {
        is_triggerStay = true;

    }
    void OnTriggerEnter(Collider other)
    {
        _isNeed_update = true;
        var basic_obj = other.gameObject.GetComponent<BasicObject>();
        if (basic_obj &&
            !boolean_obj_pool.Contains(basic_obj)
            ) boolean_obj_pool.Add(basic_obj);
    }
    void OnTriggerExit(Collider other)
    {
        _isNeed_update = true;
        var basic_obj = other.gameObject.GetComponent<BasicObject>();
        boolean_obj_pool.Remove(basic_obj);
        is_triggerStay = false;
    }

}
