using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DL.Editor
{
    public class AsmdefCycleChecker
    {
        [MenuItem("Tools/Check ASMDEF Cycles")]
        public static void CheckCycles()
        {
            var asmdefPaths = Directory.GetFiles("Assets", "*.asmdef", SearchOption.AllDirectories);
            var dependencies = new Dictionary<string, List<string>>();

            foreach (var path in asmdefPaths)
            {
                var json = File.ReadAllText(path);
                var asmdef = JsonUtility.FromJson<AsmdefData>(json);
                dependencies[asmdef.name] = new List<string>(asmdef.references);
            }

            var visited = new HashSet<string>();
            var stack = new HashSet<string>();

            foreach (var asm in dependencies.Keys)
            {
                if (HasCycle(asm, dependencies, visited, stack))
                {
                    Debug.LogError($"Циклическая зависимость найдена! {asm}");
                }
            }
        }

        private static bool HasCycle(string asm, Dictionary<string, List<string>> dependencies, HashSet<string> visited, HashSet<string> stack)
        {
            if (stack.Contains(asm))
                return true;
            if (visited.Contains(asm))
                return false;

            visited.Add(asm);
            stack.Add(asm);

            if (dependencies.TryGetValue(asm, out var refs))
            {
                foreach (var dep in refs)
                {
                    if (HasCycle(dep, dependencies, visited, stack))
                        return true;
                }
            }

            stack.Remove(asm);
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