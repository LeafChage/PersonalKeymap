using System;
using UnityEngine;
using PersonalKeymap;

[Serializable]
public class Mapping
{
    public Keymap Jump;
    public Keymap Front;
    public Keymap Attack;
    public Keymap SpecialAttack;
}

public static class CustomInput
{
    private static KeymapSetting<Mapping> keymapSetting;
    public static Mapping Mapping => keymapSetting.Mapping;

    static CustomInput()
    {
        keymapSetting = new KeymapSetting<Mapping>(new StandardJar());
    }
}

public class Demo : MonoBehaviour
{
    private void Update()
    {
        if (CustomInput.Mapping.Jump.On())
        {
            Debug.Log("Jump");
        }
        if (CustomInput.Mapping.Front.On())
        {
            Debug.Log("Front");
        }
        if (CustomInput.Mapping.Attack.On())
        {
            Debug.Log("Attack");
        }
        if (CustomInput.Mapping.SpecialAttack.On())
        {
            Debug.Log("SpecialAttack");
        }
    }
}