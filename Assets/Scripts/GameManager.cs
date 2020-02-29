using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static float HealthPointSpirit { get; private set; }
    public static float ManaPointSpirit { get; private set; }
    public static float SwordDamagePointSpirit { get; private set; }
    public static float MagicDamagePointSpirit { get; private set; }
    public static float MagicCooldownPointSpirit { get; private set; }

    [SerializeField] public static float CountSpirit = 0;

    public float HealthMinBar { get; private set; }
    public float ManaMinBar { get; private set; }
    public float SwordDamageMinBar { get; private set; }
    public float MagicDamageMinBar { get; private set; }
    public float MagicCooldownMinBar { get; private set; }

    public float HealthMaxBar { get; private set; }
    public float ManaMaxBar { get; private set; }
    public float SwordDamageMaxBar { get; private set; }
    public float MagicDamageMaxBar { get; private set; }
    public float MagicCooldownMaxBar { get; private set; }

    public float Health { get; private set; }
    public float Mana { get; private set; }
    public float SwordDamage { get; private set; }
    public float MagicDamage { get; private set; }
    public float MagicCooldown { get; private set; }

    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        HealthMinBar = 20f;
        ManaMinBar = 20f;
        SwordDamageMinBar = 2f;
        MagicDamageMinBar = 4f;
        MagicCooldownMinBar = 15f;

        HealthMaxBar = 100f;
        ManaMaxBar = 100f;
        SwordDamageMaxBar = 10f;
        MagicDamageMaxBar = 17f;
        MagicCooldownMaxBar = 5f;

        HealthPointSpirit = 0;
        ManaPointSpirit = 0;
        SwordDamagePointSpirit = 0;
        MagicDamagePointSpirit = 0;
        MagicCooldownPointSpirit = 0;

        Characteristics();
    }

    void Update()
    {
        
    }

    public void TakeSpirit()
    {
        CountSpirit++;
        Debug.Log(CountSpirit);
    }

    public void Characteristics()
    {
        Health = HealthMinBar + (HealthMaxBar - HealthMinBar) / 100 * HealthPointSpirit;
        Mana = ManaMinBar + (ManaMaxBar - ManaMinBar) / 100 * ManaPointSpirit;
        SwordDamage = SwordDamageMinBar + (SwordDamageMaxBar - SwordDamageMinBar) / 100 * SwordDamagePointSpirit;
        MagicDamage = MagicDamageMinBar + (MagicDamageMaxBar - MagicDamageMinBar) / 100 * MagicDamagePointSpirit;
        MagicCooldown = MagicCooldownMinBar + (MagicCooldownMaxBar - MagicCooldownMinBar) / 100 * MagicCooldownPointSpirit;
    }
}
