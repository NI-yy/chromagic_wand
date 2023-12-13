using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;
using KoitanLib;
using UnityEngine.UI;
using System.Linq;

public class Player : MonoBehaviour
{
    [Header("デッドゾーン")] public float deadZone;

    [Header("重力")] public float gravity;

    [Header("地上最高スピード")] public float SpeedGroundMax;
    [Header("地上加速度")] public float accelerationGround;
    [Header("空中最高スピード")] public float SpeedAirMax;
    [Header("空中加速度")] public float accelerationAir;
    [Header("ジャンプ最低高度")] public float jumpHeightMin;
    [Header("ジャンプ最高高度")] public float jumpHeightMax;
    [Header("ジャンプ最大時間")] public float jumpTimeMax;
    [Header("2段ジャンプ")] public bool beAbleToDoubleJump;

    //public GameObject canvasGame;

    private GameObject gameManagerObj;
    //private GameManager gameManagerScript;
    //private SoundManager soundManagerScript;
    private PlayerHP playerHPControllerScript;

    public GroundCheck ground;
    public GroundCheck head;
    public GameObject PlayerHPController;
    public GameObject UI_ColorOrb;
    public GameObject bullet;
    public GameObject _TwoPlayerManager;
    public GameObject soundManager;

    public GameObject particleSystem_electric;
    public GameObject particleSystem_electricBall;
    public GameObject particleSystem_fire;
    public GameObject particleSystem_fire_2;
    public GameObject particleSystem_leaf;
    public GameObject particleSystem_water;
    public GameObject particleSystem_soil;


    //honebone追加
    public bool lookRight;
    public GameObject projector;
    public PlayerProjectorData projectorData;


    private Animator anim = null;
    private Rigidbody2D rb = null;


    private bool isOnGround = false;
    private bool isOnHead = false;

    private float horizontalKeyRaw;
    private float verticalKeyRaw;
    private bool spaceKey;
    private bool spaceKeyDown;
    private Vector3 moveKeyVec;


    private Vector3 playerScale;
    private Vector3 initialPosition;
    private float jumpingTimeCount;
    private bool jumped = false;
    public float mass;

    //敵からの攻撃判定関係
    private string enemyTag = "Enemy";
    private string bulletTag = "bullet";
    private float initialForce;

    //2段ジャンプ関係
    private bool afterFirstJump = false; //1回ジャンプした後かどうか。これがtrueの時のみ2段ジャンプ可能

    //攻撃色関係
    private TwoPlayerManager twoPlayerManagerScript;

    private bool attackFlag = true; //1回攻撃するとクールタイム有

    //長押し攻撃判定
    private bool buttonDownFlag = false;
    private float buttonDownTime = 0f;
    [SerializeField] float strongAttakTimeTh = 2.0f;
    private bool enableStrongAttack = false;

    
    public int invincibility_frame = 3; //ノックバック時、何秒間点滅させるか(無敵時間に等しい)
    public float brinking_cycle = 0.1f; //点滅周期
    private bool isInvincible = false; //無敵時間内かどうか。true時、攻撃を受けない
    [Tooltip("Attach all parts of player here.")]
    public GameObject[] player_parts;





    /////////////////////////　　　イベント関数　　　////////////////////////////


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        playerScale = transform.localScale;
        initialPosition = transform.position;
        mass = rb.mass;

        //gameManagerObj = GameObject.Find("GameManager");
        //gameManagerScript = gameManagerObj.GetComponent<GameManager>();
        //soundManagerScript = gameManagerObj.GetComponent<SoundManager>();
        playerHPControllerScript = PlayerHPController.GetComponent<PlayerHP>();

