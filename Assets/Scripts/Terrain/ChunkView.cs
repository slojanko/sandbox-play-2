using System.Collections;
using UnityEngine;

namespace Sandbox {
    [RequireComponent(typeof(MeshRenderer))]
    public class ChunkView : MonoBehaviour
    {
        private MeshRenderer renderer;
        private ChunkData data;

        public void Init(ChunkData data)
        {
            this.data = data;
        }

        public void OnDestroy()
        {
        }
    }
}