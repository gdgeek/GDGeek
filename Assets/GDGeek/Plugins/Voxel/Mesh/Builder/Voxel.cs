using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class Voxel : MonoBehaviour {

		public VoxelProperty property {
			get{
				VoxelProperty prop = new VoxelProperty();
				prop.color = this.color;
				prop.scale = this.transform.localScale;
				prop.position = this.transform.position;
				return prop;
			}
			set{
				VoxelProperty prop = value;
				this.color = prop.color;
				this.transform.position = prop.position;
				this.transform.localScale = prop.scale;
			}
		}

		public Color color {
			get{
				return this.GetComponent<Renderer> ().material.color;
			}
			set{
				this.GetComponent<Renderer> ().material.color = value;
		
			}
		}



	}
}