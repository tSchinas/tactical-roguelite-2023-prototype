using UnityEngine;
using System.Collections;
//<summary>
//exception class so listeners which need to produce an exception
//can then listen and alter a scenario as necessary
//</summary>
public class BaseException
{
    public bool toggle { get; private set; }
    public readonly bool defaultToggle;

    public BaseException(bool defaultToggle)
    {
        this.defaultToggle = defaultToggle;
        toggle = defaultToggle;
    }

    public void FlipToggle()
    {
        toggle = !defaultToggle;
    }
}