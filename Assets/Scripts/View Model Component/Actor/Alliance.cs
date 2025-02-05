using System.Collections;
using UnityEngine;


public class Alliance : MonoBehaviour
{
    public Alliances allianceType;
    //public Targets targets;
    public bool confused;

    public bool IsMatch(Alliance other, Targets targets)
    {
        bool isMatch = false;
        switch (targets)
        {
            case Targets.Self:
                isMatch = other == this;
                break;
            case Targets.Ally:
                isMatch = allianceType == other.allianceType;
                break;
            case Targets.Foe:
                isMatch = (allianceType != other.allianceType) && other.allianceType != Alliances.Neutral;
                break;
        }
        return confused ? !isMatch : isMatch;
    }

    
}
