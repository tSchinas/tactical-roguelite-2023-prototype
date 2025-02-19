using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimationEnd : MonoBehaviour
{
    public void DestroyParent()
    {
        GameObject parent = this.transform.parent.gameObject;
        Destroy(parent);
    }
}
