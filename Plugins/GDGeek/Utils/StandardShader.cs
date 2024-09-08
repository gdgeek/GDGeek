using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    public class StandardShader : MonoBehaviour
    {
        [SerializeField]
        private StandardShaderUtils.BlendMode _mode = StandardShaderUtils.BlendMode.Opaque;
        private StandardShaderUtils.BlendMode _original = StandardShaderUtils.BlendMode.Opaque;


        
        private Renderer renderer_ = null;
        // Use this for initialization
        void Start ()
        {
            renderer_ = this.GetComponent<Renderer>();
            _original = _mode;
            refresh();
	    }
        public void changeRenderMode(StandardShaderUtils.BlendMode mode) {
            _mode = mode;
            refresh();
        }
        public void restore()
        {
            _mode = _original;
            refresh();
        }
        
        private void refresh()
        {
            StandardShaderUtils.ChangeRenderMode(renderer_.material, _mode);
        }

       
    }
}
