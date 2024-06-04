using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeafPlayerController : PlayerController
{
    [SerializeField] private GameObject arrowShowerPrefabs;
    [SerializeField] Transform arrowShowerSpawnPoint;
    [SerializeField] private AudioClip arrowShowerSound;
    [SerializeField] protected GameObject bulletPrefabs;
    [SerializeField] protected Transform arrowSpawnPoint;
    [SerializeField] protected Transform arrowSpawnPointOnAir;

    public override void OnAttack1(InputAction.CallbackContext context)
    {
        if (context.started && CanMove && touchingDirections.IsGrounded)
        {
            animator.SetTrigger("attack1");
        }
    }

    public override void OnAttack2(InputAction.CallbackContext context)
    {
        //base.OnAttack2(context);
        if (context.started && CanMove && !IsAttacking)
        {
            {
                animator.SetTrigger("attack2");
                Invoke("ShootNormalArrow", 0.5f);
            }
        }
    }

    public override void OnAttack3(InputAction.CallbackContext context)
    {
        base.OnAttack3(context);
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            {
                Invoke("ShootArrow3", 1f);
            }
        }
    }

    private void ShootNormalArrow()
    {
        Vector2 direction = transform.localScale.x >= 0 ? Vector2.right : Vector2.left;
        if (!touchingDirections.IsGrounded) direction = new Vector2(direction.x, -1f);
        GameObject arrow = ObjectPool.Instance.GetPooledObject();
        //arrow.SetActive(true);
        if (arrow != null)
        {
            arrow.transform.position = (touchingDirections.IsGrounded)
                ? arrowSpawnPoint.position
                : arrowSpawnPointOnAir.position;
            arrow.SetActive(true);
        }
        else
        {
            Debug.Log("k co mui ten nao");
        }

        if (!touchingDirections.IsGrounded)
        {
            float angle = direction.x >= 0 ? -45f : 45f; // Adjust the angle as needed
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            arrow.transform.rotation = quaternion.identity;
        }

        arrow.transform.localScale = new Vector3(transform.localScale.x, arrow.transform.localScale.y,
            arrow.transform.localScale.z);
        arrow.GetComponent<Arrow>()?.Shoot(direction);
    }

    private void ShootArrow3()
    {
        GameObject arrowShower = Instantiate(arrowShowerPrefabs, arrowShowerSpawnPoint.position, Quaternion.identity);
        StartCoroutine(DestroyArrow(arrowShower));
    }

    private IEnumerator DestroyArrow(GameObject arrow)
    {
        Debug.Log("da xoa");
        AudioSource.PlayClipAtPoint(arrowShowerSound, arrowShowerSpawnPoint.position);
        yield return new WaitForSeconds(1.2f);
        Destroy(arrow);
    }
}