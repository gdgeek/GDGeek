using UnityEngine;
using System.Collections;
using System;

namespace GDGeek{
	[Serializable]
	public struct BoxInt {
		public VectorInt3 center;
		public VectorInt3 size;
	}
}