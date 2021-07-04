using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTri : MonoBehaviour
{
    // Start is called before the first frame update
    float x = Random.Range(1, 10);
    float y = Random.Range(1, 10);
    float z = Random.Range(1, 10);
    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;

    void Start() {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
