using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{



	public class _VoxelMesh : MonoBehaviour{

		/*public void setMainColor(Color color){
			MeshRenderer renderer = filter.gameObject.GetComponent<MeshRenderer> ();
			renderer.material.color = color;
		}*/
		public BoxCollider collider = null;
		public MeshFilter filter = null;
	}

}
