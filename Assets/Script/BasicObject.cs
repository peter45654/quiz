using UnityEngine;
public enum PerformType { Substract, Union }
public class BasicObject : MonoBehaviour
{
    public PerformType performType;

    public Material onHover_material;
    public Material highlight_material;
    public Material substract_material;
    public Material union_material;
    public bool isSelected { get => _isSelected; }
    private bool _isSelected = false;
    private Material normal_material;
    public MeshRenderer meshRenderer { get => _meshRenderer; set => _meshRenderer = value; }
    private MeshRenderer _meshRenderer;
    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        if (performType.Equals(PerformType.Substract))
        {
            tag="Substract";
            normal_material = substract_material;
        }
        if (performType.Equals(PerformType.Union))
        {
            tag="Union";
            normal_material = union_material;
        }
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
        if (!isSelected) _meshRenderer.material = normal_material;
    }
    public void SetHovered()
    {
        if (!isSelected) _meshRenderer.material = onHover_material;
    }
    public void SetUnselected()
    {
        _isSelected = false;
        _meshRenderer.material = normal_material;
    }
}
