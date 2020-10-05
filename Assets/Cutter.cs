using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter  
{
    public static bool currentlyCutting;
    public static Mesh originalMesh;

    public static void Cut(GameObject _orgGO, 
        Vector3 _contantPoint,
        Vector3 _dir, 
        Material _cut_mat = null,
        bool fill=true,
        bool _addRBod = true)
    {
        if (currentlyCutting) return;
        currentlyCutting = true;
        Plane p = new Plane(_orgGO.transform.InverseTransformDirection(-_dir),
            _orgGO.transform.InverseTransformPoint(_contantPoint));
        originalMesh = _orgGO.GetComponent<MeshFilter>().mesh;
        var addVertices = new List<Vector3>();
        var leftMesh = new GeneratedMesh();
        var rightMesh = new GeneratedMesh();

        int[] submeshIndices;
        int triangleIndexA, triangleIndexB, triangleIndexC;
        for(int i = 0;i< originalMesh.subMeshCount; i++)
        {
            submeshIndices = originalMesh.GetTriangles(i);
            for(int j = 0; j< submeshIndices.Length; j+=3)
            {
                triangleIndexA = submeshIndices[j];
                triangleIndexB = submeshIndices[j + 1];
                triangleIndexC = submeshIndices[j + 2];

                //MeshTriangle currentTriagnle = new MeshTriangle()

            }
        }
    }

    public static MeshTriangle GetTriangle(int tIndexA,int tIndexB, int tIndexC,int subMindex)
    {
        return null;
    }

}
