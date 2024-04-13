using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BooleanResultObject : BasicObject
{
    public bool isNeed_update { get => _isNeed_update; }
    private bool _isNeed_update = false;
    public List<BasicObject> boolean_obj_pool;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boolean_obj_pool = new List<BasicObject>();
        SetUnselected();
        SetUnHover();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {


    }
    public void SetIsNeedUpdate()
    {
        _isNeed_update = true;
    }
    void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter " + other.gameObject.name);
        _isNeed_update = true;
        var basic_obj = other.gameObject.GetComponent<BasicObject>();
        if (basic_obj &&
            !boolean_obj_pool.Contains(basic_obj)
            ) boolean_obj_pool.Add(basic_obj);
    }
    void OnTriggerExit(Collider other)
    {
        print("OnTriggerExit " + other.gameObject.name);
        _isNeed_update = true;
        var basic_obj = other.gameObject.GetComponent<BasicObject>();
        boolean_obj_pool.Remove(basic_obj);
    }
}
