using System.Collections;
using UnityEngine;

namespace BaseCharacter
{
    public abstract class TypeCharacter : MonoBehaviour
    {
        protected SpriteRenderer Sp;
        protected Rigidbody2D Rb;
        protected Animator An;
        protected BoxCollider2D collider;
        protected Vector2 Vel;

        public Character character;
        public GameObject Player;
        private void FLipX(bool flip)
        {
            Sp.flipX = flip;
        }

        protected void Move(bool flip)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * character.Move();
            FLipX(flip);
        }

        protected void Stop()
        {
            Vel.x = 0f;
            character.IsWalk = false;
        }

        protected void InAir()
        {
            if (Mathf.Abs(Rb.velocity.y) > 0.05f)
            {
                Vel.y += character.FallForce * Time.deltaTime;
                if (character.OnGround)
                    character.OnGround = false;
            }
            else
            {
                if (Mathf.Abs(Rb.velocity.y) < 0.001f)
                    if (!character.OnGround)
                    {
                        character.OnGround = true;
                        Stop();
                    }
                        
                Vel.y = 0f;
                if (character.IsJumping)
                    character.IsJumping = false;
            }
        }

        protected int GroundAttack()
        {
            return character.GroundNumAttack == 1 ? 1 : Random.Range(1, character.GroundNumAttack + 1);
        }

        protected int AirAttack()
        {
            return character.AirNumAttack == 1 ? 1 : Random.Range(1, character.AirNumAttack + 1);
        }

        protected IEnumerator Attack(float timeToWait)
        {
            character.IsAttacking = true;
            yield return new WaitForSeconds(timeToWait);
            character.IsAttacking = false;
        }

        protected IEnumerator BigAttack(float timeToWait, int[] triggerReset)
        {
            yield return new WaitForSeconds(timeToWait);
            character.IsBigAttacking = false;
            foreach (var trigger in triggerReset)
            {
                An.ResetTrigger(trigger);
            }
        }
    }
}