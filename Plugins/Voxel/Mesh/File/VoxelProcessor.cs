using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{


    public interface IVoxelProcessor
    {

        bool include(MagicaVoxel magicaVoxel, VoxelData vox);

    }

}