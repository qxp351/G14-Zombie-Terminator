using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapBob : MonoBehaviour
{
    [SerializeField] CurvedObjectBob m_swapInBob = new CurvedObjectBob();
    [SerializeField] CurvedObjectBob m_swapOutBob = new CurvedObjectBob();
    bool swapIn = false;
    bool swapOut = false;

    private void Start()
    {
        m_swapInBob.Setup(transform, 1f);
    }

    private void Update()
    {
        if (swapIn)
        {
            transform.localPosition = m_swapInBob.DoHeadBob(1f);
            if (m_swapInBob.FinishedCycle())
            {
                swapIn = false;
            }
        }
        else if (swapOut)
        {
            transform.localPosition = m_swapOutBob.DoHeadBob(1f);
            if (m_swapOutBob.FinishedCycle())
            {
                swapOut = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void SwapIn() => swapIn = true;
    public void SwapOut() => swapOut = true;
}
