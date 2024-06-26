using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	[SerializeField] private float attackCooldown;
	[SerializeField] private Transform firePoint;
	[SerializeField] private GameObject[] bullets;

	private Animator anim;
	private PlayerMovement playerMovement;
	private float cooldownTimer = Mathf.Infinity;

	private void Awake() {
		anim = GetComponent<Animator>();
		playerMovement = GetComponent<PlayerMovement>();
	}

	private void Update() {
		if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && cooldownTimer > attackCooldown && playerMovement.canAttack()) {
			Attack();
		}

		cooldownTimer += Time.deltaTime;
	}

	private void Attack() {
		anim.SetTrigger("attack");
		cooldownTimer = 0;

		bullets[FindBullet()].transform.position = firePoint.position;
		bullets[FindBullet()].GetComponent<Bullet>().SetDirection(Mathf.Sign(transform.localScale.x));
	}

	private int FindBullet() {
		for (int i = 0; i < bullets.Length; ++i) {
			if (!bullets[i].activeInHierarchy) {
				return i;
			}
		}

		return 0;
	}
}
