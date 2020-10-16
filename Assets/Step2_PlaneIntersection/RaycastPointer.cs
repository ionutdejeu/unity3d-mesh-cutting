using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is a simple script that displays a red shpere at the intersection of plane and cursor
/// </summary>
public class RaycastPointer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector3? pointer_pos_intersection;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //StartCoroutine(ScaleMe(hit.transform));
                this.pointer_pos_intersection = hit.point;
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
            }
            else
            {
                pointer_pos_intersection = null;
            }
        }
    }

    IEnumerator ScaleMe(Transform objTr)
    {
        objTr.localScale *= 1.2f;
        yield return new WaitForSeconds(0.5f);
        objTr.localScale /= 1.2f;
    }

    private void OnDrawGizmos()
    {
        if (pointer_pos_intersection.HasValue)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pointer_pos_intersection.Value, 1f);
        }
    }
}
