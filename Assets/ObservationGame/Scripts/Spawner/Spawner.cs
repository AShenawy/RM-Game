using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] int totalNumberToSpawn;
    [SerializeField] Transform parent;
    [SerializeField] Spawnable[] spawnablePrefabs;
    [SerializeField] Transform[] spawnPoints;

    int totalNumberSpawned = 0;
    List<Transform> availableSpawnPoints;

    void Awake() => availableSpawnPoints = spawnPoints.ToList();

    public void Spawn()
    {
        ClearVisitors();
        while (totalNumberSpawned < totalNumberToSpawn)
        {
            Spawnable prefab = ChooseRandomPrefab();
            if (prefab != null)
            {
                Transform spawnPoint = ChooseRandomSpawnPoint(availableSpawnPoints);
                if (prefab.TypeIndex != spawnPoint.GetComponent<SpawnPoint>().TypeIndex)
                    continue;

                if (availableSpawnPoints.Contains(spawnPoint))
                    availableSpawnPoints.Remove(spawnPoint);

                var npc = Instantiate(prefab.transform, spawnPoint.position, spawnPoint.rotation, parent);
                totalNumberSpawned++;
            }
        }
    }

    void ClearVisitors()
    {
        totalNumberSpawned = 0;
        availableSpawnPoints.Clear();
        availableSpawnPoints = spawnPoints.ToList();
        if (parent.childCount > 0)
            foreach (var item in parent.GetComponentsInChildren<Spawnable>())
                Destroy(item.gameObject);
    }

    Transform ChooseRandomSpawnPoint(List<Transform> availableSpawnPoints)
    {
        if (availableSpawnPoints.Count == 0)
            return transform;

        if (availableSpawnPoints.Count == 1)
            return availableSpawnPoints[0];

        int index = UnityEngine.Random.Range(0, availableSpawnPoints.Count);
        return availableSpawnPoints[index];
    }

    Spawnable ChooseRandomPrefab()
    {
        if (spawnablePrefabs.Length == 0)
            return null;

        if (spawnablePrefabs.Length == 1)
            return spawnablePrefabs[0];

        int index = UnityEngine.Random.Range(0, spawnablePrefabs.Length);
        return spawnablePrefabs[index];
    }
}