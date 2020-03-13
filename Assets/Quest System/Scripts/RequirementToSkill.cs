using System.Collections.Generic;
using UnityEngine;

public class RequirementToSkill : MonoBehaviour, IRequirement
{
    static private Dictionary<string, string> skilsTranslate = new Dictionary<string, string>
    {
        { "Hels", "здоровья"},
        { "Streng", "силы" },
        { "Mana", "маны" },
        { "Intel", "интеллекта" }
    };

    private enum SkillItem { Hels, Streng, Mana, Intel };

    [SerializeField] private SkillItem skill;
    [SerializeField] private int value;
    [SerializeField] public string Notification { get { return NoticGenerate(); } }

    public bool IsComplete()
    {
        var actualValue = 0;
        switch (skill)
        {
            case SkillItem.Hels:
                actualValue = FindObjectOfType<SystemPumping>().health;
                break;
            case SkillItem.Intel:
                actualValue = FindObjectOfType<SystemPumping>().intelligence;
                break;
            case SkillItem.Mana:
                actualValue = FindObjectOfType<SystemPumping>().mana;
                break;
            case SkillItem.Streng:
                actualValue = FindObjectOfType<SystemPumping>().strength;
                break;
        }
        return actualValue >= value;
    }

    private string NoticGenerate()
    {
        return string.Format(
            "Недостаточный уровень {0} (необходимо: {1})", 
            skilsTranslate[skill.ToString()], 
            value
            );
    }
}