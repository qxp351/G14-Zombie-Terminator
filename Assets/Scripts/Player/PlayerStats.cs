using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    public static event Action HEAL;
    public static event Action DAMAGE;
    public static event Action<int> HEALTH;
    public static event Action DEATH;

    [Header("On Death Properties")]
    [SerializeField] GameObject deathModel = null;
    [SerializeField] GameObject deathCam = null;

    [Header("Audio Properties")]
    [SerializeField] List<AudioClip> m_hurtSounds = new List<AudioClip>();

    public static PlayerStats current;
    private void Awake() => current = this;

    public override void Heal(int amount)
    {
        base.Heal(amount);
        HEAL?.Invoke();
        HEALTH?.Invoke(health.x);
    }
    public override void Damage(int amount)
    {
        base.Damage(amount);
        DAMAGE?.Invoke();
        HEALTH?.Invoke(health.x);
        try
        {
            PlayerInput.current.audios.PlayOneShot(m_hurtSounds[UnityEngine.Random.Range(0, m_hurtSounds.Count)]);
        }
        catch
        {
            Debug.LogWarning("Either player input or m_hurtSound does not exist.");
        }
    }

    protected override IEnumerator Die()
    {
        PlayerInput.current.TogglePlayerControl(true);
        yield return null;

        GetComponentInChildren<WeaponManager>().gameObject.SetActive(false);
        deathCam.SetActive(true);
        deathModel.SetActive(true);
        DEATH?.Invoke();

        yield return new WaitForSeconds(5f);
        PlayerInput.InvokeDeath();

        yield break;
    }
}
