using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenBrick : SingletonMonoBehaviour<GenBrick>
{
    public Transform[] allPos;
    public StepType owner;
    public Stack<Transform> stackBrickNull = new Stack<Transform>();// chứa những vị trí brick đã bị lấy
    public StepType[] owners;//những character hiện tại ở map

    private void Start()
    {
        this.Setup();
    }
    public void Setup()
    {
        foreach (var v in allPos)
        {
            GameObject brickObj = SimplePool.Spawn(GameConfig.instance.brick, v.position, Quaternion.identity);
            Brick _brick = brickObj.GetComponent<Brick>();
            _brick.Setup(v.position, this.GetOwner(), v);
        }
    }
    public void PushTransToStackBrickNull(Transform _tr)
    {
        stackBrickNull.Push(_tr);
    }
    public void ReSpawnBrick(StepType newOwner)
    {
        if (stackBrickNull.Count > 0)
        {
            Transform _tr = stackBrickNull.Pop();
            GameObject brickObj = SimplePool.Spawn(GameConfig.instance.brick, _tr.position, Quaternion.identity);
            Brick _brick = brickObj.GetComponent<Brick>();
            _brick.Setup(_tr.position, newOwner, _tr);
        }
    }
    public float nextCheck = 1.0f;
    float mDelta = 0;
    private void Update()
    {
        if (Time.time > mDelta)
        {
            mDelta += nextCheck;
            if (stackBrickNull.Count > 0)
            {
                this.ReSpawnBrick(GetOwner());
            }

        }
    }

    public StepType GetOwner()
    {
        return owners[Random.Range(0, owners.Length)];
    }
}
