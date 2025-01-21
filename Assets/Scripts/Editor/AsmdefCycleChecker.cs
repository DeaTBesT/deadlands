using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DL.Editor
{
    public class AsmdefCycleChecker
    {
        private static Dictionary<string, string[]> dependencies = new Dictionary<string, string[]>();
        private static Dictionary<string, string> guidToName = new Dictionary<string, string>();
    
        [MenuItem("Tools/Check ASMDEF Cycles")]
        public static void CheckCycles()
        {
            dependencies.Clear();
            guidToName.Clear();
            string[] asmdefFiles = Directory.GetFiles("Assets", "*.asmdef", SearchOption.AllDirectories);
        
            foreach (string file in asmdefFiles)
            {
                string json = File.ReadAllText(file);
                AsmdefData asmdef = JsonUtility.FromJson<AsmdefData>(json);
                dependencies[asmdef.name] = asmdef.references ?? new string[0];
                guidToName[AssetDatabase.AssetPathToGUID(file)] = asmdef.name;
            }
        
            foreach (string asm in dependencies.Keys)
            {
                HashSet<string> visited = new HashSet<string>();
                if (HasCycle(asm, visited))
                {
                    Debug.LogError($"Циклическая зависимость обнаружена в {asm}");
                }
            }
        }
    
        private static bool HasCycle(string asm, HashSet<string> visited)
        {
            if (visited.Contains(asm)) return true;
            if (!dependencies.ContainsKey(asm)) return false;
        
            visited.Add(asm);
        
            foreach (string dep in dependencies[asm])
            {
                string depName = dep.StartsWith("GUID:") ? guidToName.GetValueOrDefault(dep.Substring(5), dep) : dep;
                if (HasCycle(depName, new HashSet<string>(visited))) return true;
            }
        
            return false;
        }
    
        [Serializable]
        private class AsmdefData
        {
            public string name;
            public string[] references;
        }
    }
}