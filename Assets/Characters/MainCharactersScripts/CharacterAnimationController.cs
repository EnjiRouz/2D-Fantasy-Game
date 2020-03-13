using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void TargetIsDead();

public class CharacterAnimationController : MonoBehaviour {

    // переменные для дальней атаки
    [SerializeField]
    private Transform throwsPosition; //позиция, из которой запускается объект (откуда начинает лететь стрела)
    [SerializeField]
    private GameObject[] throwsPrefab; // объект, который используется для дальней атаки (стрела/камень/нож и т.д.)

    [SerializeField]
    public BoxCollider2D weaponCollider;

    /// переменые для передвижения
    public float moveSpeed = 10f;
    private bool isFacingRight = true;
    public static Animator anim;

    /// переменные для прыжка
    private bool isGrounded;
    public Vector2 jumpHeight;

    /// переменные, отвечающие за здоровье, ману, наносимый персонажу урон и смерть
    [SerializeField]
    private Stats health;

    [SerializeField]
    private Stats mana;

    float cooldownTimer;
    float timeToRegeneration = 7.0f;

    [SerializeField]
    private List<string> canTakeDamageFrom;

    public bool isTakingDamage { get; set; }
    public bool isDead
    {
        get
        {
            if (health.CurrentValue <= 0)
            {
                IsDead();
            }
            return health.CurrentValue <= 0;
        }
    }
    public event TargetIsDead Dead;

    /// самая главная часть для управления анимациями - получение анииматора
    private void Start()
    {
        health.Initialize(FindObjectOfType<SystemPumping>().FunctionHels(), FindObjectOfType<SystemPumping>().FunctionHels());
        mana.Initialize(FindObjectOfType<SystemPumping>().MaxMana(), FindObjectOfType<SystemPumping>().MaxMana());
        anim = GetComponent<Animator>();
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
    }

    /// Update (обновления состояний: ходьба, прыжок, атака)
    void Update()
    {
        if (!isDead)
        {
            if (!isTakingDamage)
            {
                if (anim.GetBool("StopMovement") == false)
                {
                    Walk();
                    if (isGrounded && Input.GetKeyDown(KeyCode.Space))
                    {
                        Jump();
                        isGrounded = false;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        Attack();
                    }
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        FindObjectOfType<WindowPumping>().ShowWindow();
                        anim.SetBool("StopMovement", true);
                    }
                    // регенерация маны
                    if (mana.CurrentValue == 0 || mana.CurrentValue != mana.MaxValue)
                    {
                        cooldownTimer += Time.deltaTime;
                        if (cooldownTimer >= timeToRegeneration)
                        {
                            cooldownTimer = 0;
                            mana.CurrentValue += FindObjectOfType<SystemPumping>().mana*5;
                        }
                    }
                }
            }
        }
    }

    /// ходьба с поворотами на 180
    public void Walk()
    {
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * moveSpeed,
                                               GetComponent<Rigidbody2D>().velocity.y);
        if (move > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (move < 0 && isFacingRight)
        {
            Flip();
        }
    }

    /// прыжок
    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);
        anim.SetBool("Jump", true);
    }

    /// атака
    public void Attack()
    {
        //ищем игрока
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //получаем его инвентарь
        Inventory inventoryPlayer = player.GetComponent<Inventory>();
        anim.SetTrigger("Attack");
        //Если на нас вообще что-то надето
        if (inventoryPlayer.currentWeapon != null)
        {
            if (inventoryPlayer.currentWeapon.type == ItemType.Bow)
            {
                anim.SetTrigger("BowShot");
               // ThrowObject(throwsPrefab[0]);
            }
            if (inventoryPlayer.currentWeapon.type == ItemType.OneHandedWeapon && inventoryPlayer.currentWeapon.name == "Wand")
            {
                if (mana.CurrentValue > 0)
                {
                    anim.SetTrigger("Cast");
                    ThrowObject(throwsPrefab[1]);
                    mana.CurrentValue -= FindObjectOfType<SystemPumping>().UsedMana();
                }
            }
        }
    }

    /// бросание объекта (стрелы у лука/ножи и т.д.)
    public void ThrowObject(GameObject prefab)
    {
        if (isFacingRight)
        {
            GameObject tmp = Instantiate(prefab, throwsPosition.position, Quaternion.identity);
            tmp.GetComponent<Throws>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = Instantiate(prefab, throwsPosition.position, Quaternion.Euler(0, 180, 0));
            tmp.GetComponent<Throws>().Initialize(Vector2.left);
        }
    }

    /// поворот на 180
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    /// GroundCheck (проверка на нахождение на земле, НЕОБХОДИМО для нормального прыжка, 
    /// иначе можно бесконечно прыгать вверх и улететь со сцены)
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.SetBool("Jump", false);
        }
    }

    /// получение урона
    public IEnumerator Damage()
    {
        health.CurrentValue -= FindObjectOfType<Damage>().Damege();
        if (!isDead)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            anim.SetTrigger("Death");
            yield return null;
        }
    }

    /// соприкосновение с оружием врага (таким образом получается урон)
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (canTakeDamageFrom.Contains(other.tag))
        {
            StartCoroutine(Damage());
        }
    }

    /// триггер для фиксации смерти игрока (нужно для передачи этих данных врагу, 
    /// чтобы он перестал бить труп :D)
    public void IsDead()
    {
        if (Dead != null)
        {
            Dead();
            anim.SetTrigger("Death");
        }
    }
}