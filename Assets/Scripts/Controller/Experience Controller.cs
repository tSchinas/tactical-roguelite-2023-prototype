using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ExperienceController
{
    const int mainWepExp = 2;
    const int subWepExp = 1;

    public static void AwardExperience()
    {
        //when attacking or using an ability
        //get component used to attack (sword, dagger, wand, etc.)
        //award 2 exp to equipment in main weapon slot
        //if subweapon != null, award 1 exp to equipment in sub weapon slot
    }
}
