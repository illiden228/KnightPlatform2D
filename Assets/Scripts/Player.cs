using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _body;
    private Animator _animator;

    private float _speed = 5f;
    private float _jumpForce = 5f;
    private bool _isGround = true;
    
    [SerializeField] private float _health;
    [SerializeField] private float _mana;
    [SerializeField] private float _swordDamage;
    [SerializeField] private float _magicDamage;
    [SerializeField] private float _magicCooldown;

    private float _magicPrice;
    private float _attackSpeed = 1;
    private float _lastSwordAttack;
    private float _lastMagicAttack;



    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _lastSwordAttack = 0;
        _lastMagicAttack = 0;
        _magicPrice = 10f;

        _health = GameManager.Instance.Health;
        _mana = GameManager.Instance.Mana;
        _swordDamage = GameManager.Instance.SwordDamage;
        _magicDamage = GameManager.Instance.MagicDamage;
        _magicCooldown = GameManager.Instance.MagicCooldown;
    }

    void Update()
    {
        _animator.SetFloat("Speed", 0f);
        
        if (Input.GetKeyDown(KeyCode.W) && _isGround)
        {
            _isGround = false;
            _body.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            _animator.SetBool("Jump", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<CircleCollider2D>().offset = new Vector2(0.5f, 0f);
            _animator.SetFloat("Speed", 1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-_speed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<CircleCollider2D>().offset = new Vector2(-0.5f, 0f);
            _animator.SetFloat("Speed", 1f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && _lastSwordAttack <= 0 && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Cast"))
        {
            _animator.Play("Attack");
            Debug.Log("Запущен Attack");
            Attack(new Vector2(gameObject.transform.localPosition.x + 0.8f, gameObject.transform.localPosition.y), GetComponent<CircleCollider2D>().radius);
            _lastSwordAttack = _attackSpeed;
        }
        if (Input.GetKeyDown(KeyCode.R) && _lastMagicAttack <= 0 && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            _animator.Play("Cast");
            Debug.Log("Начало каста");
            _lastMagicAttack = _magicCooldown;
        }

        if (_lastSwordAttack > 0)
        {
            _lastSwordAttack -= Time.deltaTime;
        }
        if (_lastMagicAttack > 0)
        {
            _lastMagicAttack -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isGround = true;
        _animator.SetBool("Jump", false);
    }

    private void Attack(Vector2 center, float radius)
    {
        Debug.Log("Вызван Attack");
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        Debug.Log("Полученные коллайдеры: ");
        for (int k = 0; k < hitColliders.Length; k++)
        {
            Debug.Log(hitColliders[k]);
        }
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.GetComponent<Enemy>() != null)
            {
                hitColliders[i].gameObject.GetComponent<Enemy>().TakeDamage(_swordDamage);
            }
            i++;
        }
    }
}
