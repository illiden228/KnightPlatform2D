using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private float _durationColor = 1f;
    [SerializeField] private float time = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        if (time <= _durationColor)
        {
            time += Time.deltaTime;
            float normalizeTime = time / _durationColor;
            GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, _normalColor, normalizeTime);
        }
    }

    public void TakeDamage(float damage)
    {
        if (_health > 0)
        {
            _health -= damage;
        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log(gameObject + " Helth: " + _health + ", Take Demage: " + damage);
        GetComponent<SpriteRenderer>().color = Color.gray;
        time = 0;
    }

}
