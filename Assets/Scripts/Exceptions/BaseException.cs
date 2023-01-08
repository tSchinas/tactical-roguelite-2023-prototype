using UnityEngine;
using System.Collections;
//<summary>
//exception class so listeners which need to produce an exception
//can then listen and alter a scenario as necessary
//</summary>
public class BaseException
{
    public bool Toggle { get; private set; }
    private bool DefaultToggle;

    public BaseException(bool defaultToggle)
    {
        this.DefaultToggle = defaultToggle;
        Toggle = defaultToggle;
    }

    public void FlipToggle()
    {
        Toggle = !DefaultToggle;
    }
}