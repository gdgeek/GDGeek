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
    
        public class FacePackage
        {
            private float size_  = 0.0f;
            public FacePackage(Vector3Int direction, 
                float size)
            {
                size_ = size;
                this.direction = direction;
            }

            public class Rectangle
            {
                public Vector2Int index => Index(center, normal, this.size, this.offset);

                // 重载加号运算符
                public static Rectangle operator +(Rectangle a, Rectangle b)
                {
                   
                    HashSet<VoxelVertex> vertices = new HashSet<VoxelVertex>();

                    if(a.normal == Vector3Int.forward)
                    {
                        vertices.Add( a.p0.xy.x > b.p0.xy.x || a.p0.xy.y < b.p0.xy.y ? a.p0:b.p0);
                        vertices.Add( a.p1.xy.x < b.p1.xy.x || a.p1.xy.y < b.p1.xy.y ? a.p1:b.p1);
                        vertices.Add( a.p2.xy.x >b.p2.xy.x || a.p2.xy.y > b.p2.xy.y ? a.p2:b.p2);
                        vertices.Add( a.p3.xy.x < b.p3.xy.x || a.p3.xy.y > b.p3.xy.y ? a.p3:b.p3);
                    }
                    if(a.normal == Vector3Int.up)
                    {
                        vertices.Add( a.p0.xy.x > b.p0.xy.x || a.p0.xy.y > b.p0.xy.y ? a.p0:b.p0);
                        vertices.Add( a.p1.xy.x < b.p1.xy.x || a.p1.xy.y > b.p1.xy.y ? a.p1:b.p1);
                        vertices.Add( a.p2.xy.x > b.p2.xy.x || a.p2.xy.y < b.p2.xy.y ? a.p2:b.p2);
                        vertices.Add( a.p3.xy.x < b.p3.xy.x || a.p3.xy.y < b.p3.xy.y ? a.p3:b.p3);
                    }
                    if(a.normal == Vector3Int.back)
                    {
                        vertices.Add( a.p0.xy.x > b.p0.xy.x || a.p0.xy.y > b.p0.xy.y ? a.p0:b.p0);
                        vertices.Add( a.p1.xy.x < b.p1.xy.x || a.p1.xy.y > b.p1.xy.y ? a.p1:b.p1);
                        vertices.Add( a.p2.xy.x > b.p2.xy.x || a.p2.xy.y < b.p2.xy.y ? a.p2:b.p2);
                        vertices.Add( a.p3.xy.x < b.p3.xy.x || a.p3.xy.y < b.p3.xy.y ? a.p3:b.p3);
                    }
                    if(a.normal == Vector3Int.down)
                    {
                        vertices.Add( a.p0.xy.x > b.p0.xy.x || a.p0.xy.y < b.p0.xy.y ? a.p0:b.p0);
                        vertices.Add( a.p1.xy.x < b.p1.xy.x || a.p1.xy.y < b.p1.xy.y ? a.p1:b.p1);
                        vertices.Add( a.p2.xy.x > b.p2.xy.x || a.p2.xy.y > b.p2.xy.y ? a.p2:b.p2);
                        vertices.Add( a.p3.xy.x < b.p3.xy.x || a.p3.xy.y > b.p3.xy.y ? a.p3:b.p3);
                    }
                    
                    if(a.normal == Vector3Int.left)
                    {
                        vertices.Add( a.p0.xy.x > b.p0.xy.x || a.p0.xy.y < b.p0.xy.y ? a.p0:b.p0);
                        vertices.Add( a.p1.xy.x < b.p1.xy.x || a.p1.xy.y < b.p1.xy.y ? a.p1:b.p1);
                        vertices.Add( a.p2.xy.x > b.p2.xy.x || a.p2.xy.y > b.p2.xy.y ? a.p2:b.p2);
                        vertices.Add( a.p3.xy.x < b.p3.xy.x || a.p3.xy.y > b.p3.xy.y ? a.p3:b.p3);
                    }
                    
                    if(a.normal == Vector3Int.right)
                    {
                        vertices.Add( a.p0.xy.x < b.p0.xy.x || a.p0.xy.y < b.p0.xy.y ? a.p0:b.p0);
                        vertices.Add( a.p1.xy.x > b.p1.xy.x || a.p1.xy.y < b.p1.xy.y ? a.p1:b.p1);
                        vertices.Add( a.p2.xy.x < b.p2.xy.x || a.p2.xy.y > b.p2.xy.y ? a.p2:b.p2);
                        vertices.Add( a.p3.xy.x > b.p3.xy.x || a.p3.xy.y > b.p3.xy.y ? a.p3:b.p3);
                    }
                   
                    return new Rectangle(vertices, a.size, a.offset);

                }
                public int layer { private set; get; }


                public Vector3Int normal { private set; get; }
                public Color32 color { private set; get; }
                public Vector3 offset { private set; get; }
                public float size { private set; get; }
                public Vector2 center { private set; get; }


                public VoxelVertex p0 { private set; get; }

                public VoxelVertex p1 { private set; get; }

                public VoxelVertex p2 { private set; get; }

                public VoxelVertex p3 { private set; get; }


                public static Vector2Int Index(Vector2 center, Vector3Int normal, float size, Vector3 offset)
                {
                    Vector2 ret = (((center / size) - VoxelVertex.ToVector2(normal, offset)) * 4f);
                    return new Vector2Int(Mathf.RoundToInt(ret.x), Mathf.RoundToInt(ret.y));
                }

                
                public Rectangle(HashSet<VoxelVertex> vertices, float size, Vector3 offset)
                {
                   
                    Assert.AreEqual(vertices.Count, 4);
                    var main = vertices.First();

                    Assert.IsNotNull(main);
                    layer = Layer(main, size, offset);
              
                    this.normal = main.normal;
                    this.color = main.color;
                    this.offset = offset;
                    this.size = size;

                    this.center = new Vector2(vertices.Average(v => (float)v.xy.x), vertices.Average(v =>  (float)v.xy.y));

               

                    foreach (var vertice in vertices)
                    {
                        Assert.AreEqual(vertice.normal, main.normal);
                        Assert.AreEqual(vertice.color, main.color);
                    }

                    place(vertices);

                }


                private void place(HashSet<VoxelVertex> vertices)
                {
                    
                    if (normal == Vector3Int.forward)
                    {

                        foreach (var vertex in vertices)
                        {
                            if (vertex.xy.x > center.x && vertex.xy.y < center.y)
                            {
                                p0 = vertex;
                            }

                            if (vertex.xy.x < center.x && vertex.xy.y < center.y)
                            {
                                p1 = vertex;
                            }

                            if (vertex.xy.x > center.x && vertex.xy.y > center.y)
                            {
                                p2 = vertex;
                            }

                            if (vertex.xy.x < center.x && vertex.xy.y > center.y)
                            {
                                p3 = vertex;
                            }
                        }
                    }

                    else if (normal == Vector3Int.up )
                    {
                        foreach (var vertex in vertices)
                        {

                            if (vertex.xy.x > center.x && vertex.xy.y > center.y)
                            {
                                p0 = vertex;
                            }

                            if (vertex.xy.x < center.x && vertex.xy.y > center.y)
                            {
                                p1 = vertex;
                            }

                            if (vertex.xy.x > center.x && vertex.xy.y < center.y)
                            {
                                p2 = vertex;
                            }

                            if (vertex.xy.x < center.x && vertex.xy.y < center.y)
                            {
                                p3 = vertex;
                            }


                        }
                    } else if (normal == Vector3Int.back )
                    {
                        foreach (var vertex in vertices)
                        {

                            if (vertex.xy.x > center.x && vertex.xy.y > center.y)
                            {
                                p0 = vertex;
                            }

                            if (vertex.xy.x < center.x && vertex.xy.y > center.y)
                            {
                                p1 = vertex;
                            }

                            if (vertex.xy.x > center.x && vertex.xy.y < center.y)
                            {
                                p2 = vertex;
                            }

                            if (vertex.xy.x < center.x && vertex.xy.y < center.y)
                            {
                                p3 = vertex;
                            }
                        }
                    }
                    
                    else if (normal == Vector3Int.down)
                    {
                        foreach (var vertex in vertices)
                        {

                            if (vertex.xy.x> center.x && vertex.xy.y < center.y)
                            {
                                p0 = vertex;
                            }

                            if (vertex.xy.x < center.x && vertex.xy.y < center.y)
                            {
                                p1 = vertex;
                            }

                            if (vertex.xy.x > center.x && vertex.xy.y > center.y)
                            {
                                p2 = vertex;
                            }

                            if (vertex.xy.x < center.x && vertex.xy.y > center.y)
                            {
                                p3 = vertex;
                            }


                        }
                    }
                    else if (normal == Vector3Int.left)
                    {
                        foreach (var vertex in vertices)
                        {

                            if (vertex.xy.x > center.x && vertex.xy.y < center.y)
                            {
                                p0 = vertex;
                            }

                            if (vertex.xy.x < center.x && vertex.xy.y < center.y)
                            {
                                p1 = vertex;
                            }

                            if (vertex.xy.x > center.x && vertex.xy.y > center.y)
                            {
                                p2 = vertex;
                            }

                            if (vertex.xy.x < center.x && vertex.xy.y > center.y)
                            {
                                p3 = vertex;
                            }


                        }
                    }
                    else if (normal == Vector3Int.right)
                    {
                        foreach (var vertex in vertices)
                        {

                            if (vertex.xy.x < center.x && vertex.xy.y < center.y)
                            {
                                p0 = vertex;
                            }

                            if (vertex.xy.x > center.x && vertex.xy.y < center.y)
                            {
                                p1 = vertex;
                            }

                            if (vertex.xy.x < center.x && vertex.xy.y > center.y)
                            {
                                p2 = vertex;
                            }

                            if (vertex.xy.x > center.x && vertex.xy.y > center.y)
                            {
                                p3 = vertex;
                            }


                        }

                    }
                    else
                    {
                        throw new System.Exception("Invalid normal");
                    }

                }




             
                public Tuple<Color32, int> key
                {
                    get
                    {
                        return new Tuple<Color32, int>(color, layer);
                    }
                }
            
                public static int Layer(VoxelVertex vertex, float size, Vector3 offset)
                {
                    float layer = 0;
                    if(vertex.normal == Vector3Int.up || vertex.normal == Vector3Int.down)
                    {
                        layer = vertex.position.y/size - offset.y;
                    }
                    else if(vertex.normal == Vector3Int.left || vertex.normal == Vector3Int.right)
                    {
                        layer =  vertex.position.x/size - offset.x;
                    }
                    else if(vertex.normal == Vector3Int.forward || vertex.normal == Vector3Int.back)
                    {
                        layer =  vertex.position.z/size - offset.z;
                    }
                    else
                    {
                        Debug.LogError(vertex.normal);
                        throw new System.Exception("Invalid normal");
                    }
                    return Mathf.RoundToInt( layer*2);
                }

            
            }
          

            private Vector3Int direction
            {
                get;
                set;
            } = Vector3Int.up;

             public Dictionary<Tuple<Color32, int>, Dictionary<Vector2Int, Rectangle>> map => map_;

            private Dictionary<Tuple<Color32, int>, Dictionary<Vector2Int, Rectangle>> map_ = new Dictionary<Tuple<Color32, int>, Dictionary<Vector2Int, Rectangle>>();
          
            public void add(Rectangle rectangle)
            {
            
                if(!map_.ContainsKey(rectangle.key))
                {
                    map_.Add(rectangle.key, new Dictionary<Vector2Int, Rectangle>());
                }
                map_[rectangle.key].Add(rectangle.index, rectangle);
              
            }

            
            private void merge(
                Dictionary<Vector2Int, Rectangle> oldMap, 
                Dictionary<Vector2Int, Rectangle> newMap
                )
            {
                if (oldMap.Count == 0)
                {
                    return;
                }
                Rectangle rect = popRect(oldMap);
                newMap.Add(rect.index, rect);
                merge(oldMap, newMap);
            }

           
            private Rectangle popRect(Dictionary<Vector2Int, Rectangle> oldMap)
            {
                var first = oldMap.First();
                List<Rectangle> list = new List<Rectangle>();
                list.Add(first.Value);//放入一个方块
                //放入竖排方块
                float size = size_;
                Vector2 center = first.Value.center + new Vector2(0, size);
                Vector2Int index = Rectangle.Index( center, first.Value.normal, first.Value.size, first.Value.offset);
                while (oldMap.ContainsKey(index))
                {
                   var second = oldMap[index];
                   list.Add(second);
                   size += size_;
                   center = first.Value.center + new Vector2(0, size);
                   index = Rectangle.Index( center, first.Value.normal, first.Value.size, second.offset);
                }

                //放入横排方块
                int n = list.Count;
                bool right = true;
                List<Rectangle> temp = new List<Rectangle>();
                int m = 1;
                while (right)
                {
                    for(int i=0; i<n; ++i)
                    {
                       Vector2 ctx = first.Value.center + new Vector2(size_*m, size_ * i);
                       Vector2Int idx = Rectangle.Index( ctx, first.Value.normal, first.Value.size,first.Value.offset);
                       
                       if (!oldMap.ContainsKey(idx))
                       {
                           right = false;
                           break;
                       }
                       else
                       {
                           var second = oldMap[idx];
                           temp.Add(second);
                       }
                    }
                    if (right)
                    {
                       list.AddRange(temp);
                    }
                    ++m;
                }


                foreach (var item in list)
                {
                    oldMap.Remove(item.index);
                }
             
                return list.Aggregate((a, b) => a + b);
            }


            public void merge()
            {
             
                ConcurrentDictionary<Tuple<Color32, int>, Dictionary<Vector2Int, Rectangle>> newMap = new ConcurrentDictionary<Tuple<Color32, int>, Dictionary<Vector2Int, Rectangle>>();  

                Parallel.ForEach(map_, item =>  
                {  
                    Dictionary<Vector2Int, Rectangle> newSet = new Dictionary<Vector2Int, Rectangle>();  
                    this.merge(item.Value, newSet);  
                    newMap.TryAdd(item.Key, newSet);  
                });  

                map_ = newMap.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);  
            }
        }
        
   
}