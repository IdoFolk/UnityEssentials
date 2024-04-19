using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace IdoFolk_Essentials_Editor.CustomWindows
{
    public class ConfigWindowEditor : OdinMenuEditorWindow
    {

        [MenuItem("Tools/ConfigEditor")]
        private static void OpenWindow()
        {
            GetWindow<ConfigWindowEditor>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            //tree.AddAllAssetsAtPath
            return tree;
        }

        protected override void OnBeginDrawEditors()
        {
            var selected = this.MenuTree.Selection;

            SirenixEditorGUI.BeginHorizontalToolbar();
            {
                GUILayout.FlexibleSpace();

                if (SirenixEditorGUI.ToolbarButton("Delete"))
                {
                    var asset = selected.SelectedValue;
                    string path = AssetDatabase.GetAssetPath((Object)asset);
                    AssetDatabase.DeleteAsset(path);
                    AssetDatabase.SaveAssets();
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }



        #region WIP_CreatingConfigFiles

        protected override void OnDestroy()
        {
            base.OnDestroy();
            //_createNewConfigDatas.OnDestroy();
        }

        // public class CreateNewConfigDatas
        // {
        //
        //     private void SetConfigData(BaseConfig configData)
        //     {
        //         if(ConfigData != null) 
        //             DestroyImmediate(ConfigData);
        //
        //         ConfigData = configData;
        //     }
        //
        //     [HorizontalGroup]
        //     [Button("Enemy")]
        //     private void SetEnemyConfig()
        //     {
        //         var newConfigData = new CreateNewConfigData<EnemyConfig>("NewEnemyConfig", "Assets/Configs/UnitConfigs/EnemyConfigs");
        //         SetConfigData(newConfigData.CofigData);
        //     }
        //     [HorizontalGroup]
        //     [Button("Shaman")]
        //     private void SetShamanConfig()
        //     {
        //         var newConfigData = new CreateNewConfigData<ShamanConfig>("NewShamanConfig", "Assets/Configs/UnitConfigs/ShamanConfigs");
        //         SetConfigData(newConfigData.CofigData);
        //     }
        //     
        //     [InlineEditor(objectFieldMode: InlineEditorObjectFieldModes.CompletelyHidden)]
        //     public BaseConfig ConfigData;
        //     public void OnDestroy()
        //     {
        //         if(ConfigData != null)
        //             DestroyImmediate(ConfigData);
        //     }
        // }
        // public class CreateNewConfigData<T> where T : BaseConfig
        // {
        //     private readonly string _configPath;
        //     private readonly string _configName;
        //     
        //     public CreateNewConfigData(string name,string path)
        //     {
        //         CofigData = ScriptableObject.CreateInstance<T>();
        //         _configPath = path;
        //         _configName = name;
        //         CofigData.Name = name;
        //     }
        //     
        //     public T CofigData;
        //
        //     [Button("Add New Config")]
        //     private void CreateNewData()
        //     {
        //         AssetDatabase.CreateAsset(CofigData,_configPath + "/" + CofigData.Name + ".asset");
        //         AssetDatabase.SaveAssets();
        //         
        //         
        //         CofigData = ScriptableObject.CreateInstance<T>();
        //         CofigData.Name = _configName;
        //     }
        // }

        #endregion
    }
}