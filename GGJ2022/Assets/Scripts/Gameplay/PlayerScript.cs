using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
	[CreateAssetMenu]
	public class PlayerScript : ScriptableObject
	{
		public PlayerDataSO data;

        [Header("UI Display Variables")]
		public float sp, mp;
		public int hp;
        public int availableAttackCount = 1;


        public float speed;
        public float rushSpeed;
		public float spDecreaseAmount;
		public UnityEvent Hurt;
		private bool LeftOrRight;

		public void Init(bool LeftOrRight)
		{
			this.LeftOrRight = LeftOrRight;
            if (LeftOrRight)
            {
				GameManager.instance.playerScriptL = this;
            }
			else
            {
				GameManager.instance.playerScriptR = this;
			}

			speed = data.initSpeed;
			rushSpeed = data.rushSpeed;
			sp = data.maxSp;
			mp = 0;
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
			mp += data.mpRecover;
            if (mp > data.maxMp)
            {
                mp = 0;
                availableAttackCount++;
            }
			sp += data.spRecover;
            if (sp > data.maxSp)
                sp = data.maxSp;
		}

        public void DecreaseAvailableAttackCount()
        {
            if(availableAttackCount >= 1)
                availableAttackCount--;
        }

		public void BeDamaged(int amount)
		{
			hp -= amount;
			if (hp <= 0)
			{
				GameManager.instance.GameOver.Invoke();
			}
		}
	}
}