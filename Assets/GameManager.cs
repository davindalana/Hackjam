using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static void SaveGame(Vector3 checkpointPosition)
    {
        PlayerPrefs.SetFloat("PlayerPosX", checkpointPosition.x);
        PlayerPrefs.SetFloat("PlayerPosY", checkpointPosition.y);
        PlayerPrefs.SetFloat("PlayerPosZ", checkpointPosition.z);

        // Save other necessary game data here

        PlayerPrefs.Save();
        Debug.Log("Game Saved!");
    }

    public static void LoadGame(Transform playerTransform)
    {
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");

            playerTransform.position = new Vector3(x, y, z);

            // Load other necessary game data here

            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.LogWarning("No saved game found!");
        }
    }
}
