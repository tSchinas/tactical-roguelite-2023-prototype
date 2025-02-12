using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class StatPanel : MonoBehaviour
{
    public PanelMove panel;
    public Sprite allyBackground;
    public Sprite enemyBackground;
    public Image background;
    public Image avatar;
    public Text nameLabel;
    public Text hpLabel;
    public Text apLabel;

    public void Display(GameObject obj)
    {
        Alliance alliance = obj.GetComponent<Alliance>();
        background.sprite = alliance.allianceType == Alliances.Enemy ? enemyBackground : allyBackground;
        // avatar.sprite = null; Need a component which provides this data
        nameLabel.text = obj.name;
        Stats stats = obj.GetComponent<Stats>();
        if (stats)
        {
            hpLabel.text = string.Format("HP {0} / {1}", stats[StatTypes.HP], stats[StatTypes.MHP]);
            apLabel.text = string.Format("AP {0} / {1}", stats[StatTypes.AP], stats[StatTypes.MAP]);
        }
    }
}