using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject buildingsPanel;
    public PlacementController placementController;
    public BuildingConfig[] buildingConfigs;
    public DeleteController deleteController;

    private bool deleteMode = false;

    void Start()
    {
        buildingsPanel.SetActive(false);
        placementController = FindObjectOfType<PlacementController>();
    }

    /// <summary>
    /// �����, ������������ ��� ������� �� ������ ���������� ������
    /// </summary>
    public void OnPlaceButtonPressed()
    {
        if (deleteMode)
        {
            deleteMode = false;
            if (deleteController != null)
                deleteController.Deactivate();
        }

        // ����������� ��������� ������ ������ ������
        if (buildingsPanel != null)
            buildingsPanel.SetActive(!buildingsPanel.activeSelf);
    }

    /// <summary>
    /// �����, ������������ ��� ������ ������
    /// </summary>
    /// <param name="index"></param>
    public void SelectBuilding(int index)
    {
        if (index < 0 || index >= buildingConfigs.Length) return;
        placementController.StartPlacement(buildingConfigs[index]);
        buildingsPanel.SetActive(false);
    }

    /// <summary>
    /// �����, ������������ ��� ������� �� ������ ��������
    /// </summary>
    public void OnDeleteButtonPressed()
    {
        if (deleteController == null)
        {
            Debug.LogWarning("DeleteController �� �������� � UIController!");
            return;
        }

        // ����������� ����� ��������
        deleteMode = !deleteMode;

        if (deleteMode)
        {
            deleteController.Activate();
            if (buildingsPanel != null)
                buildingsPanel.SetActive(false);
            Debug.Log("����� �������� �������");
        }
        else
        {
            deleteController.Deactivate();
            Debug.Log("����� �������� ��������");
        }
    }

    public void OnLoadBuildingsButtonPressed()
    {
        var saveService = FindObjectOfType<SaveService>();
        if (saveService != null)
        {
            saveService.Load();
            Debug.Log("������ ��������� ������� ����� ������");
        }
    }
}
