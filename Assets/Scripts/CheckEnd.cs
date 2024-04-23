using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.TryGetComponent(out PlayerMovement playerMovement))
            {
                playerMovement.ArriveCheckPoint();
            }
        }
    }
}
