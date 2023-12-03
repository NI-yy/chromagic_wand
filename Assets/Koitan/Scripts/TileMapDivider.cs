using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TileMapDivider : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemapSource;
    [SerializeField]
    int cellWidthCount = 10, cellHeightCount = 10;
    [SerializeField]
    int divideCountMax;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        if (tilemapSource == null) return;
        Vector3 start, end;
        start = new Vector3(0, -divideCountMax * cellHeightCount);
        end = new Vector3(0, divideCountMax * cellHeightCount);
        for (int i= -divideCountMax; i<=divideCountMax; i++)
        {
            start.x = i * cellWidthCount;
            Gizmos.DrawLine(start, end);
        }  
    }

#if UNITY_EDITOR
    [ContextMenu("Divide")]
    public void Divide()
    {
        GameObject tileMapParent = new GameObject("TileMapParent");
        tileMapParent.AddComponent<Grid>();
        EditorUtility.SetDirty(tileMapParent);
        var bound = tilemapSource.cellBounds;

        //tm.SetTile()
        // ç∂â∫ÇÃç¿ïWÇì¡íËÇ∑ÇÈ
        int xOffset = Mathf.FloorToInt(bound.xMin / cellWidthCount) - 1;
        int yOffset = Mathf.FloorToInt(bound.yMin / cellHeightCount) - 1;
        for (int y = 0; y < 100; y++)
        {
            float yPos = cellHeightCount * (y + yOffset);
            for (int x = 0; x < 100; x++)
            {

                float xPos = cellWidthCount * (x + xOffset);
                Vector3 ll = new Vector3(xPos, yPos);
                GameObject tileObject = new GameObject($"tile_{x}{y}");
                tileObject.transform.SetParent(tileMapParent.transform);
                Tilemap tm = tileObject.AddComponent<Tilemap>();
                tm.AddComponent<TilemapRenderer>();
                bool existFlag = false;
                for (int j = 0; j < cellHeightCount; j++)
                {
                    for (int i = 0; i < cellWidthCount; i++)
                    {
                        Vector3Int pos = tilemapSource.WorldToCell(new Vector3(xPos + i, yPos + j));
                        TileBase tb = tilemapSource.GetTile(pos);
                        if (tb != null)
                        {
                            tm.SetTile(pos, tilemapSource.GetTile(pos));
                            existFlag = true;
                        }
                    }
                }
                if (!existFlag)
                {
                    DestroyImmediate(tileObject);
                }
            }
        }
    }
#endif
}
