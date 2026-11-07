using UnityEngine;

namespace GDGeek
{
    public interface IBoxColliderBoundsProvider
    {
        Vector3 Center { get; }
        Vector3 Size { get; }
    }
}
