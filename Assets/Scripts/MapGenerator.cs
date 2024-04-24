using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance;

    [Header("Config")]
    [SerializeField] GameObject _floorPrefab;
    [Tooltip("How many blocks lenght the map will be")]
    [SerializeField] int _lenght;

    [SerializeField] int _checkPointAt;

    [Header("Obstacles")]
    [SerializeField][Range(0f, 1f)] float rewardRatio;
    [SerializeField] int startingClearedCells;
    [SerializeField] int minObstacleSpacing;
    [SerializeField] int minObstacleCountTotal;
    [SerializeField] int maxObstacleCountTotal;
    [SerializeField] List<GameObject> obstaclePrefabs;
    [SerializeField] List<GameObject> rewardPrefabs;

    public int CheckpointZPosition { get => _checkPointAt + 1; }

    int _width = 4;

    float[] obstaclePositions; // positions on the z axis of all obstacles

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GetObstaclePositions();
        for (int z = 0; z < _lenght; z++)
        {
            PlaceObstacles(z + 1);
            for (int x = 0; x < _width; x++)
            {
                if (x == 0)
                {
                    GameObject newObject = new GameObject();
                    BoxCollider box = newObject.AddComponent<BoxCollider>();
                    box.size = Vector3.one;
                    Instantiate(newObject, new Vector3(0, 1, 1 + z), Quaternion.identity);
                    Destroy(newObject);
                }
                else if (x + 1 >= _width)
                {
                    GameObject newObject = new GameObject();
                    BoxCollider box = newObject.AddComponent<BoxCollider>();
                    box.size = Vector3.one;
                    Instantiate(newObject, new Vector3(x + 2, 1, 1 + z), Quaternion.identity);
                    Destroy(newObject);
                }
                Vector3 pos = new Vector3(_floorPrefab.GetComponent<BoxCollider>().size.x + x,
                    0,
                    _floorPrefab.GetComponent<BoxCollider>().size.z + z);
                Instantiate(_floorPrefab, pos, Quaternion.identity);
                if (z + 1 == _checkPointAt && x == _width - 1)
                {
                    GameObject CheckPointGameObject = new GameObject();
                    BoxCollider box = CheckPointGameObject.AddComponent<BoxCollider>();
                    box.size = new Vector3(_width + 1, 1, 1);
                    box.isTrigger = true;
                    CheckPointGameObject.tag = "CheckPoint";
                    CheckPointGameObject.name = "CheckPointGameObject";
                    CheckPointGameObject.AddComponent<CheckEnd>();
                    float xPos = x + 1.5f - (_width / 2);
                    if (_width % 2 != 0)
                    {
                        xPos -= 0.5f;
                    }

                    Instantiate(CheckPointGameObject, new Vector3(xPos, 1, 1 + z), Quaternion.identity);
                    Destroy(CheckPointGameObject);
                }
            }
        }
    }

    void GetObstaclePositions()
    {
        int obstacleNum = Random.Range(minObstacleCountTotal, maxObstacleCountTotal + 1);
        
        obstaclePositions = new float[obstacleNum];
        for (int i = 0; i < obstacleNum; i++)
        {
            int? zPos;
            int attempts = 0;
            int maxAttempts = 100;
            int start = startingClearedCells + 1;
            bool hasNeighbours;
            do
            {
                attempts++;
                if (attempts < maxAttempts) zPos = Random.Range(start, _checkPointAt + 1);
                else zPos = null;

                if (zPos != null) hasNeighbours = ObstacleHasNeighbours(zPos.Value);
                else hasNeighbours = false;
            } while (hasNeighbours);
            
            if (zPos != null) obstaclePositions[i] = zPos.Value;
        }
    }

    bool ObstacleHasNeighbours(int value)
    {
        if (obstaclePositions.Contains(value)) return true;
        for (int i = 1; i <= minObstacleSpacing; i++)
            if (obstaclePositions.Contains(value + i) || obstaclePositions.Contains(value - i)) 
                return true;

        return false;
    }

    void PlaceObstacles(float z)
    {
        if (!obstaclePositions.Contains(z)) return;
        float xL = (_width / 2) - 0.5f;
        float xR = _width - 0.5f;
        var positionL = new Vector3(xL, 1, z);
        var positionR = new Vector3(xR, 1, z);
        Instantiate(GetRandomObstacle(), positionL, Quaternion.identity);
        Instantiate(GetRandomObstacle(), positionR, Quaternion.identity);
    }

    GameObject GetRandomObstacle()
    {
        bool isReward = Random.value < rewardRatio;
        if (isReward) return rewardPrefabs[Random.Range(0, rewardPrefabs.Count)];
        else return obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
    }
}

