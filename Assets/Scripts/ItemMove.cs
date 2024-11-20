using Unity.VisualScripting;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    GameObject holdItem = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (holdItem != null)
                {
                    holdItem.transform.position = new Vector3(hit.point.x, 5, hit.point.z);
                    Variables.Object(holdItem).Set("isHold", false);
                    holdItem.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                    holdItem.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    holdItem.transform.rotation = Quaternion.identity;
                    holdItem = null;
                }
                else if (hit.transform.gameObject.tag == "Item")
                {
                    holdItem = hit.transform.gameObject;
                    Variables.Object(holdItem).Set("isHold", true);
                    holdItem.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                    holdItem.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    holdItem.transform.rotation = Quaternion.identity;
                }
            }

            if (holdItem != null)
            {
                holdItem.transform.position = new Vector3(hit.point.x, 5, hit.point.z);
            }
        }
    }
}
