using UnityEngine;
using UnityEngine.UIElements;

public class WaterGenerator : MonoBehaviour
{
    [SerializeField] private int _minWaterRange, _maxWaterRange;
    [SerializeField] private int _lakesCount;
    [SerializeField] private int _lakeSize;
    [SerializeField] private float _threshold;

    public void GenerateWater(Tile[,] playground)
    {
        for (int x = 0; x < playground.GetLength(0)-1; x++)
        {
            for (int y = 0; y < playground.GetLength(1)-1; y++)
            {
                var noise = Mathf.PerlinNoise((float)x / (float)playground.GetLength(0)*10, (float)y / (float)playground.GetLength(0)*10);

                if (noise >= _threshold)
                {
                    playground[x, y].Type = TileType.Water;
                }
            }
        }
        //for (int i = 0; i < _lakesCount; i++)
        //{
        //    Vector2 offset = Random.insideUnitCircle * _maxWaterRange;
        //    offset = offset.normalized * Mathf.Clamp(offset.magnitude, _minWaterRange, _maxWaterRange);
        //    offset += new Vector2(playground.GetLength(0) / 2, playground.GetLength(0) / 2);
        //    if (offset.x < playground.GetLength(0) && offset.y < playground.GetLength(1))
        //        GenerateLake(playground, new Vector2Int((int)offset.x, (int)offset.y));
        //}
    }

    public void GenerateLake(Tile[,] playground, Vector2Int position)
    {
        for (int x = 0; x < _lakeSize - 1; x++)
        {
            for (int y = 0; y < _lakeSize - 1; y++)
            {
                if (x + position.x < playground.GetLength(0) - 1 && y + position.y < playground.GetLength(1) - 1 &&
                    x + position.x >= 0 && y + position.y >= 0)
                {
                    Debug.Log($"{x + position.x}, {y + position.y}");
                    playground[x + position.x, y + position.y].Type = TileType.Water;
                }

            }
        }
    }
}
