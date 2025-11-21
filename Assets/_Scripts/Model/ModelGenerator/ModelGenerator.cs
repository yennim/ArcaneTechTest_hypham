using UnityEngine;

/// <summary>
/// Used from editor Tool
/// </summary>
public static class ModelGenerator
{
    public static GameObject CreatePyramid()
    {
        GameObject pyramid = new GameObject("Pyramid");

        MeshFilter mf = pyramid.AddComponent<MeshFilter>();
        MeshRenderer mr = pyramid.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        GeneratePyramidMesh(mesh);

        mr.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        return pyramid;
    }

    private static void GeneratePyramidMesh(Mesh mesh)
    {
        // TODO: make the edges harsher ?

        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 1, 0), // top
            new Vector3(1, -1, 1), // base corners all at y -1
            new Vector3(-1, -1, 1),
            new Vector3(-1, -1, -1),
            new Vector3(1, -1, -1)
        };

        mesh.vertices = vertices;

        int[] triangles = new int[]
        {
            // triangles towards the top
            0, 2, 1,
            0, 3, 2,
            0, 4, 3, 
            0, 1, 4,
            
            // bottom square, need this order to show face
            1, 3, 4,
            1, 2, 3
        };

        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
