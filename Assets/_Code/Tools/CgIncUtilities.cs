using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class CgIncUtilities 
{

    [MenuItem("Assets/Create/Shader/CgInc")]
    public static void Create () 
    {
        string path = GetTargetPath(Selection.activeObject);
        string name = "NewCginc";
        string ext = ".cginc";
        string fullPath = AssetDatabase.GenerateUniqueAssetPath(path + name + ext);
        string content = "" +
                         "#ifndef NEW_CGINC_INCLUDED \n" +
                         "#define NEW_CGINC_INCLUDED \n" +
                         " \n" + "// TODO : " + "Amazing CG \n" + " \n" +
                         "#endif";

        File.WriteAllText(fullPath, content);
        AssetDatabase.ImportAsset(fullPath, ImportAssetOptions.ForceUpdate);

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<TextAsset>(fullPath);
    }

    static string GetTargetPath(Object obj)
    {
        string targetPath = "Assets/";
        if (obj == null) return targetPath;

        string objPath = AssetDatabase.GetAssetPath(obj);
        if (string.IsNullOrEmpty(objPath)) return targetPath;

        if (AssetDatabase.IsValidFolder(objPath)) targetPath = objPath + "/";
        else
        {
            targetPath = "";
            string[] splitPath = objPath.Split(new string[1]{"/"}, System.StringSplitOptions.None);
            for (int i=0; i<splitPath.Length-1; i++) targetPath += splitPath[i] + "/";
        }

        return targetPath;
    }
	
}