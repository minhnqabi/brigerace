using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ManageUI : SingletonMonoBehaviour<ManageUI>
{
    public TextMeshProUGUI tmpBrickCount;
    public void UpdateBrickCount(int n)
    {
        this.tmpBrickCount.text = n.ToString();

    }
}
