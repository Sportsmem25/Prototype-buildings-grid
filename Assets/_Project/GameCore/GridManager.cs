using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid settings")]
    public Grid unityGrid;
    public int columns = 20;
    public int rows = 12;

    private bool[,] occupied;

    private void Awake()
    {
        if (unityGrid == null)
            unityGrid = FindObjectOfType<Grid>();

        occupied = new bool[columns, rows];
    }

    /// <summary>
    /// �����, ������������ ������� ���������� ������ �� ��������
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector3 CellToWorld(int x, int y)
    {
        Vector3Int cell = new Vector3Int(x, y, 0);
        Vector3 worldOrigin = unityGrid.CellToWorld(cell); // ���������� origin ������ (������ ������ ����� ����)
        Vector3 half = new Vector3(unityGrid.cellSize.x, unityGrid.cellSize.y, unityGrid.cellSize.z) * 0.5f;
        return worldOrigin + half;
    }

    /// <summary>
    /// �����, ����������� ������� ���������� � ������ ������ (x,y)
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public Vector2Int WorldToCell(Vector3 worldPos)
    {
        Vector3Int cell = unityGrid.WorldToCell(worldPos);
        return new Vector2Int(cell.x, cell.y);
    }

    /// <summary>
    /// �������� ���������� �������
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="w"></param>
    /// <param name="h"></param>
    /// <returns></returns>
    public bool CanPlace(int x, int y, int w, int h)
    {
        if (x < 0 || y < 0 || x + w > columns || y + h > rows) return false;
        for (int i = x; i < x + w; i++)
            for (int j = y; j < y + h; j++)
                if (occupied[i, j]) return false;
        return true;
    }

    /// <summary>
    /// �����, ���������� ������ ��� ��������� ��� �������
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="w"></param>
    /// <param name="h"></param>
    /// <param name="value"></param>
    public void SetOccupied(int x, int y, int w, int h, bool value)
    {
        for (int i = x; i < x + w; i++)
            for (int j = y; j < y + h; j++)
                if (i >= 0 && j >= 0 && i < columns && j < rows)
                    occupied[i, j] = value;
    }

    /// <summary>
    /// �����, ���������� ������ �� ������ �� �����
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsOccupied(int x, int y)
    {
        if (x < 0 || y < 0 || x >= columns || y >= rows) return true;
        return occupied[x, y];
    }

        /// <summary>
    /// �����, ���������� �������� �� ������
    /// </summary>
    /// <param name="startX"></param>
    /// <param name="startY"></param>
    /// <param name="w"></param>
    /// <param name="h"></param>
    /// <returns></returns>
    public bool IsAreaFree(int startX, int startY, int w, int h)
    {
        if (startX < 0 || startY < 0 || startX + w > columns || startY + h > rows) return false;
        for (int x = startX; x < startX + w; x++)
            for (int y = startY; y < startY + h; y++)
                if (IsOccupied(x, y)) return false;
        return true;
    }
}
