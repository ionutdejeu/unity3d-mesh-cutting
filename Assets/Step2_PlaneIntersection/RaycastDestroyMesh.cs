using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// Script that destoys the vertices that are situated within x range of the pointers position 
/// when clicked 
/// </summary>
[RequireComponent(typeof(MeshFilter))]
public class RaycastDestroyMesh : MonoBehaviour
{
    Mesh _mesh;
    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
    }
    Vector3? pointer_pos_intersection;

    // Update is called once per frame
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
                 
                update_mesh();
            }
            else
            {
                pointer_pos_intersection = null;
            }
        }
    }

    void update_mesh()
    {
        
        var (new_mesh_verts,new_mesh_triangle,new_mesh_normals) = RemoveIntersectionVertices(
            _mesh.vertices.ToList(),
            _mesh.triangles.ToList(),
            _mesh.normals.ToList(),
            pointer_pos_intersection.Value, 0.5f);
        _mesh.vertices= new_mesh_verts.ToArray();
        _mesh.triangles = new_mesh_triangle.ToArray();
        
        _mesh.RecalculateNormals();
        
    }
    (List<Vector3>, List<int>, List<Vector3>) RemoveIntersectionVertices(
        List<Vector3> meshVertices,
        List<int> triangles,
        List<Vector3> normals,
        Vector3 point,
        float r)
    {
        
        List<int> vertex_index_to_remove = new List<int>();
        Dictionary<int, int> old_new_vertex_index_mapping = new Dictionary<int, int>();
        List<Vector3> new_vertex_list = new List<Vector3>();
        

        
        for (int i = 0; i < meshVertices.Count; i++)
        {
            Vector3 v = meshVertices[i];
             
            if (Vector3.Distance(v, point) > r)
            {
                new_vertex_list.Add(v);
                old_new_vertex_index_mapping.Add(i, new_vertex_list.Count-1);
                
            }
            else
            {
                vertex_index_to_remove.Add(i); 
            }

        }
        Debug.Log($"Removed the follign {string.Join(", ",vertex_index_to_remove)}");
        List<int> new_triangle_list = new List<int>();
        List<Vector3> new_normals = new List<Vector3>();
        for (int i = 0; i < normals.Count; i++)
        {
            if (!vertex_index_to_remove.Contains(i))
            {
                new_normals.Add(normals[old_new_vertex_index_mapping[i]]);
            }
            
        }
        for (int i = 0; i < triangles.Count; i+=3)
        {
            int a = triangles[i];
            int b = triangles[i + 1];
            int c = triangles[i + 2];
            if (!vertex_index_to_remove.Contains(a) &&
                !vertex_index_to_remove.Contains(b) &&
                !vertex_index_to_remove.Contains(c))
            {
    
                new_triangle_list.Add(old_new_vertex_index_mapping[a]);
                new_triangle_list.Add(old_new_vertex_index_mapping[b]);
                new_triangle_list.Add(old_new_vertex_index_mapping[c]);
            }
            else
            {
                //Debug.Log($"Removed {a},{b},{c}");
            }
        }
         
        
        Debug.Log($"Vinitail:{meshVertices.Count} V:{new_vertex_list.Count} Tinitial:{triangles.Count} T: {new_triangle_list.Count} N:{new_normals.Count}");
        return (new_vertex_list, new_triangle_list,new_normals);
    }
    private void OnDrawGizmos()
    {
        if (pointer_pos_intersection.HasValue)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pointer_pos_intersection.Value, 0.5f);
        }
    }
}
