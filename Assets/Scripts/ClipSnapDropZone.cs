using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
    public class ClipSnapDropZone : VRTK_SnapDropZone
    {
        public void DestroyConnectedClip()
        {
            Destroy(currentSnappedObject);
        }
    }
}