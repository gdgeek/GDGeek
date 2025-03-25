using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GDGeek
{
   public class VoxelMeshBuild  
    {  
        public class Vector3Quad  
        {  
            private Vector3[] vectors = new Vector3[4];  
            public Vector3 offset { get; set; }  
            public Vector3Int location { get; set; } = Vector3Int.zero;  
            public float size { private get; set; } = 0.005f;  
            public Quaternion rotation { get; set; } = Quaternion.identity;  

            public Vector3Quad(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)  
            {  
                vectors[0] = v0;  
                vectors[1] = v1;  
                vectors[2] = v2;  
                vectors[3] = v3;  
            }  

            public Vector3 this[int index] => rotation * (vectors[index] * size) + offset * size;  

            public Vector3Int normal  
            {  
                get  
                {  
                    Vector3 normalized = Vector3.Cross(this[2] - this[0], this[3] - this[0]).normalized;  
                    return new Vector3Int(  
                        Mathf.RoundToInt(normalized.x),  
                        Mathf.RoundToInt(normalized.y),  
                        Mathf.RoundToInt(normalized.z)  
                    );  
                }  
            }  
        }  

        private void addVertix(List<VoxelVertex> vertices, Vector3 position, Color32 c, Vector3Int normal, Vector2 uv1)  
        {  
            vertices.Add(new VoxelVertex { position = position, normal = normal, color = c, uv = uv1 });  
        }  

        private Vector3Quad getPoints(Quaternion rotation, Vector3Int location, float size, Vector3 offset)  
        {  
            return new Vector3Quad(  
                new Vector3(0.5f, -0.5f, 0.5f),  
                new Vector3(-0.5f, -0.5f, 0.5f),  
                new Vector3(0.5f, 0.5f, 0.5f),  
                new Vector3(-0.5f, 0.5f, 0.5f))  
            {  
                rotation = rotation,  
                location = location,  
                size = size,  
                offset = location + offset  
            };  
        }  

        private void addRect(List<VoxelVertex> vertices, List<int> triangles, Vector3Quad quad, Color32 color)  
        {  
            int vertexCount = vertices.Count;  
            triangles.AddRange(new int[] { vertexCount, vertexCount + 2, vertexCount + 3, vertexCount, vertexCount + 3, vertexCount + 1 });  

            for (int i = 0; i < 4; i++)  
            {  
                addVertix(vertices, quad[i], color, quad.normal, new Vector2(i % 2, i / 2));  
            }  
        }  

        private void build(VoxelProduct.Product main, Dictionary<Vector3Int, VoxelHandler> voxs, Dictionary<Vector3Int, VoxelHandler> all, float size, Vector3 offset)  
        {  
            List<Vector3Int> keys = new List<Vector3Int>(voxs.Keys);  

            // 创建共享的线程安全的并发集合  
            var vertices = new List<VoxelVertex>();  
            var triangles = new List<int>();  

            object lockObj = new object(); // 用于同步  

            Parallel.For(0, keys.Count, i =>  
            {  
                var localVertices = new List<VoxelVertex>();  
                var localTriangles = new List<int>();  

                Vector3Int key = keys[i];  
                VoxelHandler voxel = voxs[key];  
                Color32 color = voxel.color;  

                // 检查每个方向是否需要添加面  
                if (!all.ContainsKey(key + Vector3Int.forward))  
                {  
                    var quad = getPoints(Quaternion.identity, key, size, offset);  
                    addRect(localVertices, localTriangles, quad, color);  
                }  
                if (!all.ContainsKey(key + Vector3Int.up))  
                {  
                    var quad = getPoints(Quaternion.AngleAxis(-90f, Vector3.right), key, size, offset);  
                    addRect(localVertices, localTriangles, quad, color);  
                }  
                if (!all.ContainsKey(key + Vector3Int.back))  
                {  
                    var quad = getPoints(Quaternion.AngleAxis(-180f, Vector3.right), key, size, offset);  
                    addRect(localVertices, localTriangles, quad, color);  
                }  
                if (!all.ContainsKey(key + Vector3Int.down))  
                {  
                    var quad = getPoints(Quaternion.AngleAxis(-270f, Vector3.right), key, size, offset);  
                    addRect(localVertices, localTriangles, quad, color);  
                }  
                if (!all.ContainsKey(key + Vector3Int.left))  
                {  
                    var quad = getPoints(Quaternion.AngleAxis(-90f, Vector3.up), key, size, offset);  
                    addRect(localVertices, localTriangles, quad, color);  
                }  
                if (!all.ContainsKey(key + Vector3Int.right))  
                {  
                    var quad = getPoints(Quaternion.AngleAxis(90f, Vector3.up), key, size, offset);  
                    addRect(localVertices, localTriangles, quad, color);  
                }  

                // 将本地结果合并到全局列表中，需要线程同步  
                lock (lockObj)  
                {  
                    int vertexOffset = vertices.Count;  
                    vertices.AddRange(localVertices);  
                    foreach (var tri in localTriangles)  
                    {  
                        triangles.Add(tri + vertexOffset);  
                    }  
                }  
            });  

            main.draw.vertices = vertices;  
            main.draw.triangles = triangles;  
        }  

        public void build(VoxelProduct product)  
        {  
            foreach (var prod in product.products)  
            {  
                build(prod, product.main.voxels, product.size, product.offset);  
            }  
        }  

        public void build(VoxelProduct.Product main, Dictionary<Vector3Int, VoxelHandler> all, float size, Vector3 offset)  
        {  
            main.draw = new VoxelDrawData();  
            build(main, main.voxels, all, size, offset);  
        }  
    }  
}