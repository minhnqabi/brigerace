using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenBrige : MonoBehaviour
{
    public int brigeNum;
    public float brigeHigh,brigeWidth;
    GameObject brige;
    Vector3 startPosGenBrige;
    public Transform tranRot;
    public Transform startTrans;
    

    // Start is called before the first frame update
    void Start()
    {
        brige=GameConfig.instance.brige;
        startPosGenBrige=startTrans.position;
        this.Gen();
        transform.rotation=tranRot.rotation;
        
    }
    public void Gen()
    {
        for(int i=1;i<brigeNum;i++)
        {
            GameObject obj=SimplePool.Spawn(GameConfig.instance.brige,
            new Vector3(startPosGenBrige.x,startPosGenBrige.y+brigeHigh*i,startPosGenBrige.z+brigeWidth*i),
            Quaternion.identity);
            obj.transform.SetParent(transform);
            obj.GetComponent<Brige>().Setup(this);
        }

    }
    Brige currentActiveBrige;
    public Brige CurrentActiveBrige()
    {
        return currentActiveBrige;
    }
    public void ActiveBrige(Brige bridge)
    {
        this.currentActiveBrige=bridge;
    }

}
