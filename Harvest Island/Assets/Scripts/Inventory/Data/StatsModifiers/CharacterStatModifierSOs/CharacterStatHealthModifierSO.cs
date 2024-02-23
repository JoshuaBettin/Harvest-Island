using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharStatHealthModSO")]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float value)
    {
        HealthBar healthBar = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>();
        Debug.Log("Heal");
        if(healthBar != null) healthBar.HealHealth(value);
    }
}
