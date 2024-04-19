using System;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IdoFolk_Essentials_Editor.CustomWindows
{
    public enum SceneType
    {
        Production,
        Test
    }

    public class SceneEditor : OdinMenuEditorWindow
    {
        public const string PRODUCTION_PATH = "Assets/Scenes/Production";
        public const string TEST_PATH = "Assets/Scenes/Testing";
        public const string START_SCENE_NAME = "PersistentScene";

        [MenuItem("Tools/Scenes")]
        private static void OpenWindow()
        {
            SceneEditor window = GetWindow<SceneEditor>();
            CreateFolder(PRODUCTION_PATH);
            CreateFolder(TEST_PATH);
            NewSceneClass.CreateScene(PRODUCTION_PATH,START_SCENE_NAME);
            window.Show();
        }

        private static void CreateFolder(string path)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                var folderPath = path.Substring(0,path.LastIndexOf('/'));
                var folderName = path.Substring(path.LastIndexOf('/')+1);
                AssetDatabase.CreateFolder(folderPath,folderName);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SceneEditorCache.OnSceneDeleted += ForceMenuTreeRebuild;
        }

        protected override void OnDestroy()
        {
            SceneEditorCache.OnSceneDeleted -= ForceMenuTreeRebuild;
            base.OnDestroy();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.DefaultMenuStyle.Offset = 1f;

            var result = AssetDatabase.FindAssets("t:SceneAsset", new string[] { PRODUCTION_PATH });
            SceneEditorCache[] productionScenes = new SceneEditorCache[result.Length];
            tree.Add("  New Scene", new NewSceneClass());
            for (int i = 0; i < result.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(result[i]);
                var currentScene = AssetDatabase.LoadAssetAtPath(path, typeof(SceneAsset)) as SceneAsset;
                productionScenes[i] = new SceneEditorCache(true, currentScene);
                // Debug.Log("Path " + AssetDatabase.GUIDToAssetPath(guid));
                tree.Add("Production/" + currentScene.name, productionScenes[i]);
            }

            result = AssetDatabase.FindAssets("t:SceneAsset", new string[] { TEST_PATH });
            productionScenes = new SceneEditorCache[result.Length];
            for (int i = 0; i < result.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(result[i]);
                var currentScene = AssetDatabase.LoadAssetAtPath(path, typeof(SceneAsset)) as SceneAsset;
                productionScenes[i] = new SceneEditorCache(true, currentScene);
                // Debug.Log("Path " + AssetDatabase.GUIDToAssetPath(guid));
                tree.Add("Tests/" + currentScene.name, productionScenes[i]);
            }
            //    tree.AddAllAssetsAtPath("Test Scenes", "Assets/Scenes/Test Scenes", typeof(SceneAsset));

            return tree;
        }

        public static string GetPath(SceneType path)
        {
            switch (path)
            {
                case SceneType.Production:
                    return PRODUCTION_PATH;
                case SceneType.Test:
                    return TEST_PATH;
            }

            throw new Exception();
        }
        
        public class NewSceneClass
        {
            [SerializeField] private string _name;
            [SerializeField] private SceneType _path = SceneType.Test;

            [Sirenix.OdinInspector.Button]
            private void CreateNewScene()
            {
                CreateScene(GetPath(_path), _name);
            }

            public static void CreateScene(string scenePath, string sceneName)
            {
                if (sceneName.Length == 0)
                    return;
                var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
                if (!AssetDatabase.IsValidFolder(scenePath))
                {
                    var folderPath = scenePath.Substring(0,scenePath.LastIndexOf('/'));
                    var folderName = scenePath.Substring(scenePath.LastIndexOf('/')+1);
                    AssetDatabase.CreateFolder(folderPath,folderName);
                }
                EditorSceneManager.SaveScene(scene, scenePath + $"\\{sceneName}.unity");
            }
        }

        public class SceneEditorCache
        {
            public static event Action OnSceneDeleted;
            private bool _canBeDeleted;
            private SceneAsset _current;
            private List<Scene> _scenes = new List<Scene>();

            public SceneEditorCache(bool canBeDeleted, SceneAsset scene)
            {
                _current = scene;
                _canBeDeleted = canBeDeleted;
            }

            [Sirenix.OdinInspector.Button]
            public void LoadSingle()
            {
                Debug.Log($"Loading {_current.name} Scene");
                Reset();
                EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(_current), OpenSceneMode.Single);
            }

            [Sirenix.OdinInspector.Button]
            public void LoadAdditive()
            {
                _scenes.Add(EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(_current), OpenSceneMode.Additive));
                Debug.Log($"Loading {_current.name} Scene Additive ");
            }

            [Sirenix.OdinInspector.Button]
            public void Unload()
            {
                for (int i = 0; i < _scenes.Count; i++)
                    EditorSceneManager.CloseScene(_scenes[i], false);
                Reset();
                Debug.Log($"Unloading {_current.name} Scenes");
            }

            // TODO not working currently
            // [Sirenix.OdinInspector.Button]
            // public void Delete()
            // {
            //     if (_canBeDeleted == false)
            //     {
            //         Debug.LogError("Scene Cannot be deleted from editor window!");
            //         return;
            //     }
            //
            //     var path = AssetDatabase.GetAssetPath(_current);
            //
            //     if (Directory.Exists(path))
            //     {
            //         Directory.Delete(path);
            //         OnSceneDeleted?.Invoke();
            //     }
            //
            //     Debug.Log("Scene Deleted");
            // }

            private void Reset() => _scenes.Clear();
        }
    }
}