using UnityEngine;

public class DeleteController : MonoBehaviour
{
    private GridManager grid;
    private SaveService saveService;
    public GameObject deleteOverlay;
    private bool isActive = false;

    private void Awake()
    {
        if (deleteOverlay != null)
            deleteOverlay.SetActive(false);
    }

    private void Start()
    {
        grid = FindObjectOfType<GridManager>();
        saveService = FindObjectOfType<SaveService>();
    }

    private void Update()
    {
        if(!isActive) return;

        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var cell = grid.WorldToCell(mouse);

        if (Input.GetMouseButtonDown(0))
        {
            // �������� ���� �� ������ � ���� ������
            var found = saveService.FindBuildingAt(cell.x, cell.y);
            if (found != null)
            {
                // ������� �����
                grid.SetOccupied(found.x, found.y, found.w, found.h, false);

                // �������� ������ � ����� (�� �����/�����������)
                var go = GameObject.Find(found.instanceName);
                if (go != null) Destroy(go);

                // �������� �� ����������
                saveService.RemoveBuildingByName(found.instanceName);
            }
            else
            {
                Debug.Log("�� ������ ������ � ���� ������");
            }
        }

        // ������ ���� � ����� �� ������ ��������
        if (Input.GetMouseButtonDown(1))
            Deactivate();
    }

    /// <summary>
    /// �����, ������������ ����� ��������
    /// </summary>
    public void Activate()
    {
        isActive = true;
        deleteOverlay.SetActive(true);
    }

    /// <summary>
    /// �����, �������������� ����� ��������
    /// </summary>
    public void Deactivate()
    {
        isActive = false;
        deleteOverlay.SetActive(false);
    }
}