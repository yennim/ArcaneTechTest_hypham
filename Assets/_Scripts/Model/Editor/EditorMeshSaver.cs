#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class MeshSaver
{
    public static void SaveMeshAsset(Mesh mesh, string meshName, string folderPath = "Assets/GeneratedMeshes/")
    {
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            string[] folders = folderPath.Split('/');
            string currentPath = "Assets";

            for (int i = 1; i < folders.Length - 1; i++)
            {
                if (!AssetDatabase.IsValidFolder(currentPath + "/" + folders[i]))
                {
                    AssetDatabase.CreateFolder(currentPath, folders[i]);
                }
                currentPath += "/" + folders[i];
            }
        }

        string fullPath = folderPath + meshName + ".asset";

        AssetDatabase.DeleteAsset(fullPath);

        AssetDatabase.CreateAsset(mesh, fullPath);
        AssetDatabase.SaveAssets();

        Debug.Log($"Mesh '{meshName}' saved at : {fullPath}");
    }
}
#endif