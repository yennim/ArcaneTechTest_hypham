#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class EditorPyramidGenerator : EditorWindow
{
    [MenuItem("Tools/Generate & Save Pyramid Prefab")]
    public static void GenerateAndSavePyramid()
    {
        GameObject generatedPyramid = ModelGenerator.CreatePyramid();
        Mesh mesh = generatedPyramid.GetComponent<MeshFilter>().sharedMesh;
        
        MeshSaver.SaveMeshAsset(mesh, "PyramidMesh", "Assets/Prefabs/Meshes/Primitives/");
        
        string prefabPath = "Assets/Prefabs/PyramidPrefab.prefab";
        PrefabUtility.SaveAsPrefabAsset(generatedPyramid, prefabPath);
        
        DestroyImmediate(generatedPyramid);
    }
}
#endif