using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject firewoodPrefab, enemyPrefab;
    [SerializeField] private float spawnFuelCooldown, spawnEnemyCooldown;
    [SerializeField] private int maxFuelAtScene, maxEnemyAtScene;

    private List<GameObject> _fuels = new List<GameObject>();
    private List<GameObject> _enemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(Spawn(firewoodPrefab, spawnFuelCooldown, _fuels, maxFuelAtScene));
        StartCoroutine(Spawn(enemyPrefab, spawnEnemyCooldown, _enemies, maxEnemyAtScene));
    }

    private IEnumerator Spawn(GameObject pref, float cooldown, List<GameObject> collection, int maxValue)
    {
        while (!GameManager.IsPaused)
        {
            if (collection.Count < maxValue)
                collection.Add(Instantiate(pref, Random.insideUnitCircle * GameManager.WorldSize / 2,
                    Quaternion.identity));
            
            yield return new WaitForSeconds(cooldown);
        }
    }
}