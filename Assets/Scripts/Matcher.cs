using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Matcher : MonoBehaviour
{
    GameObject item;
    public float rotateSpeed = 60f;
    public float sizeChangeSpeed = 1f;
    private Vector3 originalScale;
    private Vector3 maxScale, minScale;
    private float way = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        item?.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        if (item != null)
        {
            item.transform.localScale = Vector3.Lerp(item.transform.localScale, item.transform.localScale + Vector3.one * way, sizeChangeSpeed * Time.deltaTime);
            if (item.transform.localScale.x >= maxScale.x)
            {
                way = -1f;
            }
            if (item.transform.localScale.x <= minScale.x)
            {
                way = 1f;
            }
        }

    }

    //OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (item == null && other.gameObject.CompareTag("Item"))
        {
            Debug.Log("Item entered");
            item = other.gameObject;
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            originalScale = item.transform.localScale;
            maxScale = originalScale * 1.1f;
            minScale = originalScale * 0.5f;
        }
    }

    //OnTriggerExit is called when the Collider other has stopped touching the trigger
    void OnTriggerExit(Collider other)
    {
        if (item != null && other.gameObject.CompareTag("Item"))
        {
            Debug.Log("Item exited");
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.localScale = originalScale;
            item = null;
        }
    }
}
