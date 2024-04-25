using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance;

    [Header("Config")]
    [SerializeField] GameObject _floorPrefab;
    [SerializeField] GameObject CheckPointGameObject;
    [Tooltip("How many blocks width the map will be")]
    [SerializeField] int _width;
    [Tooltip("How many blocks lenght the map will be")]
    [SerializeField] int _lenght;

    [SerializeField] int _checkPointAt;

    [Header("Obstacles")]
    [SerializeField] [Range(0f, 1f)] float rewardRatio;
    [SerializeField] int startingClearedCells;
    [SerializeField] int minObstacleCountTotal;
    [SerializeField] int maxObstacleCountTotal;
    [SerializeField] int minObstacleCountPerCell;
    [SerializeField] int maxObstacleCountPerCell;
    [SerializeField] List<GameObject> obstaclePrefabs;
    [SerializeField] List<GameObject> rewardPrefabs;

    public int CheckpointZPosition { get => _checkPointAt + 1; }

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
                /*
                if (x == 0) //Spawn GameObjects para bloquear el borde izquierdo
                {
                    GameObject newObject = new GameObject();
                    BoxCollider box = newObject.AddComponent<BoxCollider>();
                    box.size = Vector3.one;
                    Instantiate(newObject, new Vector3(0, 1, 1 + z), Quaternion.identity);
                    Destroy(newObject);
                }
                else if (x + 1 >= _width) //Spawn GameObjects para bloquear el borde izquierdo
                {
                    GameObject newObject = new GameObject();
                    BoxCollider box = newObject.AddComponent<BoxCollider>();
                    box.size = Vector3.one;
                    Instantiate(newObject, new Vector3(x + 2, 1, 1 + z), Quaternion.identity);
                    Destroy(newObject);
                }
                */
                Vector3 pos = new Vector3(_floorPrefab.GetComponent<BoxCollider>().size.x + x,
                    0,
                    _floorPrefab.GetComponent<BoxCollider>().size.z + z);
                Instantiate(_floorPrefab, pos, Quaternion.identity);
                if (z + 1 == _checkPointAt && x == _width - 1)
                {
                    //GameObject CheckPointGameObject = new GameObject();
                    //BoxCollider box1 = CheckPointGameObject.AddComponent<BoxCollider>();
                    //box1.size = new Vector3(_width + 1, 1, 1);
                    //box1.isTrigger = true;
                    //CheckPointGameObject.tag = "CheckPoint";
                    //CheckPointGameObject.name = "CheckPointGameObject";
                    //CheckPointGameObject.AddComponent<CheckEnd>();
                    float xPos = x + 1.5f - (_width / 2);
                    if (_width % 2 != 0)
                    {
                        xPos -= 0.5f;
                    }

                    var go = Instantiate(CheckPointGameObject, new Vector3(xPos, 1, 1 + z), Quaternion.identity);
                    //go.transform.localScale = _width;
                    Destroy(CheckPointGameObject);

                }

            }
        }
        GameObject newObject = new GameObject();
        BoxCollider box = newObject.AddComponent<BoxCollider>();
        box.size = new Vector3(1, 2, _lenght);
        Instantiate(newObject, new Vector3(0, 1, _lenght / 2), Quaternion.identity);
        Instantiate(newObject, new Vector3(_width + 1, 1, _lenght / 2), Quaternion.identity);
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

                if (zPos != null) hasNeighbours = obstaclePositions.Contains(zPos.Value + 1) || obstaclePositions.Contains(zPos.Value - 1);
                else hasNeighbours = false;
            } while (hasNeighbours);

            if (zPos != null) obstaclePositions[i] = zPos.Value;
        }
    }

    void PlaceObstacles(float z)
    {
        if (!obstaclePositions.Contains(z)) return;
        int obstacleCountInCell = Random.Range(minObstacleCountPerCell, maxObstacleCountPerCell + 1);
        obstacleCountInCell = Mathf.Clamp(obstacleCountInCell, 0, _width);
        for (int i = 1; i <= obstacleCountInCell; i++)
        {
            int x = _width / obstacleCountInCell * i;
            var position = new Vector3(x, 1, z);
            Instantiate(GetRandomObstacle(), position, Quaternion.identity);
        }
    }

    GameObject GetRandomObstacle()
    {
        bool isReward = Random.value < rewardRatio;
        if (isReward) return rewardPrefabs[Random.Range(0, rewardPrefabs.Count)];
        else return obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
    }
}

