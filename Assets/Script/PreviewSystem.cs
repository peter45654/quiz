using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Parabox.CSG;
public class PreviewSystem : MonoBehaviour
{
    public List<GameObject> union_objects;
    public List<GameObject> substract_objects;
    public GameObject preview_object;
    public Material[] preview_materials;

    void Start()
    {
        union_objects = new List<GameObject>();
        substract_objects = new List<GameObject>();

        union_objects = GameObject.FindGameObjectsWithTag("Union").ToList();
        substract_objects = GameObject.FindGameObjectsWithTag("Substract").ToList();
    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0)) UpdateReult();
        if (Input.GetMouseButton(0)) UpdateReult();
    }
    void UpdateReult()
    {
        if (preview_object) Destroy(preview_object);
        Model result = CSG.Union(union_objects[0], union_objects[1]);
        preview_object = new GameObject();
        preview_object.AddComponent<MeshFilter>().sharedMesh = result.mesh;
        preview_object.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();

        result = CSG.Subtract(preview_object, substract_objects[0]);
        Destroy(preview_object);
        preview_object = new GameObject();
        preview_object.AddComponent<MeshFilter>().sharedMesh = result.mesh;
        preview_object.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();

        result = CSG.Subtract(preview_object, substract_objects[1]);
        Destroy(preview_object);
        preview_object = new GameObject();
        preview_object.AddComponent<MeshFilter>().sharedMesh = result.mesh;
        preview_object.AddComponent<MeshRenderer>().sharedMaterials = preview_materials;

    }
}
