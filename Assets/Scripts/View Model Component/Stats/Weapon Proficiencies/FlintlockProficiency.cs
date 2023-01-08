//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FlintlockProficiency : MonoBehaviour
//{
//    #region Consts
//    public const int minLevel = 1;
//    public const int maxLevel = 10;
//    public const int maxExperience = 920;
//    #endregion

//    #region Fields/Properties
    
//    public int W5L { get { return stats[MCStatTypes.W5L]; } }
//    public int W5E { get { return stats[MCStatTypes.W5E]; } set { stats[MCStatTypes.W5E] = value; } }
//    public float W5EP { get { return (float)(W5L - minLevel) / (float)(maxLevel - minLevel); } }
    
//    MCStats stats;
//    #endregion

//    #region MonoBehavior

//    private void Awake()
//    {
//        stats = GetComponent<MCStats>();
//    }
//    private void OnEnable()
//    {
//        this.AddObserver(OnExpWillChange, MCStats.WillChangeNotification(MCStatTypes.W5E), stats);
//        this.AddObserver(OnExpDidChange, MCStats.DidChangeNotification(MCStatTypes.W5E), stats);
//    }
//    private void OnDisable()
//    {
//        this.AddObserver(OnExpWillChange, MCStats.WillChangeNotification(MCStatTypes.W5E), stats);
//        this.AddObserver(OnExpDidChange, MCStats.WillChangeNotification(MCStatTypes.W5E), stats);
//    }
//    #endregion

//    #region Event Handlers
//    void OnExpWillChange(object sender, object args)
//    {
//        ValueChangeException vce = args as ValueChangeException;
//        vce.AddModifier(new ClampValueModifier(int.MaxValue, W5E, maxExperience));
//    }

//    void OnExpDidChange(object sender, object args)
//    {
//        stats.SetValue(MCStatTypes.W5L, LevelForExperience(W5E), false);
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
//    public void Init(int level)
//    {
//        stats.SetValue(MCStatTypes.W5L, level, false);
//        stats.SetValue(MCStatTypes.W5E, ExperienceForLevel(level), false);
//    }
//    #endregion
//}
