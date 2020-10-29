using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
 
public class Map_interaction : MonoBehaviour
{
    public Grid grid;
    public Tilemap baseMap;
    public void Update() {
        
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
            Debug.Log(coordinate);
            Debug.Log(baseMap.GetSprite(coordinate));
        }
    }         
}