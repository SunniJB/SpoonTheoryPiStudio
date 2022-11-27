using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabAnimController : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void On()
    {
        anim.SetTrigger("On");
    }
    public void Off()
    {
        anim.SetTrigger("Off");
    }

}
