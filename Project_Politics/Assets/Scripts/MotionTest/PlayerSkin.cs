using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    Color srColor;
    SpriteRenderer sr;
    public Animator anim;
    public AnimatorOverrideController aoc;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        srColor = sr.color;
        srColor.a = 0;
        sr.color = srColor;


    }

    public void EquipSkin( AnimationClip[] clips )
    {
        if(clips != null)
        {
            sr.color = Color.white;
            aoc["Idle"] = clips[0];
            aoc["Walk"] = clips[1];
            aoc["BackWalk"] = clips[2];
            aoc["Volunteer"] = clips[3];

        }
        else
        {
            srColor.a = 0;
            sr.color = srColor;
            aoc["Idle"] = null;
            aoc["Walk"] = null;
            aoc["BackWalk"] = null;
            aoc["Volunteer"] = null;
        }
    }



}
