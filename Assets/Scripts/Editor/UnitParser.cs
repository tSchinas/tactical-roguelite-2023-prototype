using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public static class UnitParser
{
    [MenuItem("PreProduction/Parse Units")]
    public static void Parse()
    {
        CreateDirectories();
        ParseStats();
        //ParseBonuses(); TODO
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static void CreateDirectories()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Units"))
            AssetDatabase.CreateFolder("Assets/Resources", "Units");
    }

    static void ParseStats()
    {
        Debug.Log("Parsing stats...");
        string readPath = string.Format("{0}/Settings/UnitStats.csv", Application.dataPath);
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i < readText.Length; ++i)
            ApplyParsedStats(readText[i]);
    }

    static void ApplyParsedStats(string line)
    {
        string[] elements = line.Split(',');
        GameObject obj = GetOrCreate(elements[0]);
        //Job job = obj.GetComponent<Job>();
        //for (int i = 1; i<Job.statOrder.Length+1;++i)
        //    job.baseStats[i - 1] = Convert.ToInt32(elements[i]);

        Debug.Log("Applying stats");

        StatModifierFeature move = GetFeature(obj, StatTypes.MOV);
        move.amount = Convert.ToInt32(elements[7]);
        Debug.Log("Applying Move stat");

        StatModifierFeature jump = GetFeature(obj, StatTypes.JMP);
        jump.amount = Convert.ToInt32(elements[8]);
        Debug.Log("Applying Jump stat");
    }

    //static void ParseBonuses()
    //{
    //    string readPath = string.Format("{0}/Settings/UnitBonuses.csv", Application.dataPath);
    //    string[] readText = File.ReadAllLines(readPath);
    //    for (int i = 1; i < readText.Length; ++i)
    //        ApplyParsedBonuses(readText[i]);
    //}

    //static void ApplyParsedBonuses(string line)
    //{
    //    string[] elements = line.Split(',');
    //    GameObject obj = GetOrCreate(elements[0]);
    //    Job job = obj.GetComponent<Job>();
    //    for(int i = 1; i<elements.Length;++i)
    //        job.growStats[i - 1] = Convert.ToSingle(elements[i]);
    //}

    static StatModifierFeature GetFeature (GameObject obj, StatTypes type)
    {
        StatModifierFeature[] smf = obj.GetComponents<StatModifierFeature>();
        for (int i = 0; i<smf.Length;++i)
        {
            if (smf[i].type == type)
                return smf[i];
        }

        StatModifierFeature feature = obj.AddComponent<StatModifierFeature>();
        feature.type = type;
        return feature;
    }
    static GameObject GetOrCreate(string jobName)
    {
        string fullPath = string.Format("Assets/Resources/Units/{0}.prefab", jobName);
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(fullPath);
        if (obj == null)
            obj = Create(fullPath);
        return obj;
    }
    static GameObject Create (string fullPath)
    {
        GameObject instance = new GameObject("TEMP");
        Debug.Log("TEMP created in UnitParser.Create()");
       // instance.AddComponent<Job>();
        GameObject prefab = PrefabUtility.CreatePrefab(fullPath, instance);
        GameObject.DestroyImmediate(instance);
        return prefab;
    }
}
