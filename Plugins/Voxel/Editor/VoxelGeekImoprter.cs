using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;
using GDGeek;
using System.IO;

namespace GDGeek
{
    [ScriptedImporter(4, new[] { "vox" }, importQueueOffset: 4000, AllowCaching = false)]
    public class VoxelGeekImoprter : ScriptedImporter
    {
        private Material _material = null;

        void init()
        {
#if UNITY_EDITOR

           

            if (_material == null)
            {

                _material = UnityEditor.AssetDatabase.LoadAssetAtPath<Material>("Assets/7DGame/Media/Materials/VoxelMat.mat");

            }
            _material.SetVector("_Tiling", new Vector2(1f / 0.005f, 1f /  0.005f));
#endif
        }

        public override void OnImportAsset(AssetImportContext ctx)
        {
            init();
            // 打开文件流来读取文件内容
            using (var stream = new FileStream(ConvertProjectPathToSystemPath(ctx.assetPath), FileMode.Open))
            {
                string assetName = Path.GetFileNameWithoutExtension(ctx.assetPath);

                var vs = MagicaVoxelFormater.ReadFormStream(stream).vs;
                VoxelGeometry.MeshData data = VoxelBuilder.Struct2Data(vs,0.005f);
                Mesh mesh = VoxelBuilder.Data2Mesh(data);
                MeshFilter filter = VoxelBuilder.Mesh2Filter(mesh);
                MeshRenderer render = VoxelBuilder.FilterAddRenderer(filter, this._material);
                GameObject asset = filter.gameObject;

                mesh.name = assetName;
                asset.name = assetName;

                ctx.AddObjectToAsset(assetName + "_mesh", mesh);
                ctx.AddObjectToAsset(assetName + "_object", asset);
                ctx.SetMainObject(asset);
            }

        }

        // 将项目路径转换为系统路径的辅助方法
        protected static string ConvertProjectPathToSystemPath(string projectPath)
        {
            var dataPath = UnityEngine.Application.dataPath;
            var projectFolder = dataPath.Substring(0, dataPath.Length - "Assets".Length);
            return Path.Combine(projectFolder, projectPath);
        }
    }

}