using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor_HeroKnight : MonoBehaviour
{
    private int m_ColCount = 0;
    private float m_DisableTimer;

    private void OnEnable()
    {
        m_ColCount = 0;
    }

    public bool State()
    {
        if (m_DisableTimer > 0)
            return false;
        return m_ColCount > 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar la capa del objeto colisionado (reemplaza "Ground" con el nombre de tu capa)
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_ColCount++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verificar la capa del objeto colisionado (reemplaza "Ground" con el nombre de tu capa)
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_ColCount--;
        }
    }

    void Update()
    {
        m_DisableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        m_DisableTimer = duration;
    }
}
