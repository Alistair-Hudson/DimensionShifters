using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ExportAssetBundles
{
    [MenuItem("Assets/Build AssetBundle")]
    static void ExportResource()
    {
        string folderName = "AssetBundles";
        string filePath = Path.Combine(Application.streamingAssetsPath, folderName);

        //Uncomment to build for platforms
        //BuildPipeline.BuildAssetBundles(filePath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        //BuildPipeline.BuildAssetBundles(filePath, BuildAssetBundleOptions.None, BuildTarget.iOS);
        BuildPipeline.BuildAssetBundles(filePath, BuildAssetBundleOptions.None, BuildTarget.Android);
        //BuildPipeline.BuildAssetBundles(filePath, BuildAssetBundleOptions.None, BuildTarget.WebGL);
        //BuildPipeline.BuildAssetBundles(filePath, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);

        //Refresh the Project folder
        AssetDatabase.Refresh();
    }
}
