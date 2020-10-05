using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedMesh : MonoBehaviour
{
    List<Vector3> vertices = new List<Vector3>();
    List<Vector3> normals= new List<Vector3>();
    List<Vector3> uvs = new List<Vector3>();
    List<List<int>> submeshIndices = new List<List<int>>();

    public List<Vector3> Vertices { get => vertices; set => vertices = value; }
    public List<Vector3> Normals { get => normals; set => normals = value; }
    public List<Vector3> Uvs { get => uvs; set => uvs = value; }
    public List<List<int>> SubmeshIndices { get => submeshIndices; set => submeshIndices = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTriangleToMesh(MeshTriangle _triangle)
    {
        int curr_verts_count = vertices.Count;
        vertices.AddRange(_triangle.Verices);
        normals.AddRange(_triangle.Normals);
        uvs.AddRange(_triangle.Normals);

        if (submeshIndices.Count < _triangle.SubmeshIndex + 1)
        {
            for (int i = submeshIndices.Count; i < _triangle.SubmeshIndex + 1; i++)
            {
                submeshIndices.Add(new List<int>());
            }
        }
        
        for (int i = 0; i < 3; i++)
        {
            submeshIndices[_triangle.SubmeshIndex].Add(curr_verts_count + i);
        }
    }

}
