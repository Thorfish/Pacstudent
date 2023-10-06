using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateLevel : MonoBehaviour
{
    [SerializeField]
    private Tilemap walls;
    [SerializeField]
    private Tilemap pellets;

    [SerializeField]
    private TileBase[] wallTiles;
    [SerializeField]
    private TileBase[] pelletTiles;

    [SerializeField]
    private GameObject powerPellet;

    // Start is called before the first frame update
    void Start()
    {
        //loop through tilemap
        int z = pellets.cellBounds.z;
        for (int x = pellets.cellBounds.x; x < pellets.cellBounds.xMax; x++)
        {
            for (int y = pellets.cellBounds.y; y < pellets.cellBounds.yMax; y++)
            {
                Vector3Int point = new Vector3Int(x, y, z);
                if (pellets.HasTile(point)) {
                    if (pellets.GetTile(point) == pelletTiles[1])
                    {
                        Instantiate(powerPellet, point+pellets.tileAnchor, Quaternion.identity);
                    }
                }
            }
            
        }
        pellets.ClearAllTiles();
    }

    // Update is called once per frame
    void Update()
    {
        //Place Pellets
        
    }
}
