using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPoints : MonoBehaviour
{
    float count = 0;
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero; 
    public GameObject[] Interaction = new GameObject[6];
    public Camera cam;
    Vector3 Intialposition;
    float Ax = 2;
    public GameObject Interact;
    int MouseDown = 0;

    // Interactor Objects position calculators
    public Vector3[] Axis()
    {
        Vector3[] dir = { Intialposition + transform.up * Ax, Intialposition + transform.right * Ax, Intialposition + transform.forward * Ax};
        return dir;
    }
    public Vector3[] Axis2()
    {
        Vector3[] dir = { Intialposition - transform.up * Ax, Intialposition - transform.right * Ax, Intialposition - transform.forward * Ax };
        return dir;
    }
    public Vector3 zero()
    {
        return Intialposition;
    }



    void Start()
    {
        Intialposition = transform.position;
    }

    void Update()
    {
        // Interactor Objects position setup
        Interaction[0].transform.position = Axis()[0];
        Interaction[1].transform.position = Axis()[1];
        Interaction[2].transform.position = Axis()[2];
        Interaction[3].transform.position = Axis2()[0];
        Interaction[4].transform.position = Axis2()[1];
        Interaction[5].transform.position = Axis2()[2];



        // Zoom out + Interactor's distance augmentation
        if (count == 50)
        {
            cam.gameObject.transform.position -= new Vector3(0f, 0f, 3f);
            Ax +=1;
            Interact.gameObject.transform.localScale += new Vector3(0.5f, 0.5f,0.5f);
            count = 0;
        }

        // Hold 
        if (MouseDown == 1)
        {
            DragRotation();
            mPrevPos = Input.mousePosition;
        }
        if (!Input.GetMouseButton(0))
        {
            MouseDown = 0;
        }


        // MouseClick 
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject.GetComponent<InteractionPoints>() != null)
                {
                    DragRotation();    
                }
                // Zoom out count up
                if (hitInfo.collider.gameObject.GetComponent<Interactor>() != null)
                {
                 count += 1;
                }
            }
        }
        mPrevPos = Input.mousePosition; 
    }


    // Drag Rotation Function
    void DragRotation()
    {
        MouseDown = 1;
        mPosDelta = Input.mousePosition - mPrevPos;
        if (Vector3.Dot(transform.up, Vector3.up) >= 0)
            transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, cam.transform.right), Space.World);
        else
            transform.Rotate(transform.up, Vector3.Dot(mPosDelta, cam.transform.right), Space.World);
        transform.Rotate(cam.transform.right, Vector3.Dot(mPosDelta, cam.transform.up), Space.World);
    }
} 
