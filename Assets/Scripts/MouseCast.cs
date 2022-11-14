using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCast : MonoBehaviour
{
    public Camera cam;
    Vector3 center;
    public float amount;
    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
             if (hitInfo.collider.gameObject.GetComponent<Interactor>() != null)
                { 
                    GameObject interactor = hitInfo.collider.gameObject;
                    Vector3 direction = (interactor.transform.position - center).normalized;
                    //scale
                    if (direction == transform.up.normalized || -direction == transform.up.normalized)
                    {
                       transform.localScale += new Vector3(0f, amount, 0f);
                    }
                    if (direction == transform.forward.normalized || -direction == transform.forward.normalized)
                    {
                        transform.localScale += new Vector3(0f, 0f, amount);
                    }
                    if (direction == transform.right.normalized || -direction == transform.right.normalized)
                    {
                        transform.localScale += new Vector3(amount, 0f, 0f);
                    }
                    //position
                    transform.localPosition += direction * amount /2;
                }
            }
        }
       
    }
}
