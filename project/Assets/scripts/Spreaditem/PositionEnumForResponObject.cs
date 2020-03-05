using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 *引き出しや机の上に配置する場合を想定したもの
 * (今回は活用せず)
 * 
 */

public class PositionEnumForResponObject : MonoBehaviour {

    public PositionEnumerate positionEnumerate;
    public int RespawnPosition{ get; set; }
    private void OnValidate()
    {
        RespawnPosition = (int)positionEnumerate;
    }
    private void Awake()
    {
        RespawnPosition = (int)positionEnumerate;
    }
}
