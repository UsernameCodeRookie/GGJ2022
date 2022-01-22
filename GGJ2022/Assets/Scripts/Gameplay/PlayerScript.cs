using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
	public class PlayerScript : MonoBehaviour
	{
		public PlayerDataSO data;

		public float speed, rushSpeed, sp, mp;
		public int hp;

		public float spDecreaseAmount;

		public UnityEvent Hurt;

		public void Init()
		{
			speed = data.initSpeed;
			rushSpeed = data.rushSpeed;
			sp = data.maxSp;
			mp = data.maxMp;
			hp = data.maxHp;

			spDecreaseAmount = data.SpDecreaseAmount;

		}

		public bool SpDrop()
		{
			sp -= spDecreaseAmount * Time.deltaTime;

			if (sp > 0)
			{
				return true;
			}
			else
			{
				sp = 0;
				return false;
			}
		}

		public void Upd()
		{
			mp += data.mpRate * Time.deltaTime;
			if (mp > data.maxMp) mp = data.maxMp;
		}

		public void GetFruit()
		{
			mp += 1f;
			if (mp > data.maxMp) mp = data.maxMp;
			sp = data.maxSp;
		}

		public void BeDamaged(int amount)
		{
			hp -= amount;
			if (hp <= 0)
			{
				Debug.LogError("Die");
				Destroy(gameObject);
			}
		}
	}
}