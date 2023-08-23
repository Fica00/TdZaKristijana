using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewAbility", menuName = "ScriptableObject/Ability")]
public class AbilitiesSO : ScriptableObject
{
    [field: SerializeField] public int Id;
    [field: SerializeField] public int Cost;
    [field: SerializeField] public Sprite Sprite;

    static List<AbilitiesSO> allAbilities;

    public static void Init()
    {
        allAbilities = Resources.LoadAll<AbilitiesSO>("Abilities/").ToList();
    }

    public static AbilitiesSO Get(int _id)
    {
        return allAbilities.Find(element => element.Id == _id);
    }

    public static List<AbilitiesSO> Get()
    {
        return allAbilities;
    }
}
