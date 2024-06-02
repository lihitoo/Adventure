using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeafPlayerController : PlayerController
{
    [SerializeField] private GameObject arrowShowerPrefabs;
    [SerializeField] Transform arrowShowerSpawnPoint;
    [SerializeField] private AudioClip arrowShowerSound;

    public override void OnAttack2(InputAction.CallbackContext context)
    {
        base.OnAttack2(context);
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            {
                Invoke("ShootArrow2", 0.5f);
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

    private void ShootArrow2()
    {
        Vector2 direction = transform.localScale.x >= 0 ? Vector2.right : Vector2.left;
        GameObject bullet = Instantiate(bulletPrefabs, arrowSpawnPoint.position, Quaternion.identity);
        bullet.transform.localScale = new Vector3(transform.localScale.x, bullet.transform.localScale.y, bullet.transform.localScale.z);
        bullet.GetComponent<Shootable>()?.Shoot(direction); // Sử dụng component Shootable để bắn mũi tên
    }
    private void ShootArrow3()
    {

        
        GameObject arrowShower = Instantiate(arrowShowerPrefabs, arrowShowerSpawnPoint.position, Quaternion.identity);
        StartCoroutine(DestroyArrow(arrowShower));
    }

    private IEnumerator DestroyArrow(GameObject arrow)
    {
        Debug.Log("da xoa");
        AudioSource.PlayClipAtPoint(arrowShowerSound,arrowShowerSpawnPoint.position);
        yield return new WaitForSeconds(1.2f);
        Destroy(arrow);
    } 
}