using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class BowProficiency : MonoBehaviour
//{
//    #region Consts
//    public const int minLevel = 1;
//    public const int maxLevel = 10;
//    public const int maxExperience = 920;
//    #endregion

//    #region Fields/Properties
//    public int W1L { get { return stats[MCStatTypes.W1L]; } }
//    public int W1E { get { return stats[MCStatTypes.W1E]; } set { stats[MCStatTypes.W1E] = value; } }
//    public float W1EP { get { return (float)(W1L - minLevel) / (float)(maxLevel - minLevel); } }

//    MCStats stats;
//    #endregion

//    #region MonoBehavior

//    private void Awake()
//    {
//        stats = GetComponent<MCStats>();
//    }
//    private void OnEnable()
//    {
//        this.AddObserver(OnExpWillChange, MCStats.WillChangeNotification(MCStatTypes.W1E), stats);
//        this.AddObserver(OnExpDidChange, MCStats.DidChangeNotification(MCStatTypes.W1E), stats);
//    }
//    private void OnDisable()
//    {
//        this.AddObserver(OnExpWillChange, MCStats.WillChangeNotification(MCStatTypes.W1E), stats);
//        this.AddObserver(OnExpDidChange, MCStats.WillChangeNotification(MCStatTypes.W1E), stats);
//    }
//    #endregion

//    #region Event Handlers
//    void OnExpWillChange(object sender, object args)
//    {
//        ValueChangeException vce = args as ValueChangeException;
//        vce.AddModifier(new ClampValueModifier(int.MaxValue, W1E, maxExperience));
//    }

//    void OnExpDidChange(object sender, object args)
//    {
//        stats.SetValue(MCStatTypes.W1L, LevelForExperience(W1E), false);
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
//        stats.SetValue(MCStatTypes.W1L, level, false);
//        stats.SetValue(MCStatTypes.W1E, ExperienceForLevel(level), false);
//    }
//    #endregion

//}
