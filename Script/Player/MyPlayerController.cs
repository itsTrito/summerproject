using System.Collections;
using BaseCharacter;
using UnityEngine;

namespace Player
{
    public class MyPlayerController : TypeCharacter
    {
        public static readonly int OnGround = Animator.StringToHash("OnGround");
        private static readonly int Jumping = Animator.StringToHash("Jumping");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Crouch = Animator.StringToHash("Crouch");
        private static readonly int Armed = Animator.StringToHash("Armed");
        private static readonly int AttackType = Animator.StringToHash("AttackType");
        private static readonly int BigAttackStart = Animator.StringToHash("BigAttackStart");
        private static readonly int BigAttackEnd = Animator.StringToHash("BigAttackEnd");

        private int _slide = 0;

        private void Awake()
        {
            character = new Character(4f, 2f, 8f, -8f, 10);
            Sp = GetComponent<SpriteRenderer>();
            An = GetComponent<Animator>();
            Rb = GetComponent<Rigidbody2D>();
            collider = GetComponent<BoxCollider2D>();
            Vel = new Vector2();
            character.SetAttackPossibility(3,2);
        }

        private void FixedUpdate()
        {
            InAir();
            An.SetInteger(AttackType,0);

            //Prendre/Enlever arme
            if (Input.GetKeyDown(KeyCode.Q) && !character.IsJumping && !character.IsCrouch && !character.IsWalk)
            {
                character.IsArmed = !character.IsArmed;
            }

            //Mouvement gauche/droite
            if ((!character.IsCrouch || character.IsCrouch && character.IsWalk) && !character.DoAttack() && character.OnGround)
            {
                if (Input.GetKey(KeyCode.A))
                    Move(true);
                
                if (Input.GetKey(KeyCode.D))
                    Move(false);
            }
            else 
                Stop();

            if (((Input.GetKeyUp(KeyCode.D)) || (Input.GetKeyUp(KeyCode.A))) && character.OnGround)
            {
                Stop();
            }

		void OnCollisionEnter2D(Collision2D collision){
            if (collision.collider.tag == "Ground")
            {
              	character.OnGround = true;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.tag == "Ground")
            {
                character.OnGround = false;
            }
        }

            //Crouch
            if (Input.GetKeyDown(KeyCode.LeftControl) && !character.IsArmed)
            {
                character.IsCrouch = true;
                if (character.IsWalk)
                {
                    collider.size = new Vector2(0.2f, 0.1734726f);
                    collider.offset = new Vector2(0f, -0.09326369f);
                    StartCoroutine(DoSlide());
                }
                else
                {
                    collider.size = new Vector2(0.2f, 0.2184089f);
                    collider.offset = new Vector2(0f, -0.07079558f);
                }
            }
            else
            {
                if (character.IsCrouch && (_slide == 2 || Input.GetKeyUp(KeyCode.LeftControl)))
                {
                    character.IsCrouch = false;
                    _slide = 1;
                    collider.size = new Vector2(0.2f, 0.3f);
                    collider.offset = new Vector2(0f, -0.03f);
                }
            }

            //Attack
            if ((!character.IsWalk || !character.OnGround) && character.IsArmed)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    An.SetInteger(AttackType, character.OnGround ? GroundAttack() : AirAttack());
                    StartCoroutine(Attack(1.5f));
                }

                if (!character.DoAttack() && !character.OnGround)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        An.SetTrigger(BigAttackStart);
                        character.IsBigAttacking = true;
                    }
                }
                
                if (character.IsBigAttacking)
                {
                    if (character.OnGround || Input.GetKeyUp(KeyCode.Mouse1))
                    {
                        An.SetTrigger(BigAttackEnd);
                        StartCoroutine(BigAttack(0.5f, new int[]{BigAttackStart, BigAttackEnd}));
                    }
                }
            }

            //Saut
            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(Vel.y) < 0.001f)
            {
                Vel.y = character.Jump();
            }

            //Animateur/déplacement
            Rb.velocity = Vel;
            An.SetBool(OnGround, character.OnGround);
            An.SetBool(Jumping, character.IsJumping);
            An.SetBool(Walk, character.IsWalk);
            An.SetBool(Crouch, character.IsCrouch);
            An.SetBool(Armed, character.IsArmed);
        }

        private IEnumerator DoSlide()
        {
            yield return new WaitForSeconds(0.5f);
            _slide = _slide == 0 ? 2 : 0;
        }
    }
}