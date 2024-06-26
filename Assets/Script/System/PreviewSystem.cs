using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Parabox.CSG;
using Unity.VisualScripting;
public class PreviewSystem : MonoBehaviour
{
    public List<GameObject> union_objects;
    public List<GameObject> substract_objects;
    public GameObject preview_object;
    public Material preview_material;
    static public PreviewSystem Instance { get => _previewSystem; }
    private static PreviewSystem _previewSystem;
    private bool isNeed_Reset=false;
    void Start()
    {
        if (_previewSystem == null) _previewSystem = this;
        Setup();
    }
    void LateUpdate(){
        if(isNeed_Reset==false)return;
        Reset();
        isNeed_Reset=false;
    }
    public void UpdateResult()
    {

        if (preview_object) Destroy(preview_object);
        if (union_objects.Count.Equals(0))
        {
            Setup();
            return;
        }
        Model result = CSG.Union(union_objects[0], union_objects[1]);
        preview_object = new GameObject();
        preview_object.transform.parent = union_objects[0].transform.parent;
        preview_object.AddComponent<MeshFilter>().sharedMesh = result.mesh;
        preview_object.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();


        result = CSG.Subtract(preview_object, substract_objects[0]);
        Destroy(preview_object);
        preview_object = new GameObject();
        preview_object.transform.parent = union_objects[0].transform.parent;
        preview_object.AddComponent<MeshFilter>().sharedMesh = result.mesh;
        preview_object.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();


        result = CSG.Subtract(preview_object, substract_objects[1]);
        Destroy(preview_object);
        preview_object = new GameObject();
        preview_object.transform.parent = union_objects[0].transform.parent;
        preview_object.AddComponent<MeshFilter>().sharedMesh = result.mesh;
        preview_object.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
        preview_object.GetComponent<MeshRenderer>().material = preview_material;

    }
    public void SetReset(){
        isNeed_Reset=true;
    }
    public void Reset()
    {
        union_objects.Clear();
        substract_objects.Clear();
    }
    public void Setup()
    {
        union_objects = new List<GameObject>();
        substract_objects = new List<GameObject>();

        union_objects = GameObject.FindGameObjectsWithTag("Union").ToList();
        substract_objects = GameObject.FindGameObjectsWithTag("Substract").ToList();
    }
}
