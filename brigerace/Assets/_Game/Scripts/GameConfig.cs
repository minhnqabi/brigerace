﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : SingletonMonoBehaviour<GameConfig>
{
    
    // Start is called before the first frame update
    public GameObject brige, brick;
    public Material[] player, mat1, mat2, none;
    public Material[] GetMatByType(StepType _t)
    {
        switch (_t)
        {
            case StepType.NONE:
                return none;

            case StepType.PLAYER:
                return player;

            case StepType.AI1:
                return mat1;

            case StepType.AI2:
                return mat2;

        }
        return none;
    }
    private void Start() {
        Application.targetFrameRate=60;
    }


}
