using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTriangle 
{
    List<Vector3> verices = new List<Vector3>();
    List<Vector3> normals = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();
    int submeshIndex;

    public List<Vector3> Verices { get => verices; set => verices = value; }
    public List<Vector3> Normals { get => normals; set => normals = value; }
    public List<Vector2> Uvs { get => uvs; set => uvs = value; }
    public int SubmeshIndex { get => submeshIndex; set => submeshIndex = value; }

 
    public MeshTriangle(Vector3[] _verts, Vector3[] _normals,Vector2[] _uvs,int _submes_index)
    {
        Clear();
        verices.AddRange(_verts);
        normals.AddRange(_normals);
        uvs.AddRange(_uvs);
    }

    public void AddPoint(Vector3 point, Vector3 normal,Vector2 uv)
    {
        this.verices.Add(point);
        this.normals.Add(normal);
        this.uvs.Add(uv);

    }
    public void Clear()
    {
        verices.Clear();
        normals.Clear();
        uvs.Clear();
        submeshIndex = 0;
    }
}
