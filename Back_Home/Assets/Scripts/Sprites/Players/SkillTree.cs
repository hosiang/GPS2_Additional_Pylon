using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField] private bool skill01_DoubleThrust_State = false;
    [SerializeField] private CustomButton thrustButton;

    public bool Skill01_DoubleThrust_State => skill01_DoubleThrust_State;

    public void Skill_Open(Object requireObject, Global.SkillsTree skillsTree)
    {
        if(requireObject.GetType().Name == nameof(PlayerControl))
        {
            skill01_DoubleThrust_State = true;
            thrustButton.SetButtonDoubleClickActive(this, true);
        }
    }
}
