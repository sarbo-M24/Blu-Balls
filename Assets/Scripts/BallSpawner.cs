using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject redBall;
    public GameObject blueBall;

    public float spawnInterval = 2f;

    public float maxSpawnXAxis = 0f;
    public float minSpawnXAxis = 0f;
    public float maxSpawnYAxis = 0f;
    public float minSpawnYAxis = 0f;
    public float spawnZaxis = 0f;

    [Header("Endless Mode")]
    [Tooltip("If true, spawn positions are relative to this spawner's transform " +
             "(which follows the player). Leave false for enclosed mode.")]
    public bool spawnRelativeToSelf = false;

    private void Start()
    {
        InvokeRepeating(nameof(spawnBalls), 2f, spawnInterval);
    }

    void spawnBalls()
    {
        Spawn(redBall);
        Spawn(blueBall);
    }

    void Spawn(GameObject prefab)
    {
        Vector3 local = new Vector3(
            Random.Range(minSpawnXAxis, maxSpawnXAxis),
            Random.Range(minSpawnYAxis, maxSpawnYAxis),
            spawnZaxis);

        Vector3 pos = spawnRelativeToSelf ? transform.position + local : local;
        Instantiate(prefab, pos, Quaternion.identity);
    }
}