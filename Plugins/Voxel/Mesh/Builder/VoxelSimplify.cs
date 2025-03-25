using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  

namespace GDGeek
{
    
        
    public class VoxelSimplify
    {

        private Dictionary<Vector3Int, FacePackage> divide(VoxelProduct.Product product, float size, Vector3 offset)  
        {  
            ConcurrentDictionary<Vector3Int, FacePackage> map = new ConcurrentDictionary<Vector3Int, FacePackage>();  
            int triangleCount = product.draw.triangles.Count / 6;  

            Parallel.For(0, triangleCount, i =>  
            {  
                
                List<int> triangles = product.draw.triangles.GetRange(i * 6, 6);  
                HashSet<VoxelVertex> set = new HashSet<VoxelVertex>();  

                foreach (int item in triangles)  
                {  
                    VoxelVertex vertice = product.draw.vertices[item];  
                    set.Add(vertice);  
                }  

                FacePackage.Rectangle rectangle = new FacePackage.Rectangle(set, size, offset);  

                FacePackage facePackage = map.GetOrAdd(rectangle.normal, key => new FacePackage(key, size));  
                lock (facePackage)  
                {  
                    facePackage.add(rectangle);  
                }  
            });  

            return new Dictionary<Vector3Int, FacePackage>(map);  
        }  
        private void merge(Dictionary<Vector3Int, FacePackage> map)
        {
            
            
            Parallel.ForEach(map, item =>
            {
                item.Value.merge();
            });

        }//编程多线程

        private void build(VoxelProduct.Product product, float size, Vector3 offset)
        {
          
            Dictionary<Vector3Int, FacePackage> map = divide(product, size,  offset);
            merge(map);
            rebuild(map, product);
          
        }

        private void rebuild(Dictionary<Vector3Int, FacePackage> map, VoxelProduct.Product product)
        {
            List<VoxelVertex> vertices = new List<VoxelVertex>();
            List<int> triangles = new List<int>();
            foreach (var item in map)
            {
                FacePackage facePackage = item.Value;
                foreach (var set in facePackage.map)
                {
                    foreach (var rectangle in set.Value)
                    {

                        int offset = vertices.Count;
                        
                        triangles.Add(0 + offset);
                        triangles.Add(2 + offset);
                        triangles.Add(3 + offset);
                        triangles.Add(0 + offset);
                        triangles.Add(3 + offset);
                        triangles.Add(1 + offset);
                        
                        vertices.Add(rectangle.Value.p0);
                        vertices.Add(rectangle.Value.p1);
                        vertices.Add(rectangle.Value.p2);
                        vertices.Add(rectangle.Value.p3);
                    }
                }
            }
            product.draw.vertices = vertices;
            product.draw.triangles = triangles;
        }

        public void build(VoxelProduct product)  
        {  
            var list = product.products;  
            var offset = product.offset;  
            var size = product.size;  

            Parallel.ForEach(list, item =>  
            {  
                build(item, size, offset);  
            });  
        }  
    }
}