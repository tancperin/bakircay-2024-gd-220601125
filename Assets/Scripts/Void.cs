using UnityEngine;

public class Void : MonoBehaviour
{
    public Transform RespawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("Item"))
         if (RespawnPoint != null)
         {
            other.transform.position = RespawnPoint.position;
         }
         else 
         {
            Destroy(other.gameObject);
         }
    }
}
