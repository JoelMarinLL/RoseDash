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

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _lenght; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                if (j == 0)
                {
                    Debug.Log("Spawn Collider");
                    GameObject newObject = new GameObject();
                    BoxCollider box = newObject.AddComponent<BoxCollider>();
                    box.size = Vector3.one;
                    Instantiate(newObject, new Vector3(0, 1, 1+i), Quaternion.identity);
                    Destroy(newObject);
                }else if (j + 1 >= _width)
                {
                    GameObject newObject = new GameObject();
                    BoxCollider box = newObject.AddComponent<BoxCollider>();
                    box.size = Vector3.one;
                    Instantiate(newObject, new Vector3(j+2, 1, 1 + i), Quaternion.identity);
                    Destroy(newObject);
                }
                Vector3 pos = new Vector3(_floorPrefab.GetComponent<BoxCollider>().size.x + j,
                    0,
                    _floorPrefab.GetComponent<BoxCollider>().size.z + i);
                Instantiate(_floorPrefab, pos, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
