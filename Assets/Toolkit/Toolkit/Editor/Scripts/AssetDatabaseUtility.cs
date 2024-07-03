using System.Text;
using UnityEditor;
using UnityEngine;

namespace Ca2d.Toolkit.Editor
{
    public static class AssetDatabaseUtility
    {
        /// <summary>
        /// Check if this path contains a valid asset.
        /// </summary>
        /// <param name="path">Target path</param>
        /// <returns>True that path is an asset</returns>
        public static bool PathIsValidAsset(string path)
        {
            return AssetDatabase.IsValidFolder(path) == false &&
                   string.IsNullOrWhiteSpace(AssetDatabase.AssetPathToGUID(path)) == false;
        }

        /// <summary>
        /// Create a folder in a path if this folder has not been created.
        /// </summary>
        /// <param name="folderPath">Target folder path</param>
        /// <returns>Is this path is ready to be a folder?</returns>
        public static bool CreateFolderIfNotExist(string folderPath)
        {
            if (PathIsValidAsset(folderPath)) return false;
            if (AssetDatabase.IsValidFolder(folderPath)) return true;

            var pathVector = folderPath.Split('/');
            if (pathVector.Length == 0) return false;
            if (pathVector.Length == 1) return AssetDatabase.IsValidFolder(pathVector[0]);
            if (pathVector.Length < 2)
            {
                if (AssetDatabase.IsValidFolder($"{pathVector[0]}/{pathVector[1]}")) return true;
                return string.IsNullOrEmpty(AssetDatabase.CreateFolder(pathVector[0], pathVector[1])) == false;
            }
            
            var sb = new StringBuilder(pathVector[0]);
            var prev = pathVector[0];
            for (var i = 1; i < pathVector.Length; i++)
            {
                var tk = pathVector[i];
                if (i == pathVector.Length - 1 && tk.Contains('.')) continue;
                sb.Append('/').Append(tk);
                
                var cur = sb.ToString();
                if (AssetDatabase.IsValidFolder(cur) == false)
                {
                    if (string.IsNullOrEmpty(AssetDatabase.CreateFolder(prev, tk)))
                        return false;
                }

                prev = cur;
            }
            
            return true;
        }
        
        /// <summary>
        /// Create asset at path with everything done (include create path and create asset.).
        /// </summary>
        /// <param name="path">The folder path this file will be create to.</param>
        /// <typeparam name="T">The type of asset.</typeparam>
        /// <returns>If asset path is valid, return new asset.</returns>
        public static T CreateAssetAtPath<T>(string path) where T : ScriptableObject
        {
            if (CreateFolderIfNotExist(path) == false) return null;
            var asset = ScriptableObject.CreateInstance<T>();

            AssetDatabase.CreateAsset(asset, $"{path}");

            return asset;
        }

        /// <summary>
        /// Create asset at path with everything done (include create path and create asset.).
        /// </summary>
        /// <param name="path">The folder path this file will be create to.</param>
        /// <param name="name">The file name this asset will be.</param>
        /// <typeparam name="T">The type of asset.</typeparam>
        /// <returns>If asset path is valid, return new asset.</returns>
        public static T CreateAssetAtPath<T>(string path, string name) where T : ScriptableObject
        {
            if (CreateFolderIfNotExist(path) == false) return null;
            var asset = ScriptableObject.CreateInstance<T>();

            if (path.EndsWith('/')) AssetDatabase.CreateAsset(asset, $"{path}{name}.asset");
            else AssetDatabase.CreateAsset(asset, $"{path}/{name}.asset");

            return asset;
        }
        
        /// <summary>
        /// Trim an editor path to resource path.
        /// </summary>
        /// <param name="path">Editor path</param>
        public static void AssetPathTrimmer(ref string path)
        {
            path = path.Replace("Assets/Resources/", "");
            path = path.Replace(".asset", "");
            path = path.Replace(".prefab", "");
        }
    }
}