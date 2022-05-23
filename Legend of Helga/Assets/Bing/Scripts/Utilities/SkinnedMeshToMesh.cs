using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SkinnedMeshToMesh : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;
    public VisualEffect VFXGraph;
    public float refreshRate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateVFXGraph());
    }

    IEnumerator UpdateVFXGraph()
    {
        while (gameObject.activeSelf)
        {
            Mesh m1 = new Mesh();
            skinnedMesh.BakeMesh(m1);
            Vector3[] meshVertices = m1.vertices;

            Mesh m2 = new Mesh();
            m2.vertices = meshVertices;

            VFXGraph.SetMesh("Mesh", m2);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
