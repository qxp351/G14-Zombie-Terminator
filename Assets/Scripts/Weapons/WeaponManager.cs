using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponManager : MonoBehaviour
{
    public enum Weapon { pistol, rifle, bag, launcher }

    int m_currentWeapon = 0;
    public static Weapon CurrentWeapon;
    [SerializeField] List<GameObject> m_weapons = new List<GameObject>();
    [SerializeField] List<WeaponIKs> m_IKs = new List<WeaponIKs>();

    [Header("Flashlight")]
    [SerializeField] GameObject m_flashlight = null;

    public static WeaponManager current;
    private void Awake() => current = this;

    private void Start()
    {
        for (int i = 0; i < m_weapons.Count; i++)
        {
            if (m_weapons[i].activeSelf)
            {
                m_currentWeapon = i;
                CurrentWeapon = (Weapon)i;
                break;
            }
        }
        SwapWeapon(CurrentWeapon);
    }

    public void SwapWeapon(Weapon weapon)
    {
        var value = (int)weapon;
        UnEquipWeapon(m_currentWeapon);
        EquipWeapon(value);
        m_currentWeapon = value;
        CurrentWeapon = (Weapon)m_currentWeapon;
    }

    public void OpenInventory()
    {
        UnEquipWeapon(m_currentWeapon);
        EquipWeapon((int)Weapon.bag);
    }
    public void CloseInventory()
    {
        UnEquipWeapon((int)Weapon.bag);
        EquipWeapon(m_currentWeapon);
    }

    void UnEquipWeapon(int weapon)
    {
        m_weapons[weapon].gameObject.SetActive(false);
        m_IKs[weapon].TurnOff();
    }

    void EquipWeapon(int weapon)
    {
        m_weapons[weapon].gameObject.SetActive(true);
        m_IKs[weapon].TurnOn();
    }

    public void EquipFlashlight()
    {
        m_flashlight.SetActive(true);
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
