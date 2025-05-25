using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using SerializableData.Persistent;
using Sirenix.OdinInspector;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Builder")]
public class BuildPipeline : ScriptableObject
{
    private const string CharacterExtension = "xcharacter";
    private const string WorldExtension = "xworld";
    
    [BoxGroup("Project")] [SerializeField] private string projectName;
    [BoxGroup("Project")] [SerializeField] private string projectDescription;
    [BoxGroup("Project")] [SerializeField] private Texture2D icon;
    
    [BoxGroup("Target")] [SerializeField] private SceneDataConfig asset;
    [BoxGroup("Target")] [SerializeField] private List<BuildTarget> targets;
    
    [Button]
    private void Build()
    {
        var isCharacter = asset is CharacterDataConfig;
        var extension = isCharacter ? CharacterExtension : WorldExtension;

        var result = EditorUtility.SaveFilePanel("Build", string.Empty, "build", extension);
        var buildResults = new List<(string, BuildTarget)>();

        foreach (var target in targets)
        {
            buildResults.Add((BuildForTarget(result, target), target));
        }
        
        if(File.Exists(result))
            File.Delete(result);

        var buildPath = Path.GetDirectoryName(result);
        
        
        using (var archive = ZipFile.Open(result, ZipArchiveMode.Create))
        {
            foreach (var buildResult in buildResults)
            {
                archive.CreateEntryFromFile(buildResult.Item1, TargetToName(buildResult.Item2));
                Directory.Delete(Path.GetDirectoryName(buildResult.Item1), true);
            }
            
            CreateIcon(archive, buildPath, icon);
            
            var jobj = new JObject();
            jobj["name"] = projectName;
            jobj["description"] = projectDescription;

            var manifestJson = JsonConvert.SerializeObject(jobj);
            var manifestPath = Path.Combine(buildPath, "manifest");

            using (var manifestStream = new FileStream(manifestPath, FileMode.OpenOrCreate))
            {
                var bytes = Encoding.UTF8.GetBytes(manifestJson);
                manifestStream.Write(bytes, 0, bytes.Length);
            }
            
            archive.CreateEntryFromFile(manifestPath, "manifest");
            File.Delete(manifestPath);
        }
    }

    private void CreateIcon(ZipArchive archive, string tempDirectory, Texture2D img)
    {
        if(!img)
            return;
        
        var iconPath = Path.Combine(tempDirectory, "icon");
        
        using (var iconStream = new FileStream(iconPath, FileMode.Create))
        {
            var iconBytes = File.ReadAllBytes(AssetDatabase.GetAssetPath(icon));
            iconStream.Write(iconBytes, 0, iconBytes.Length);
        }
        
        archive.CreateEntryFromFile(iconPath, "icon");
        File.Delete(iconPath);
    }

    private string BuildForTarget(string path, BuildTarget target)
    {
        var bundlePath = AssetDatabase.GetAssetPath(asset);
        var bundleName = AssetDatabase.GetImplicitAssetBundleName(bundlePath);
        var bundlePaths = AssetDatabase.GetAssetPathsFromAssetBundle(bundleName);
        
        var build = new AssetBundleBuild
        {
            assetBundleName = bundleName,
            assetNames = bundlePaths
        };
        
        var sourcePath = Path.GetDirectoryName(path);
        var targetPath = Path.Combine(sourcePath, target.ToString());
        
        if(!Directory.Exists(targetPath))
            Directory.CreateDirectory(targetPath);
        
        AssetBundleBuild[] builds = { build };

        try
        {
            UnityEditor.BuildPipeline.BuildAssetBundles(targetPath, builds, BuildAssetBundleOptions.None, target);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        return Path.Combine(targetPath, bundleName);
    }

    private string TargetToName(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.StandaloneWindows:
                return "windows";
            case BuildTarget.StandaloneOSX:
                return "osx";
            case BuildTarget.iOS:
                return "ios";
            case BuildTarget.Android:
                return "android";
            default:
                return "unknown";
        }
    }
}
