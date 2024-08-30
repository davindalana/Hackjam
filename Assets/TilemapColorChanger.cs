using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapColorChanger : MonoBehaviour
{
    public SceneData sceneData; // Reference to the SceneData ScriptableObject
    public Tilemap backgroundTilemap; // Reference to the background Tilemap
    public Tilemap foregroundTilemap; // Reference to the foreground Tilemap
    public Transform playerTransform; // Reference to the player

    private void Update()
    {
        if (playerTransform != null && sceneData != null)
        {
            // Check if the player is below y -50
            if (playerTransform.position.y < -50)
            {
                // Change the foreground color in the ScriptableObject
                sceneData.foregroundColor = new Color32(246, 153, 134, 255); // Hex color A43821
                ApplyColors();
            }
            else
            {
                // Change the foreground color in the ScriptableObject
                sceneData.foregroundColor = new Color32(189, 255, 254, 255); // Hex color BDFFFE
                ApplyColors();
            }
        }
    }

    private void ApplyColors()
    {
        // Apply the colors from the ScriptableObject to the Tilemaps
        if (foregroundTilemap != null)
        {
            foregroundTilemap.color = sceneData.foregroundColor;
        }

        if (backgroundTilemap != null)
        {
            //backgroundTilemap.color = sceneData.backgroundColor;
        }
    }
}
