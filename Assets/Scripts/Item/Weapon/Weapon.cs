using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Study3D;

public abstract class Weapon : ItemObject
{
    // 무기별로 상속받게 하고, 각자 집어넣기, 꺼내기 모션을 다르게 구현하도록 해야할듯. 일단은 대충 짜자


    // 집어넣기
    public void Holster()
    {
        Transform t_temp = Player.player.transform.Find("Temp");
        transform.SetParent(t_temp );
        transform.localPosition = Vector3.zero;
    }

    // 꺼내기
    public void Hold()
    {
        Transform t_hand = Player.player.transform.Find("hand");
        transform.SetParent(t_hand );
        transform.localPosition = Vector3.zero;
    }
}
