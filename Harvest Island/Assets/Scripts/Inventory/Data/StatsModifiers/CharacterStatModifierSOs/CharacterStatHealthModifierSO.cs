using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharStatHealthModSO")]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float value)
    {
        HealthBar healthBar = GameObject.FindObjectOfType<HealthBar>();
        if(healthBar != null) healthBar.ChangeHealth(value);
    }
}
