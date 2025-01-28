using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Spawn : MonoBehaviour
{
    #region Variables
    #region Serialized
    #endregion
    #region Private
    private float timeBtwSpawn = .2f;
    private float timer = 0f;
    public bool doneSpawn = false;
    private Transform spawnPoint;
    #endregion

    #region Public
    public enum SpawnType { Random, Pairs }
    public GameObject[] objs;
    public int maxSpawn = 6;
    public SpawnType spawnType;
    public float itemSpawnSize = 2f;
    #endregion
    #endregion

    void Start()
    {
        spawnPoint = GetComponent<Transform>();
        switch (GameMode.gameMode)
        {
            case "Pair10":
                {
                    maxSpawn = 10;
                    spawnType = SpawnType.Pairs;
                    break;
                }
            case "Pair25":
                {
                    maxSpawn = 25;
                    spawnType = SpawnType.Pairs;
                    break;
                }
            case "Pair40":
                {
                    maxSpawn = 40;
                    spawnType = SpawnType.Pairs;
                    break;
                }
            case "Unlimited":
                {
                    maxSpawn = 20;
                    spawnType = SpawnType.Random;
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnType == SpawnType.Pairs && GameObject.FindGameObjectsWithTag("Item").Length >= maxSpawn * 2 || spawnType == SpawnType.Random && GameObject.FindGameObjectsWithTag("Item").Length >= maxSpawn) doneSpawn = true;
        if (spawnType == SpawnType.Random && GameObject.FindGameObjectsWithTag("Item").Length < maxSpawn) doneSpawn = false;
        if (!doneSpawn)
        {
            if (timer <= 0)
            {
                Transform tr1 = new GameObject().transform;
                Transform tr2 = new GameObject().transform;
                tr1.position = spawnPoint.position;
                tr2.position = spawnPoint.position;
                tr1.position = new Vector3(tr1.position.x + Random.Range(-4f, 4f), tr1.position.y, tr1.position.z + Random.Range(-4f, 4f));
                tr2.position = new Vector3(tr2.position.x + Random.Range(-4f, 4f), tr2.position.y, tr2.position.z + Random.Range(-4f, 4f));
                int objtoSpawn = Random.Range(0, objs.Length);
                SpawnObject(objs[objtoSpawn], tr1);
                if (spawnType == SpawnType.Pairs) SpawnObject(objs[objtoSpawn], tr2);
                
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
        obj = Instantiate(obj, spawnPoint.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        if (obj.GetComponent<Rigidbody>() == null) obj.AddComponent<Rigidbody>();
        obj.tag = "Item";
        obj.layer = 6;
        obj.transform.localScale = new Vector3(itemSpawnSize, itemSpawnSize, itemSpawnSize);
        bool isHold = false;
        Variables.Object(obj).Set("isHold", isHold);
        Variables.Object(obj).Set("isMathing", false);
    }

    public void RespwanItems(int count)
    {
        for (int i = 0; i < (spawnType == SpawnType.Pairs ? count / 2 : count); i++)
        {
            Transform tr1 = new GameObject().transform;
            Transform tr2 = new GameObject().transform;
            tr1.position = spawnPoint.position;
            tr2.position = spawnPoint.position;
            tr1.position = new Vector3(tr1.position.x + Random.Range(-4f, 4f), tr1.position.y, tr1.position.z + Random.Range(-4f, 4f));
            tr2.position = new Vector3(tr2.position.x + Random.Range(-4f, 4f), tr2.position.y, tr2.position.z + Random.Range(-4f, 4f));
            int objtoSpawn = Random.Range(0, objs.Length);
            SpawnObject(objs[objtoSpawn], tr1);
            if (spawnType == SpawnType.Pairs) SpawnObject(objs[objtoSpawn], tr2);

        }
    }
}
