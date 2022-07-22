using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenBrick : SingletonMonoBehaviour<GenBrick>
{
    public Transform[] allPos, allPosLv2;
    public StepType owner;
    public Stack<Transform> stackBrickNull = new Stack<Transform>();// chứa những vị trí brick đã bị lấy
    public Stack<Transform> stackBrickNullLv2 = new Stack<Transform>();
    public StepType[] owners;//những character hiện tại ở map
    public List<StepType> ownersLevel2 = new List<StepType>();
    public float nextCheck = 1.0f;
    float mDelta = 0;
    public Stack<Brick> stackBrickAi1, stackBrickAi2;



    private void Start()
    {
        stackBrickAi1 = new Stack<Brick>();
        stackBrickAi2 = new Stack<Brick>();
        this.Setup();
        StartCoroutine(IEGenBrick());
    }
    public void Setup()
    {
        foreach (var v in allPos)
        {
            GameObject brickObj = SimplePool.Spawn(GameConfig.instance.brick, v.position, Quaternion.identity);
            Brick _brick = brickObj.GetComponent<Brick>();
            
            _brick.Setup(v.position, this.GetOwner(), v);
            if(_brick.owner==StepType.AI1)
            {
                stackBrickAi1.Push(_brick);

            }
            else if(_brick.owner==StepType.AI2)
            {
                stackBrickAi2.Push(_brick);
            }

        }
        AiController.instance.ActiveAi();
    }
    public void PushTransToStackBrickNull(Transform _tr, bool isLv1 = true)
    {
        if (isLv1)
        {

            stackBrickNull.Push(_tr);
        }
        else
        {
            stackBrickNullLv2.Push(_tr);
        }
    }
    public void ReSpawnBrick(StepType newOwner, bool isLv1 = true)
    {
        if (stackBrickNull.Count > 0)
        {
            Debug.LogWarning("Spawn1");
            Transform _tr = stackBrickNull.Pop();
            GameObject brickObj = SimplePool.Spawn(GameConfig.instance.brick, _tr.position, Quaternion.identity);
            Brick _brick = brickObj.GetComponent<Brick>();
            _brick.Setup(_tr.position, newOwner, _tr);
        }
        if (!isLv1)
        {
            if (stackBrickNullLv2.Count > 0)
            {
                Debug.LogWarning("Spawn2");
                Transform _tr = stackBrickNullLv2.Pop();
                GameObject brickObj = SimplePool.Spawn(GameConfig.instance.brick, _tr.position, Quaternion.identity);
                Brick _brick = brickObj.GetComponent<Brick>();
                _brick.Setup(_tr.position, newOwner, _tr);
            }

        }
    }


    IEnumerator IEGenBrick()
    {

        yield return Yielders.Get(1.0f);
        if (stackBrickNull.Count > 0)
        {
            this.ReSpawnBrick(GetOwner());
        }
        StartCoroutine(IEGenBrick());

    }

    public StepType GetOwner(bool isLv1 = true)
    {
        if (isLv1)
        {

            return owners[Random.Range(0, owners.Length)];
        }
        else
        {
            return ownersLevel2[Random.Range(0, ownersLevel2.Count)];
        }
    }
    IEnumerator IEGenBrickLv2()
    {
        foreach (var v in allPosLv2)
        {
            yield return Yielders.Get(0.3f);
            GameObject brickObj = SimplePool.Spawn(GameConfig.instance.brick, v.position, Quaternion.identity);
            Brick _brick = brickObj.GetComponent<Brick>();
            _brick.Setup(v.position, this.GetOwner(false), v, false);

        }
    }
    IEnumerator IEReSpawnBrickLv2()
    {
        yield return Yielders.Get(1.0f);
        if (stackBrickNullLv2.Count > 0)
        {
            //Debug.LogWarning("Spawn2");
            this.ReSpawnBrick(this.GetOwner(false),false);
        }
        StartCoroutine(IEReSpawnBrickLv2());
    }
    public void StartGenBrickLevel2()
    {
        this.StopAllCoroutines();
        StartCoroutine(IEGenBrickLv2());
        StartCoroutine(IEReSpawnBrickLv2());
    }
    public void AddOwnerToLv2(StepType owner)
    {
        if (!ownersLevel2.Contains(owner))
        {
            ownersLevel2.Add(owner);
        }
    }
}
