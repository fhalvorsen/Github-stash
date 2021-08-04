using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimTest : MonoBehaviour
{
    public Animator charAnim;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            charAnim.SetTrigger("WalkTrigger");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            charAnim.SetTrigger("IdleTrigger");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            charAnim.SetTrigger("ReadingTrigger");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            charAnim.SetTrigger("BikeTrigger");
        }
    }
}
