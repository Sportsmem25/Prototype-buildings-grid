using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GridRenderer : MonoBehaviour
{
    public GridManager gridManager;
    public Color gridColor = new Color(0.8f, 0.8f, 0.8f);
    public float lineWidth = 0.02f;
    public Material lineMaterial;

    private GameObject parentLines;

    void Start()
    {
        if (gridManager == null) gridManager = FindObjectOfType<GridManager>();
        DrawGrid();
    }

    /// <summary>
    /// ����� ��� ��������� �����
    /// </summary>
    public void DrawGrid()
    {
        if (gridManager == null || gridManager.unityGrid == null) return;

        if (parentLines != null) DestroyImmediate(parentLines);
        parentLines = new GameObject("GridLines");
        parentLines.transform.SetParent(transform, false);
        var mat = lineMaterial ?? new Material(Shader.Find("Sprites/Default"));
        mat.color = gridColor;
        int cols = gridManager.columns;
        int rows = gridManager.rows;
        var g = gridManager.unityGrid;
        Vector3 cellSize = g.cellSize;

        // ������������ �����
        for (int x = 0; x <= cols; x++)
        {
            var go = new GameObject("GridLine_V_" + x);
            go.transform.SetParent(parentLines.transform, false);
            var lr = go.AddComponent<LineRenderer>();
            lr.material = mat;
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.positionCount = 2;
            lr.useWorldSpace = true;
            Vector3Int startCell = new Vector3Int(x, 0, 0);
            Vector3 start = g.CellToWorld(startCell);
            Vector3Int endCell = new Vector3Int(x, rows, 0);
            Vector3 end = g.CellToWorld(endCell);

            // �������� ����� �� ������ ������, ����� ��������� � CellToWorld(center)
            start += new Vector3(cellSize.x * 0.0f, cellSize.y * 0.0f, 0f);
            end += new Vector3(cellSize.x * 0.0f, 0f, 0f);
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            lr.sortingOrder = 100;
        }

        // �������������� �����
        for (int y = 0; y <= rows; y++)
        {
            var go = new GameObject("GridLine_H_" + y);
            go.transform.SetParent(parentLines.transform, false);
            var lr = go.AddComponent<LineRenderer>();
            lr.material = mat;
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.positionCount = 2;
            lr.useWorldSpace = true;
            Vector3Int startCell = new Vector3Int(0, y, 0);
            Vector3 start = g.CellToWorld(startCell);
            Vector3Int endCell = new Vector3Int(cols, y, 0);
            Vector3 end = g.CellToWorld(endCell);
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            lr.sortingOrder = 100;
        }
    }
}