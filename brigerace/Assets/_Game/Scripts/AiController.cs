using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : SingletonMonoBehaviour<AiController>
{
    // Start is called before the first frame update
    public List<AIPlayer> listAi;
   

    // Update is called once per frame
    void Update()
    {
       for(int i=0;i<listAi.Count;i++)
       {
        listAi[i].ActionUpdate();
       }
        
    }
    public void ActiveAi()
    {
        foreach (var ai in this.listAi)
        {
            ai.Init();
        }
    }
}
