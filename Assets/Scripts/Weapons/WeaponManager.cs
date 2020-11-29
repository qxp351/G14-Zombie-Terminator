using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponManager : MonoBehaviour
{
    public enum Weapon { pistol, rifle, bag }

    int m_currentWeapon = 0;
    [SerializeField] List<WeaponSwapBob> m_weapons = new List<WeaponSwapBob>();
    [SerializeField] List<WeaponIKs> m_IKs = new List<WeaponIKs>();

    public static WeaponManager current;
    private void Awake() => current = this;

    public void SwapWeapon(Weapon weapon)
    {
        var value = (int)weapon;
        UnEquipWeapon(m_currentWeapon);
        EquipWeapon(value);
        m_currentWeapon = value;
    }

    void UnEquipWeapon(int weapon)
    {
        m_weapons[weapon].SwapOut();
        m_IKs[weapon].TurnOff();
    }

    void EquipWeapon(int weapon)
    {
        m_weapons[weapon].gameObject.SetActive(true);
        m_weapons[weapon].SwapIn();
        m_IKs[weapon].TurnOn();
    }

    [System.Serializable]
    private class WeaponIKs
    {
        public TwoBoneIKConstraint left, right;

        public WeaponIKs(TwoBoneIKConstraint left, TwoBoneIKConstraint right)
        {
            this.left = left;
            this.right = right;
        }

        public void TurnOn()
        {
            left.weight = 1f;
            right.weight = 1f;
        }

        public void TurnOff()
        {
            left.weight = 0f;
            right.weight = 0f;
        }
    }
}
