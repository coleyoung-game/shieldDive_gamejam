using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessEnd : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void EndRestart()
    {

        Debug.Log("aaa");

    }
}
