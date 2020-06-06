using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIndex : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public int triangle_index;
    void Update()
    {   
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 100)){
                // MeshCollider collider=hit.collider as MeshCollider;
                // if(collider==null||collider.sharedMesh==null) return;
                // Mesh mesh0=collider.sharedMesh;
                // Vector3[] vertices=mesh0.vertices;
                // int[] triangles=mesh0.triangles;
                triangle_index=hit.triangleIndex;
            }
        }

    }
}
