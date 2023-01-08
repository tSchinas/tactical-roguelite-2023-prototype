//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class SwordProficiency : MonoBehaviour
//{
//    #region Consts
//    public const int minLevel = 1;
//    public const int maxLevel = 10;
//    public const int maxExperience = 920;
//    #endregion

//    #region Fields/Properties
    
//    public int W9L { get { return stats[MCStatTypes.W9L]; } }
//    public int W9E { get { return stats[MCStatTypes.W9E]; } set { stats[MCStatTypes.W9E] = value; } }
//    public float W9EP { get { return (float)(W9L - minLevel) / (float)(maxLevel - minLevel); } }
    
//    MCStats stats;
//    #endregion

//    #region MonoBehavior
    
//    private void Awake()
//    {
//        stats = GetComponent<MCStats>();
//    }
//    private void OnEnable()
//    {
//        this.AddObserver(OnExpWillChange, MCStats.WillChangeNotification(MCStatTypes.W9E), stats);
//        this.AddObserver(OnExpDidChange, MCStats.DidChangeNotification(MCStatTypes.W9E), stats);
//    }
//    private void OnDisable()
//    {
//        this.AddObserver(OnExpWillChange, MCStats.WillChangeNotification(MCStatTypes.W9E), stats);
//        this.AddObserver(OnExpDidChange, MCStats.WillChangeNotification(MCStatTypes.W9E), stats);
//    }
//    #endregion

//    #region Event Handlers
//    void OnExpWillChange(object sender, object args)
//    {
//        ValueChangeException vce = args as ValueChangeException;
//        vce.AddModifier(new ClampValueModifier(int.MaxValue, W9E, maxExperience));
//    }

//    void OnExpDidChange(object sender, object args)
//    {
//        stats.SetValue(MCStatTypes.W9L, LevelForExperience(W9E), false);
//    }
//    #endregion
//    #region Public
//    public static int ExperienceForLevel(int level)
//    {
//        float levelPercent = Mathf.Clamp01((float)(level - minLevel) / (float)(maxLevel - minLevel));
//        return (int)EasingEquations.EaseInQuad(0, maxExperience, levelPercent);
//    }

//    public static int LevelForExperience(int exp)
//    {
//        int lvl = maxLevel;
//        for (; lvl >= minLevel; --lvl)
//            if (exp >= ExperienceForLevel(lvl))
//                break;
//        return lvl;
//    }
//    public void Init (int level)
//    {
//        stats.SetValue(MCStatTypes.W9L, level, false);
//        stats.SetValue(MCStatTypes.W9E, ExperienceForLevel(level), false);
//    }
//    #endregion
//}
