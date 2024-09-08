using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderBuilder : MonoBehaviour,IExecute
{

    public BoxCollider building()
    {
        Renderer[] renderers  = this.GetComponentsInChildren<Renderer>();
        bool hasBounds = false;
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        Quaternion rotate = this.transform.rotation;
        Vector3 position = this.transform.position;
        Vector3 scale = this.transform.lossyScale;
        this.transform.rotation = Quaternion.identity;
        this.transform.position = Vector3.zero;
        this.transform.setGlobalScale(Vector3.one);
        foreach (var childRenderer in renderers)
        {
            if (childRenderer != null)
            {
                if (hasBounds)
                {
                    bounds.Encapsulate(childRenderer.bounds);
                }
                else
                {
                    bounds = childRenderer.bounds;
                    hasBounds = true;
                }
            }
        }
        BoxCollider bc = this.gameObject.AskComponent<BoxCollider>();
       
        bc.center = bounds.center;
        // var scale = this.transform.localScale;
        bc.size = bounds.size;// new Vector3(bounds_.size.x/ scale.x, bounds_.size.y / scale.y, bounds_.size.z / scale.z);

        this.transform.rotation = rotate;
        this.transform.position = position;
        this.transform.setGlobalScale(scale);
        return bc;
    }

    public void execute()
    {
        building();
    }
}
