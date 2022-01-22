using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    [CreateAssetMenu]
    public class FruitGenerate : ScriptableObject
    {
        [Header("Generate Settings")]
        public float generateFrequency;
        [Range(0,1)]
        public float generateTimer;
        public int generateAmount;
        [Header("Accelerate Settings")]
        public float frequencyVelocity;
        public float frequencyAddAmount;
        [Range(0, 1)]
        public float frequencyTimer;
        public float amountVelocity;
        [Range(0, 1)]
        public float amountTimer;



        public bool UpdateTimer()
        {
            bool flag = false;
            generateTimer += Time.deltaTime * generateFrequency;

            if(generateTimer > 1)
            {
                flag = true;
                frequencyTimer += frequencyVelocity;
                amountTimer += amountVelocity;

                while(frequencyTimer > 1)
                {
                    frequencyTimer -= 1;
                    generateFrequency += frequencyAddAmount;
                }
                while(amountTimer > 1)
                {
                    amountTimer -= 1;
                    generateAmount += 1;
                }
            }

            return flag;
        }
        

        public void Init()
        {
            
        }
    }
}
