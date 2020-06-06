using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeOnIndex : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject arm;
    public Mesh armMesh;
    public float disPara = 0.2f;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private Mesh mesh;
    private Material material;
    //使用GetIndex得到的
    public int forearm_index = 4234;

    void Start()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshCollider = gameObject.GetComponent<MeshCollider>();
        if (meshFilter==null){
        meshFilter = gameObject.AddComponent<MeshFilter>();
        }
        if (meshRenderer == null)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }
        mesh = meshCollider.sharedMesh;
        Vector3 forearm_normal = CalTriNormal();
        gameObject.transform.forward = -forearm_normal;
    }

    // Update is called once per frame
    void Update()
    {
        if (armMesh != null && arm != null)
        {
            Vector3 forearm_normal = CalTriNormal();

            Vector3 forearm_pos = CalTriPos();
           
            //Vector3 forearm_dir=CalTriDir();

            gameObject.transform.position = forearm_pos + forearm_normal * disPara;

            // Debug.DrawLine (forearm_pos, forearm_pos+gameObject.transform.forward, Color.red, 0.0f);
            // Debug.DrawLine (forearm_pos, forearm_pos+gameObject.transform.up, Color.blue, 0.0f);
            // Debug.DrawLine (forearm_pos, forearm_pos+gameObject.transform.right, Color.yellow, 0.0f);
        }
    }

    private void OnDisable()
    {
        Destroy(meshFilter);
        Destroy(meshRenderer);
        mesh = null;
    }

    // private Vector3 CalTriDir(){
    //     Vector3 p1,p2;
    //     Vector3[] vertices = armMesh.vertices;
    //     int[] triangles=armMesh.triangles;
    //     p1=vertices[triangles[forearm_index*3]];
    //     p2=vertices[triangles[forearm_index*3+1]];
    //     p1=arm.transform.TransformPoint(p1);
    //     p2=arm.transform.TransformPoint(p2);
    //     return (p2-p1);
    // }
    private Vector3 CalTriPos()
    {
        Vector3 p1, p2, p3;
        Vector3[] vertices = armMesh.vertices;
        int[] triangles = armMesh.triangles;
        p1 = vertices[triangles[forearm_index * 3]];
        p2 = vertices[triangles[forearm_index * 3 + 1]];
        p3 = vertices[triangles[forearm_index * 3 + 2]];

        p1 = arm.transform.TransformPoint(p1);
        p2 = arm.transform.TransformPoint(p2);
        p3 = arm.transform.TransformPoint(p3);
        return (p1 + p2 + p3) / 3;
    }
    private Vector3 CalTriNormal()
    {
        Vector3 p1, p2, p3;
        Vector3[] normals = armMesh.normals;
        int[] triangles = armMesh.triangles;
        p1 = normals[triangles[forearm_index * 3]];
        p2 = normals[triangles[forearm_index * 3 + 1]];
        p3 = normals[triangles[forearm_index * 3 + 2]];

        p1 = arm.transform.TransformVector(p1);
        p2 = arm.transform.TransformVector(p2);
        p3 = arm.transform.TransformVector(p3);

        return (p1 + p2 + p3) / 3;
    }
    private Mesh CreateMesh(float width, float height)
    {
        Mesh m = new Mesh();
        m.vertices = new Vector3[]{
            new Vector3(-width/2,-height/2,0),
            new Vector3(-width/2,height/2,0),
            new Vector3(width/2,height/2,0),
            new Vector3(width/2,-height/2,0)
        };
        m.uv = new Vector2[]{
            new Vector2(0,0),
            new Vector2(0,1),
            new Vector2(1,1),
            new Vector2(1,0)
        };
        m.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        m.RecalculateNormals();
        m.RecalculateBounds();
        return m;
    }

}
