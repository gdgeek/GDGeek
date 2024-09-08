using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek{
    public class JustUp : MonoBehaviour
    {
      
        // Update is called once per frame
        void Update()
        {
            Vector3 forward = transform.forward;
            forward.y = 0;
            if (forward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(forward.normalized, Vector3.up);
            }
            else
            {
                transform.up = Vector3.up;
            }
        }
    }
}