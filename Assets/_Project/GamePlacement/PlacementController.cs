using System;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    public GridManager grid;
    public SaveService save;
    public Transform buildingRoot;
    public Camera targetCamera;

    private BuildingConfig activeConfig;
    private GameObject preview;
    private SpriteRenderer previewRenderer;
    private Vector2Int currentCell;
    private bool isPlacing = false;

    void Start()
    {
        if (grid == null) grid = FindObjectOfType<GridManager>();
        if (save == null) save = FindObjectOfType<SaveService>();
        if (targetCamera == null) targetCamera = Camera.main;
    }

    void Update()
    {
        if (!isPlacing || activeConfig == null) return;

        Vector3 mouseWorld = targetCamera.ScreenToWorldPoint(Input.mousePosition);
        currentCell = grid.WorldToCell(mouseWorld);

        if (preview)
        {
            preview.transform.position = grid.CellToWorld(currentCell.x, currentCell.y);
            UpdatePreviewColor(currentCell);
        }

        if (Input.GetMouseButtonDown(0))
            TryPlaceBuilding(currentCell);

        if (Input.GetMouseButtonDown(1))
            CancelPlacement();
    }

    /// <summary>
    /// Активирует режим размещения здания
    /// </summary>
    /// <param name="config"></param>
    public void StartPlacement(BuildingConfig config)
    {
        activeConfig = config;
        isPlacing = true;
        CreatePreview(config);
    }

    /// <summary>
    /// Создает здание для предпросмотра
    /// </summary>
    /// <param name="cfg"></param>
    void CreatePreview(BuildingConfig cfg)
    {
        if (preview) Destroy(preview);

        preview = new GameObject("Preview_" + cfg.id);
        previewRenderer = preview.AddComponent<SpriteRenderer>();
        previewRenderer.sprite = Resources.Load<Sprite>(cfg.spritePath);
        previewRenderer.color = new Color(0f, 1f, 0f, 0.4f);
        preview.transform.localScale = Vector3.one;
    }

    /// <summary>
    /// Обновление цвета предпросмотра
    /// </summary>
    /// <param name="cell"></param>
    void UpdatePreviewColor(Vector2Int cell)
    {
        bool canPlace = grid.IsAreaFree(cell.x, cell.y, activeConfig.width, activeConfig.height);
        Color targetColor = canPlace ? new Color(0f, 1f, 0f, 0.4f) : new Color(1f, 0f, 0f, 0.4f);
        if (previewRenderer != null)
            previewRenderer.color = targetColor;
    }

    /// <summary>
    /// Проверка можно ли разместить здание
    /// </summary>
    /// <param name="cell"></param>
    void TryPlaceBuilding(Vector2Int cell)
    {
        if (!grid.IsAreaFree(cell.x, cell.y, activeConfig.width, activeConfig.height))
        {
            Debug.Log("Нельзя разместить здесь — место занято или вне сетки");
            return;
        }

        var go = new GameObject(activeConfig.id + "_" + Guid.NewGuid());
        go.transform.position = grid.CellToWorld(cell.x, cell.y);
        if (buildingRoot != null) go.transform.parent = buildingRoot;
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>(activeConfig.spritePath);
        sr.color = Color.white;
        var bi = go.AddComponent<BuildingInstance>();
        bi.id = activeConfig.id;
        bi.gridX = cell.x;
        bi.gridY = cell.y;
        bi.width = activeConfig.width;
        bi.height = activeConfig.height;

        grid.SetOccupied(cell.x, cell.y, activeConfig.width, activeConfig.height, true);
        save.RegisterBuildingInstance(bi, go.name);

        Destroy(preview);
        preview = null;
        previewRenderer = null;
        isPlacing = false;
        activeConfig = null;
    }

    /// <summary>
    /// Отмена предпросмотра здания
    /// </summary>
    void CancelPlacement()
    {
        if (preview) Destroy(preview);
        preview = null;
        previewRenderer = null;
        activeConfig = null;
        isPlacing = false;
    }
}