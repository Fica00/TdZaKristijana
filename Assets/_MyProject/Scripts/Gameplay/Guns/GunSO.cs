using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewGun", menuName = "ScriptableObject/Gun")]
public class GunSO : ScriptableObject
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float HatColor { get; private set; }
    [field: SerializeField] public Bullet Bullet { get; private set; }
    [field: SerializeField] public List<int> AmountOfBullets { get; private set; }
    [field: SerializeField] public List<int> AmountOfClips { get; private set; }
    [field: SerializeField] public List<float> ReloadSpeed { get; private set; }
    [field: SerializeField] public List<float> Cooldown { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public Sprite UIPreview { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public List<float> UpgradeCost { get; private set; }
    [field: SerializeField] public int UnclockCost { get; private set; }
    [field: SerializeField] public int Spread { get; private set; }
    [field: SerializeField] public AudioClip Sound { get; private set; }

    protected bool canFire;

    private static List<GunSO> allGuns;

    public static void Init()
    {
        allGuns = Resources.LoadAll<GunSO>("Guns/").ToList();
        Debug.Log(allGuns.Count);
    }

    public static GunSO Get(int _id)
    {
        return allGuns.Find(_element => _element.Id == _id);
    }

    public static List<GunSO> Get()
    {
        return allGuns;
    }
}
