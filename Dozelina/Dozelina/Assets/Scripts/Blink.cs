using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Blinking()
    {
        animator.SetTrigger("Blink");
    }
}