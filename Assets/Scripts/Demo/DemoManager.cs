using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class DemoManager : MonoBehaviour
{
    private Camera _cam;
    private PlayerMovement _player;
    [SerializeField] private PlayerData playerTypes;
    [SerializeField] private Tilemap[] levels;
    [SerializeField] private Transform spawnPoint;

    private int _currentTilemapIndex;
    private Color _currentForegroundColor;

    public SceneData SceneData;

    private void Awake()
    {
        _cam = FindObjectOfType<Camera>();
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        SetSceneData(SceneData);
        SwitchLevel(1);

    }

    public void SetSceneData(SceneData data)
    {
        SceneData = data;

        //Update the camera and tilemap color according to the new data.
        _cam.orthographicSize = data.camSize;
        _cam.backgroundColor = data.backgroundColor;
        levels[_currentTilemapIndex].color = data.foregroundColor;

        _currentForegroundColor = data.foregroundColor;
    }

    public void SwitchLevel(int index)
    {
        //Switch tilemap active and apply color.
        levels[_currentTilemapIndex].gameObject.SetActive(false);
        levels[index].gameObject.SetActive(true);
        levels[index].color = _currentForegroundColor;
        levels[_currentTilemapIndex] = levels[index];

        _player.transform.position = spawnPoint.position;

        _currentTilemapIndex = index;
    }
    

}
