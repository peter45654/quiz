using System.Collections.Generic;
using UnityEngine;

using Parabox.CSG;

public class BooleanResultObject : BasicObject
{
    public List<BasicObject> boolean_obj_pool;
    public bool isNeed_update { get => _isNeed_update; }
    private bool _isNeed_update = false;
    private Mesh original_mesh;

    private MeshFilter meshFilter;
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
        if (_isNeed_update == false) return;
        UpdateMesh();
        _isNeed_update = false;
    }
    void UpdateMesh()
    {
        meshFilter.sharedMesh = original_mesh;
        Model result = null;
        foreach (var item in boolean_obj_pool)
        {
            var obj = item.gameObject;
            result = CSG.Subtract(gameObject, obj);
        }
        if (result != null) meshFilter.sharedMesh = result.mesh;
    }
    public void SetIsNeedUpdate()
    {
        _isNeed_update = true;
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
    }
}
