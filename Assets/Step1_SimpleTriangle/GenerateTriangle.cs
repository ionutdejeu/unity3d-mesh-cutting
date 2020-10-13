using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class GenerateTriangle : MonoBehaviour
{
    Mesh m;
    Vector3[] verts;
    int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<MeshFilter>().mesh;
        verts = new Vector3[] {
            new Vector3(0,0,0),
            new Vector3(0,0,10),
            new Vector3(10,0,0)
        };
        triangles = new int[] { 0, 1, 2 };
        m.vertices = verts;
        m.triangles = triangles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        foreach(Vector3 vert in verts)
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(vert, 1);
        }
    }
}
