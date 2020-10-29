
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map_gen : MonoBehaviour {

    [Range(0,100)] 
    public int randomFillPercent;
    public int width;
    public int height;

    public string seed;

    public Tilemap baseMap;
    public Tile grassTile;
    public Tile stoneTile;

    int[,] map;

    void Start() {
        GenerateMap();
    }

    void GenerateMap() {
        map = new int[width, height];
        RandomFillMap();
        RichCenter();

        for (int i = 0; i < 5; i++) {
            SmoothMap();
        }
        if (display) {
            DrawTiles();
        }      
    }

    void RandomFillMap() {
        if (seed == null) {
            System.Random rnd = new System.Random();
            int seed = rnd.Next(1,2000001);
        }

        System.Random prng = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                map[x, y] = (prng.Next(0,100) < randomFillPercent)? 1: 0;
            }
        }
    }

    void SmoothMap() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                int neighbourStoneCount = StoneTilesCount(x,y);

                if (neighbourStoneCount > 4)
                    map[x, y] = 1;
                else if (neighbourStoneCount <4)
                    map[x, y] = 0;
            }
        }
    }

    void RichCenter() {
        int centralX = width/2;
        int centralY = height/2;
        for (int x = centralX -5; x <= centralX + 5; x++) {
            for (int y = centralY -5; y <= centralY +5; y++) {
                map[x, y] = 1;
            }
        }
    }

    int StoneTilesCount(int gridX, int gridY) {
        int stoneCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX +1; neighbourX++) {
            for (int neighbourY = gridY - 1; neighbourY <= gridY +1; neighbourY++) {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height) {
                    if (neighbourX != gridX || neighbourY != gridY) {
                        stoneCount += map[neighbourX,neighbourY];
                    }
                }
                else {
                    stoneCount++;
                }
            }
        }

        return stoneCount;
    }

    public bool display;

    void DrawTiles() {
        if (display) {
            if (map != null) {
                for (int x = 0; x < width; x++) {
                    for (int y = 0; y < height; y++) {
                        if (map[x,y] == 1) {
                            baseMap.SetTile(new Vector3Int(x,y,0),stoneTile);
                        }
                        else {
                            baseMap.SetTile(new Vector3Int(x,y,0),grassTile);
                        }    
                    }
                }
            }
        }
    }


}