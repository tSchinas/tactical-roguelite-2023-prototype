//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FanProficiency : MonoBehaviour
//{
//    #region Consts
//    public const int minLevel = 1;
//    public const int maxLevel = 10;
//    public const int maxExperience = 920;
//    #endregion

//    #region Fields/Properties
    
//    public int W4L { get { return stats[MCStatTypes.W4L]; } }
//    public int W4E { get { return stats[MCStatTypes.W4E]; } set { stats[MCStatTypes.W4E] = value; } }
//    public float W4EP { get { return (float)(W4L - minLevel) / (float)(maxLevel - minLevel); } }
    
//    MCStats stats;
//    #endregion

//    #region MonoBehavior

//    private void Awake()
//    {
//        stats = GetComponent<MCStats>();
//    }
//    private void OnEnable()
//    {
//        this.AddObserver(OnExpWillChange, MCStats.WillChangeNotification(MCStatTypes.W4E), stats);
//        this.AddObserver(OnExpDidChange, MCStats.DidChangeNotification(MCStatTypes.W4E), stats);
//    }
//    private void OnDisable()
//    {
//        this.AddObserver(OnExpWillChange, MCStats.WillChangeNotification(MCStatTypes.W4E), stats);
//        this.AddObserver(OnExpDidChange, MCStats.WillChangeNotification(MCStatTypes.W4E), stats);
//    }
//    #endregion

//    #region Event Handlers
//    void OnExpWillChange(object sender, object args)
//    {
//        ValueChangeException vce = args as ValueChangeException;
//        vce.AddModifier(new ClampValueModifier(int.MaxValue, W4E, maxExperience));
//    }

//    void OnExpDidChange(object sender, object args)
//    {
//        stats.SetValue(MCStatTypes.W4L, LevelForExperience(W4E), false);
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
//        stats.SetValue(MCStatTypes.W4L, level, false);
//        stats.SetValue(MCStatTypes.W4E, ExperienceForLevel(level), false);
//    }
//    #endregion
//}
