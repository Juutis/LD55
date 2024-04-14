using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapInstantiator : MonoBehaviour
{
    [SerializeField]
    private List<Tilemap> instanceTileMaps;
    [SerializeField]
    private Tile emptyTile;
    [SerializeField]
    private List<TileMapPrefab> tilePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var instanceTileMap in instanceTileMaps)
        {
            instanceTileMap.ResizeBounds();
            BoundsInt bounds = instanceTileMap.cellBounds;
            TileBase[] allTiles = instanceTileMap.GetTilesBlock(bounds);

            for (int xb = bounds.min.x; xb < bounds.max.x; xb++)
            {
                for (int yb = bounds.min.y; yb < bounds.max.y; yb++)
                {
                    for (int z = bounds.min.z; z < bounds.max.z; z++)
                    {
                        TileBase t = instanceTileMap.GetTile(new Vector3Int(xb, yb, z));

                        if (t == null)
                        {
                            continue;
                        }

                        instanceTileMap.SetTile(new Vector3Int(xb, yb, 0), null);

                        TileMapPrefab tileMapPrefab = tilePrefabs.Where(x => x.Tile.name == t.name).FirstOrDefault();

                        if (tileMapPrefab == null)
                        {
                            continue;
                        }

                        GameObject prefab = tileMapPrefab.Prefab;

                        if (prefab == null)
                        {
                            Debug.LogError($"Tile name {t.name} was not given a prefab in TileMapInstantiator!");
                            continue;
                        }

                        GameObject instance = Instantiate(prefab);
                        instance.SetActive(true);
                        instance.transform.position = instanceTileMap.CellToWorld(new Vector3Int(xb, yb, 0));
                        Vector3Int cellPosition = instanceTileMap.LocalToCell(instance.transform.localPosition);
                        instance.transform.localPosition = instanceTileMap.GetCellCenterLocal(cellPosition);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

[Serializable]
public class TileMapPrefab
{
    public Tile Tile;
    public GameObject Prefab;
}