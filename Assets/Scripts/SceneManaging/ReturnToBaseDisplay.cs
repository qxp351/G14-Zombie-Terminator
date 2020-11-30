using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToBaseDisplay : MonoBehaviour
{
    CanvasGroup m_cg;

    private void OnEnable()
    {
        ReturnToBase.RETURNTOBASE += ReturnToBase_RETURNTOBASE;
        if (!m_cg) m_cg = GetComponent<CanvasGroup>();
    }

    private void OnDisable()
    {
        ReturnToBase.RETURNTOBASE -= ReturnToBase_RETURNTOBASE;
    }

    private void ReturnToBase_RETURNTOBASE()
    {
        m_cg.alpha = 1f;
        m_cg.interactable = true;
        m_cg.blocksRaycasts = true;
    }

    public void Return()
    {
        SceneManager.LoadScene(SceneIndices.Base);
    }

    public void Stay()
    {
        m_cg.alpha = 0f;
        m_cg.interactable = false;
        m_cg.blocksRaycasts = false;
        PlayerInput.current.TogglePlayerControl(true);
    }
}
