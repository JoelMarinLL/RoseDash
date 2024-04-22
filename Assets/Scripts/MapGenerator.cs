using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject _floorPrefab;
    [Tooltip("How many blocks width the map will be")]
    [SerializeField] int _width;
    [Tooltip("How many blocks lenght the map will be")]
    [SerializeField] int _lenght;

    [SerializeField] int _checkPointAt;

    // Start is called before the first frame update
    void Start()
    {
        for (int z = 0; z < _lenght; z++)
        {
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
                if (z == _checkPointAt && x == _width - 1)
                {
                    GameObject CheckPointGameObject = new GameObject();
                    BoxCollider box = CheckPointGameObject.AddComponent<BoxCollider>();
                    box.size = new Vector3(_width + 1, 1, 1);
                    CheckPointGameObject.tag = "CheckPoint";
                    CheckPointGameObject.name = "CheckPointGameObject";
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
}

