using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ArmTexture : MonoBehaviour
{   
    public int[] rectIndexs={4323,4426,4442,4035};
    private bool firstTime=true;
    public Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        if(firstTime&&mesh!=null){
            GenUV();
            firstTime=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(firstTime&&mesh!=null){
            GenUV();
            firstTime=false;
        }
    }

    float GetCross(Vector2 p1, Vector2 p2,Vector2 p)
    {
        return (p2.x - p1.x) * (p.y - p1.y) -(p.x - p1.x) * (p2.y - p1.y);
    }

    private bool inArea(Vector2[] area,Vector2 p){
        return GetCross(area[0],area[1],p) * GetCross(area[2],area[3],p) >= 0 && GetCross(area[1],area[2],p) *  GetCross(area[3],area[0],p) >= 0;
    }

    private void GenUV(){
        MeshFilter meshfilter=gameObject.GetComponent<MeshFilter>();
        
        if(!meshfilter){
            meshfilter=gameObject.AddComponent<MeshFilter>();
        }
        Vector2[] uvs=mesh.uv;
        int[] triangles=mesh.triangles;
        Vector2[] fourUV=new Vector2[4];
        for(int i=0;i<4;i++){
            fourUV[i]=uvs[triangles[rectIndexs[i]*3]];
        }
        for(int i=0;i<uvs.Length;i++){
            if(!inArea(fourUV,uvs[i])) uvs[i]=new Vector2(0,0);
            else{
                uvs[i]=(uvs[i]-fourUV[0])/(fourUV[3]-fourUV[0]);
            }
        }
        mesh.uv=uvs;
        meshfilter.mesh=mesh;
    }
}
