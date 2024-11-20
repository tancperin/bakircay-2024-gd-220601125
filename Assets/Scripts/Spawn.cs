using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Spawn : MonoBehaviour
{
    public GameObject[] objs;
    public int maxSpawn = 5;
    private float timeBtwSpawn = .5f;
    private float timer = 0;

    private Transform spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Item").Length < maxSpawn)
        {
            if (timer <= 0)
            {
                Transform tr = spawnPoint;
                tr.position = new Vector3(tr.position.x + Random.Range(-0.5f, 0.5f), tr.position.y, tr.position.z + Random.Range(-0.5f, 0.5f));
                SpawnObject(objs[Random.Range(0, objs.Length)], tr);
                timer = timeBtwSpawn;
            }
            else
            {
                timer -= Time.deltaTime;
            }
            
        }    
    }

    void SpawnObject(GameObject obj, Transform spawnPoint)
    {
        Instantiate(obj, spawnPoint.position, Quaternion.identity);
    }
}
