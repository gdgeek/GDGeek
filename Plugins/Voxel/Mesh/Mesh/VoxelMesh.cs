using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;


namespace GDGeek{



	public class _VoxelMesh : MonoBehaviour{

		/*public void setMainColor(Color color){
			MeshRenderer renderer = filter.gameObject.GetComponent<MeshRenderer> ();
			renderer.material.color = color;
		}*/
		[FormerlySerializedAs("collider")]
		public BoxCollider boxCollider = null;
		public MeshFilter filter = null;
	}

}
