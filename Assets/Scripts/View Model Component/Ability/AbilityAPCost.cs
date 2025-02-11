using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class AbilityAPCost : MonoBehaviour
{
    public int amount = 1;
    Ability ability;

    private void Awake()
    {
        ability = GetComponent<Ability>();
    }

    private void OnEnable()
    {
        this.AddObserver(OnCanPerformCheck, Ability.CanPerformCheck, ability);
        this.AddObserver(OnDidPerformNotification, Ability.DidPerformNotification, ability);
    }
    void OnDisable()
    {
        this.RemoveObserver(OnCanPerformCheck, Ability.CanPerformCheck, ability);
        this.RemoveObserver(OnDidPerformNotification, Ability.DidPerformNotification, ability);
    }

    void OnCanPerformCheck(object sender, object args)
    {
        Stats s = GetComponentInParent<Stats>();
        if (s[StatTypes.AP] < amount)
        {
            BaseException exc = (BaseException)args;
            exc.FlipToggle();
        }
    }
    void OnDidPerformNotification(object sender, object args)
    {
        Stats s = GetComponentInParent<Stats>();
        s[StatTypes.AP] -= amount;
    }
}
