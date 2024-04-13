using UnityEngine;

public class BasicObject : MonoBehaviour
{
    public Material onHover_material;
    public Material highlight_material;
    public Material original_material;
    public bool isSelected { get => _isSelected; }
    private bool _isSelected = false;
    public MeshRenderer meshRenderer{get=>_meshRenderer;set =>_meshRenderer=value;}
    private MeshRenderer _meshRenderer;
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        SetUnselected();
        SetUnHover();
    }

    public void SetSelected()
    {
        _isSelected = true;
        _meshRenderer.material = highlight_material;
    }
    public void SetUnHover()
    {
        if (isSelected) _meshRenderer.material = highlight_material;
        if (!isSelected) _meshRenderer.material = original_material;
    }
    public void SetHovered()
    {
        if (!isSelected) _meshRenderer.material = onHover_material;
    }
    public void SetUnselected()
    {
        _isSelected = false;
        _meshRenderer.material = original_material;
    }
}
