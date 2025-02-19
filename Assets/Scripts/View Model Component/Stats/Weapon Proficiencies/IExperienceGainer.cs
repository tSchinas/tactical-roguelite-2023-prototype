using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExperienceGainer
{
    void GainExperience(Weapon mainWeapon, Weapon subWeapon);
}
