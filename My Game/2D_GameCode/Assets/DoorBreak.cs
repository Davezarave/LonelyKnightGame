using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBreak : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject _this;

    // Update is called once per frame
    void Update()
    {
        if (boss.activeSelf == false && (spawnPoint.position.x < player.position.x))
            _this.SetActive(false);
         
    }
}
