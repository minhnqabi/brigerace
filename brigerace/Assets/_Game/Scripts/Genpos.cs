using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genpos : MonoBehaviour
{
    // Start is called before the first frame update
    public int row, colum;
    public float width, heigh;
    public GameObject brick;
    private void Start()
    {
        this.GenBrick();

    }
    public void GenBrick()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < colum; j++)

            {
                GameObject pos = Instantiate(brick, new Vector3(width * i, 0.11f, heigh * j), Quaternion.identity);
                pos.transform.parent = transform;
            }
        }
    }

}
