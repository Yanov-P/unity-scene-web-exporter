using UnityEngine;

namespace Assets.Kanau.Utils
{
    public class ComponentHelper
    {
        public static Mesh GetMesh(GameObject go) {
            Mesh mesh = null;
            if (go.GetComponent<Renderer>() is MeshRenderer) {
                MeshFilter meshFilter = go.GetComponent<MeshFilter>();
                mesh = meshFilter.sharedMesh;
            } else {
                mesh = go.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            }
            return mesh;
        }

        public static string GetMaterialName(GameObject go)
        {
            return go.GetComponent<Renderer>().sharedMaterial.name;
        }
    }
}
