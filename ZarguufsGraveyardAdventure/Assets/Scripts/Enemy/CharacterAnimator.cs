using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Walk();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Idle() {
        anim.Play("idle");
    }

    public void Walk() {
        anim.Play("walk");
    }
}
