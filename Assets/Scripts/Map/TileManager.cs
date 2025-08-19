using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public List<List<MapTile>> mapTiles = new();
    public RectTransform scrollViewPanel;
    public RewardManager rewardManager;
    public GameObject rewardPanel;
    public MapTile currentTile;
    public GameObject mapTilePrefab;

    private void Start() {
        CreateMapTiles();
        UpdateTileAccess();
    }

    private void UpdateTileAccess() {
        bool isTileActive = PlayerPrefs.HasKey("TileActive");

        for (int y = 0; y < mapTiles.Count; y++) {
            for (int x = 0; x < mapTiles[y].Count; x++) {


                bool isCompleted = PlayerPrefs.GetInt($"TileCompleted_{y}", 0) == 1;
                bool isLastCompleted = PlayerPrefs.GetInt($"LastCompleted_{y}", 0) == 1;

                mapTiles[y][x].MarkAsCompleted(isCompleted);

                if (!isTileActive && isLastCompleted) {
                    mapTiles[y][x].UnlockNextTiles();

                    //TODO: Scroll to finished tile

                    if (mapTiles[y][x].tileType == MapTile.TileType.Battlefield && PlayerPrefs.GetInt($"RewardChosen", 0) == 0) {
                        rewardManager.ShowReward(mapTiles[y][x].enemyType);
                    }
                }
            }
        }

        if (isTileActive) {
            string activeTileIndex = PlayerPrefs.GetString("TileActive", "");
            string[] activeTileIndexSplit = activeTileIndex.Split("-");
            Vector2Int activeTileIndexVector = new(int.Parse(activeTileIndexSplit[0]), int.Parse(activeTileIndexSplit[1]));
            mapTiles[activeTileIndexVector.y][activeTileIndexVector.x].SetUnlocked(true);
        }
    }

    public void MarkTileAsCurrent(MapTile tile) {
        currentTile = tile;
        string tileIndex = $"{tile.gridIndex.y}-{tile.gridIndex.x}";
        TileCompleter.MarkTileAsCompleted(false, tileIndex);
        PlayerPrefs.SetString($"TileActive", tileIndex);
    }

    private GameObject CreateMapTile(Vector2 tilePos, Vector2 gridIndex) {
        GameObject mapTileObject = Instantiate(mapTilePrefab, tilePos, quaternion.identity, scrollViewPanel);
        mapTileObject.name = $"MapTile {gridIndex.y}-{gridIndex.x}";

        MapTile mapTile = mapTileObject.GetComponent<MapTile>();
        mapTile.gridIndex = gridIndex;
        mapTiles[(int)gridIndex.y].Add(mapTile);

        return mapTileObject;
    }

    private void CreateMapTiles() {

        for (int y = 0; y < 5; y++) {
            mapTiles.Add(new List<MapTile>());

            switch (y) {
                case 0:
                    for (int x = 0; x < 3; x++) {
                        Vector2 tilePos = new(-400 + x * 400, 200);
                        GameObject mapTileObject = CreateMapTile(tilePos, new(x, y));

                        RectTransform rect = mapTileObject.GetComponent<RectTransform>();
                        rect.anchoredPosition = tilePos;

                        MapTile mapTile = mapTileObject.GetComponent<MapTile>();
                        mapTile.SetUnlocked(true);
                    }
                    break;

                case 1:
                    foreach (var mapTile in mapTiles[y - 1]) {
                        int nParents = Rng.Range(2, 3);

                        for (int x = 0; x < nParents; x++) {
                            int xPos = nParents == 1 ? (int)mapTile.transform.position.x : (int)mapTile.transform.position.x - 100 + (x * 200);
                            Vector2 tilePos = new(xPos, mapTile.transform.position.y + 200);
                            GameObject parentMapTileObject = CreateMapTile(tilePos, new(x, y));

                            MapTile parentMapTile = parentMapTileObject.GetComponent<MapTile>();
                            mapTile.nextTiles.Add(parentMapTile);
                        }
                    }
                    break;
            }
        }
    }
}
