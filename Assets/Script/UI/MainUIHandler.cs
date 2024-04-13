using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainUIHandler : MonoBehaviour
{
    [Header("Camera")]
    public TMP_Dropdown camera_dropdown;
    public TMP_Text camera_fov_text;
    public Camera camera_angle;
    public Camera camera_top;
    [Header("CaseButton")]
    public GameObject case_attach_parent;
    public GameObject case1_prefab;
    public GameObject case2_prefab;
    public Button case1_btn;
    public Button case2_btn;

    [Header("Control Info")]
    public Button info_btn;
    public RectTransform info_panel;
    public Button info_panel_ok_btn;

    void Update()
    {
        camera_fov_text.text = Camera.main.fieldOfView.ToString("0.00");
    }
    public void OnCameraDropDownChange(int option)
    {
        var camera_to_switch = camera_top;
        if (option.Equals(1)) camera_to_switch = camera_angle;
        Camera.main.gameObject.SetActive(false);
        camera_to_switch.gameObject.SetActive(true);        
    }
    public void SetupCase1() { SetupCase(case1_prefab); }
    public void SetupCase2() { SetupCase(case2_prefab); }
    void SetupCase(GameObject prefab)
    {
        foreach (Transform item in case_attach_parent.transform) Destroy(item.gameObject);
        var obj = Instantiate(prefab);
        obj.transform.parent = case_attach_parent.transform;
        PreviewSystem.Instance.SetReset();
    }
}
