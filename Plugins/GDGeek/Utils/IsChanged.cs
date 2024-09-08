using UnityEngine;
using System.Collections;
namespace GDGeek
{
    public class IsChanged 
    {

        private TransformData data_ = new TransformData();
        
        public bool hasChanged(Transform transform) {
            TransformData data = new TransformData(transform, Space.World);
            if (data_ != data) {
                data_ = data;
                return true;
            }
           
              

            return false;
        }
        
       
    }
}