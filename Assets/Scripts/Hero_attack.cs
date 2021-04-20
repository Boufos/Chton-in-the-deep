using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_attack : MonoBehaviour
{
    public Transform punch;
    public float punchRadius;
	public Animator animAttack;


	public static void Action(Vector2 point, float radius, int layerMask, float damage)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, 1 << layerMask);

		foreach (Collider2D hit in colliders)
		{
			if (hit.GetComponent<Enemy>())
			{
				hit.GetComponent<Enemy>().GetDamage(damage);
			}
		}
	}

    private void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			//animAttack.SetBool("Attack", true);
			Hero_attack.Action(punch.position, punchRadius, 7, 3);
		}
	}
}