        twoPlayerManagerScript = _TwoPlayerManager.GetComponent<TwoPlayerManager>();

        
    }


    void Update()
    {
        GetKeysInput();
        isOnGround = ground.IsOnGround();
        isOnHead = head.IsOnGround();

        if (buttonDownFlag)
        {
            buttonDownTime += Time.deltaTime;
        }
        
        if(buttonDownTime >= strongAttakTimeTh)
        {
            enableStrongAttack = true;
            twoPlayerManagerScript.MixColor();
            Debug.Log("MixColor");
        }
    }

    private void FixedUpdate()
    {
        
        if (isOnGround)
        {
            //soundManager.GetComponent<SoundManager>().StartPlayingWalkSE();
            if (jumped)
            {
                //anim.SetBool("jumpDown", true);
                jumped = false;
            }

            anim.SetBool("onGround", true);
            anim.SetBool("jumpUp", false);

            ManageXMoveGround();
            ManageYMoveGround();

        }
        else
        {
            //soundManager.GetComponent<SoundManager>().StopPlayingWalkSE();
            anim.SetBool("onGround", false);
            //anim.SetBool("jumping", false);
            anim.SetBool("running", false);

            ManageXMoveAir();
            ManageYMoveAir();

        }
        
        ResetKeyDown();


    }





    /////////////////////////　　　システム系　　　////////////////////////////

    private void GetKeysInput()
    {
        horizontalKeyRaw = KoitanInput.GetStick(StickCode.LeftStick).x;
        verticalKeyRaw = KoitanInput.GetStick(StickCode.LeftStick).y;

        moveKeyVec = new Vector3(verticalKeyRaw, horizontalKeyRaw);


        if (Input.GetKey(KeyCode.Space) || KoitanInput.Get(ButtonCode.A))
        {
            spaceKey = true;
        }
        else
        {
            spaceKey = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || KoitanInput.GetDown(ButtonCode.A))
        {
            spaceKeyDown = true;
        }

        if (KoitanInput.GetDown(ButtonCode.B) || Input.GetMouseButtonDown(0))
        {
            buttonDownTime = 0f;
            buttonDownFlag = true;
        }

        if(KoitanInput.GetUp(ButtonCode.B) || Input.GetMouseButtonUp(0))
        {
            buttonDownTime = 0f;
            buttonDownFlag = false;
            
            Attack();

            if (enableStrongAttack)
            {
                twoPlayerManagerScript.DevideMixedColor();
            }

            enableStrongAttack = false;
        }
    }

    private void ResetKeyDown()
    {
        spaceKeyDown = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag) && !(isInvincible))
        {
            playerHPControllerScript.ReduceHP();
            isInvincible = true;
            StartCoroutine(KnockBackBlinking());
            isInvincible = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(bulletTag) && !(isInvincible))
        {
            playerHPControllerScript.ReduceHP();
            isInvincible = true;
            StartCoroutine(KnockBackBlinking());
            isInvincible = false;
            Destroy(collision.gameObject);
        }
    }

    IEnumerator KnockBackBlinking()
    {
        for (int i = 0; i < invincibility_frame; i++)
        {
            //ノックバック時の点滅
            yield return new WaitForSeconds(brinking_cycle);
            foreach (GameObject obj in player_parts)
            {
                obj.SetActive(false);
            }
            yield return new WaitForSeconds(brinking_cycle);
            foreach (GameObject obj in player_parts)
            {
                obj.SetActive(true);
            }
        }
        
    }

    //honebone : 自分が出した弾にあたって速攻GameOverになるのでいったんコメントアウトしてます
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag(bulletTag))
    //    {
    //        playerHPControllerScript.ReduceHP();
    //    }
    //}





    /////////////////////////　　　動き　　　////////////////////////////


    private void ManageXMoveGround()
    {
        Vector3 newScale = playerScale;

        //動摩擦係数＝MaxスピードでAddForceと釣り合う値
        float deceleration = accelerationGround * mass / SpeedGroundMax;

        //if (Mathf.Abs(horizontalKeyRaw) > deadZone)
        //{
        //    soundManager.GetComponent<SoundManager>().StartPlayingWalkSE();
        //}

        if (horizontalKeyRaw > deadZone)
        {
            anim.SetBool("running", true);
            transform.localScale = newScale;
            lookRight = true;

            //x方向に加速度*質量の力を加える
            rb.AddForce(new Vector2(1, 0) * accelerationGround * mass);

            //x方向の速度に従って摩擦が働く
            rb.AddForce(new Vector2(rb.velocity.x, 0) * -1 * deceleration);
        }
        else if (horizontalKeyRaw < -deadZone)
        {
            //soundManager.GetComponent<SoundManager>().StartPlayingWalkSE();
            anim.SetBool("running", true);

            //左右の向きを変える
            newScale.x = -newScale.x;
            transform.localScale = newScale;
            lookRight = false;


            //-x方向に加速度*質量の力を加える
            rb.AddForce(new Vector2(-1, 0) * accelerationGround * mass);

            //x方向の速度に従って摩擦が働く
            rb.AddForce(new Vector2(rb.velocity.x, 0) * -1 * deceleration);
        }
        else
        {
            //soundManager.GetComponent<SoundManager>().StopPlayingWalkSE();
            anim.SetBool("running", false);


            //x方向の速度に従って摩擦が働く
            rb.AddForce(new Vector2(rb.velocity.x, 0) * -1 * deceleration * 10);

            //ｘ軸方向のスピードの絶対値が極小（SpeedMaxの1/100）になったら０にする
            if (Mathf.Abs(rb.velocity.x) < SpeedGroundMax * 0.01f)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

        }

    }

    private void ManageXMoveAir()
    {

        Vector3 newScale = playerScale;
        float mass = rb.mass;

        //空気抵抗係数＝MaxスピードでAddForceと釣り合う値
        float deceleration = accelerationAir * mass / SpeedAirMax;

        if (horizontalKeyRaw > deadZone)
        {
            transform.localScale = newScale;
            lookRight = true;

            //x方向に加速度*質量の力を加える
            rb.AddForce(new Vector2(1, 0) * accelerationAir * mass);

            //x方向の速度に従って空気抵抗が働く
            rb.AddForce(new Vector2(rb.velocity.x, 0) * -1 * deceleration);
        }
        else if (horizontalKeyRaw < -deadZone)
        {
            anim.SetBool("running", true);

            //左右の向きを変える
            newScale.x = -newScale.x;
            transform.localScale = newScale;
            lookRight = false;


            //-x方向に加速度*質量の力を加える
            rb.AddForce(new Vector2(-1, 0) * accelerationAir * mass);

            //x方向の速度に従って空気抵抗が働く
            rb.AddForce(new Vector2(rb.velocity.x, 0) * -1 * deceleration);
        }
        else
        {
            anim.SetBool("running", false);


            //x方向の速度に従って摩擦が働く
            rb.AddForce(new Vector2(rb.velocity.x, 0) * -1 * deceleration);

            //ｘ軸方向のスピードの絶対値が極小（SpeedMaxの1/100）になったら０にする
            if (Mathf.Abs(rb.velocity.x) < SpeedAirMax * 0.01f)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    private void ManageYMoveGround()
    {
        //2段ジャンプのために変数のスコープ変更_yy
        //float initialForce = Mathf.Sqrt(gravity * jumpHeightMin * 2) * mass;
        initialForce = Mathf.Sqrt(gravity * jumpHeightMin * 2) * mass;

        //地面上でスペースキーが押下されたとき、上方向に力を加えることでジャンプする.同時に時間計測が始まる
        if (spaceKeyDown)
        {
            rb.AddForce(new Vector2(0, 1) * initialForce, ForceMode2D.Impulse);
            jumpingTimeCount = 0f;
            
            afterFirstJump = true;

            if (spaceKeyDown)
            {
                soundManager.GetComponent<SoundManager>().PlayJumpSe();
                anim.SetBool("jumpUp", true);
                jumped = true;
            }
        }

        rb.AddForce(new Vector2(0, -1) * gravity);


    }

    private void ManageYMoveAir()
    {
        float jumpingForce = (1 - 1 / jumpHeightMax) * gravity * mass * 1.5f;

        if (spaceKey && jumpingTimeCount < jumpTimeMax && !isOnHead)
        {
            rb.AddForce(new Vector2(0, 1) * jumpingForce);
            jumpingTimeCount += Time.deltaTime;
            
            
        }
        else if ((spaceKeyDown || KoitanInput.GetDown(ButtonCode.A)) && afterFirstJump && beAbleToDoubleJump)
        {
            if (spaceKeyDown)
            {
                soundManager.GetComponent<SoundManager>().PlayJumpSe();
                anim.SetBool("jumpUp", true);
                anim.SetBool("jumpUp", false);
                jumped = true;
            }

            //2段ジャンプ
            rb.AddForce(new Vector2(0, 1) * initialForce, ForceMode2D.Impulse);
            jumpingTimeCount = 0f;
            
            afterFirstJump = false;
        }

        rb.AddForce(new Vector2(0, -1) * gravity);
    }

    private void Attack()
    {
        if (attackFlag)
        {

            attackFlag = false;
            Color color_wand = UI_ColorOrb.GetComponent<Image>().color;
            //string wandColorString = twoPlayerManagerScript.GetWandColor().ToStr();
            TwoPlayerManager.WandColor wandColor = twoPlayerManagerScript.GetWandColor();
            Debug.Log("杖の色: " + wandColor);
            //GameObject currentBullet = Instantiate(bullet, this.transform.position + new Vector3(0f, 4.0f, 0f), Quaternion.identity);
            //currentBullet.GetComponent<SpriteRenderer>().color = color_wand;
            //currentBullet.GetComponent<bulletController>().SetBulletColor(wandColorString);

            Vector3 dir = Vector3.right;
            if (!lookRight) { dir = Vector3.left; }


            StartCoroutine(ParticleAttack(wandColor, dir, color_wand));
        }
    }

    private IEnumerator ParticleAttack(TwoPlayerManager.WandColor wandColor, Vector3 dir, Color color_wand)
    {
        if(wandColor == TwoPlayerManager.WandColor.White || wandColor == TwoPlayerManager.WandColor.Black)
        {
            Debug.Log("白か黒だよ");
            attackFlag = true;
            yield return null;
        }
        else
        {
            anim.SetBool("attack", true);
            Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, dir);
            if (wandColor == TwoPlayerManager.WandColor.Red)
            {
                anim.SetBool("redAttack", true);
                yield return new WaitForSeconds(0.2f);

                if (lookRight)
                {
                    var p = Instantiate(particleSystem_fire, transform.position + new Vector3(0f, 4.0f, 0f), quaternion);
                    Destroy(p, 1.0f);
                }
                else
                {
                    var p = Instantiate(particleSystem_fire, transform.position + new Vector3(0f, 4.0f, 0f), Quaternion.Euler(0, 180, 0));
                    Destroy(p, 1.0f);
                }

                soundManager.GetComponent<SoundManager>().PlayFireSe();

                yield return new WaitForSeconds(0.8f); //1つ目のアニメーション終了待ち


                anim.SetBool("toAttackFire2", true);
                yield return new WaitForSeconds(0.1f);
                if (lookRight)
                {
                    var p = Instantiate(particleSystem_fire_2, transform.position + new Vector3(0f, 4.0f, 0f), quaternion);
                    Destroy(p, 1.0f);
                }
                else
                {
                    var p = Instantiate(particleSystem_fire_2, transform.position + new Vector3(0f, 4.0f, 0f), Quaternion.Euler(0, 180, 0));
                    Destroy(p, 1.0f);
                }

                soundManager.GetComponent<SoundManager>().PlayFireSe();

                StartCoroutine(ResetAnimFlag("redAttack"));
                StartCoroutine(ResetAnimFlag("toAttackFire2"));
            }
            else if (wandColor == TwoPlayerManager.WandColor.Green)
            {
                anim.SetBool("greenAttack", true);
                yield return new WaitForSeconds(0.3f);

                var p = Instantiate(particleSystem_leaf, transform.position + new Vector3(0f, 4.0f, 0f), Quaternion.identity);
                Destroy(p, 0.7f);

                soundManager.GetComponent<SoundManager>().PlayWindSe();

                StartCoroutine(ResetAnimFlag("attack"));
                StartCoroutine(ResetAnimFlag("greenAttack"));
            }
            else if (wandColor == TwoPlayerManager.WandColor.Blue)
            {
                anim.SetBool("blueAttack", true);
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ResetAnimFlag("attack"));
                StartCoroutine(ResetAnimFlag("blueAttack"));
                if (lookRight)
                {
                    var p = Instantiate(particleSystem_water, transform.position + new Vector3(7f, 6.0f, 0f), Quaternion.identity);
                    p.GetComponent<AttackWaterController>().isRight = true;
                    p.GetComponent<AttackWaterController>().ActiveBulletCollider();
                }
                else
                {
                    var p = Instantiate(particleSystem_water, transform.position + new Vector3(-7f, 6.0f, 0f), Quaternion.Euler(0, 180, 0));
                    p.GetComponent<AttackWaterController>().isRight = false;
                    p.GetComponent<AttackWaterController>().ActiveBulletCollider();
                }

                soundManager.GetComponent<SoundManager>().PlayWaterSe();
            }
            else if (wandColor == TwoPlayerManager.WandColor.Orange)
            {
                anim.SetBool("orangeAttack", true);
                yield return new WaitForSeconds(1f);

                if (lookRight)
                {
                    var p = Instantiate(particleSystem_soil, transform.position + new Vector3(5f, 7.0f, 0f), Quaternion.identity);
                    Destroy(p, 1.0f);
                }
                else
                {
                    var p = Instantiate(particleSystem_soil, transform.position + new Vector3(-5f, 7.0f, 0f), Quaternion.Euler(0, 180, 0));
                    Destroy(p, 1.0f);
                }

                soundManager.GetComponent<SoundManager>().PlayRockSe();

                StartCoroutine(ResetAnimFlag("attack"));
                StartCoroutine(ResetAnimFlag("orangeAttack"));
            }
            else if (wandColor == TwoPlayerManager.WandColor.Yellow)
            {
                anim.SetBool("yellowAttack", true);
                yield return new WaitForSeconds(0.3f);

                var p = Instantiate(particleSystem_electric, transform.position + new Vector3(0f, 4.0f, 0f), Quaternion.identity);

                var p_ball = Instantiate(projector, transform.position + new Vector3(0f, 4.0f, 0f), Quaternion.identity);
                p_ball.GetComponent<PlayerProjector>().Init(projectorData, dir, color_wand);

                Destroy(p, 0.7f);

                soundManager.GetComponent<SoundManager>().PlayThunderSe();

                StartCoroutine(ResetAnimFlag("yellowAttack"));
            }

            StartCoroutine(ResetAnimFlag("attack"));

            yield return new WaitForSeconds(2.0f);
            attackFlag = true;
        }
        
    }

    //void ResetFlag(string flagName)
    //{
    //    anim.SetBool(flagName, false);
    //}

    private IEnumerator ResetAnimFlag(string flagName)
    {
        yield return new WaitForSeconds(1f);

        anim.SetBool(flagName, false);
    }
}