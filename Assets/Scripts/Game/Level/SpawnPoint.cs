using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public string spawnPointLayer = "SpawnPoint";
    public string enemyLayer = "Enemy";

    public bool IsFree()
    {
        return Physics.OverlapSphere(transform.position, 1f, LayerMask.GetMask(enemyLayer)).Length == 0;
    }

    public Vector3 getPoint()
    {
        return transform.position;
    }

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer(spawnPointLayer);
    }

    
}
