using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button1 : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        GameObject.Find("block_group").GetComponent<astar_manager>().button();
    }
}
