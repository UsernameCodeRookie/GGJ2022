using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GridSystem
{
    public class FruitGenerate : MonoBehaviour
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

        public UnityEvent UpdateTimerEvent;

        private void Update()
        {
            UpdateTimer();
        }

        public void SetValue(FruitGenerateSO fruitGenerateSO)
        {
            generateFrequency = fruitGenerateSO.generateFrequency;
            generateAmount = fruitGenerateSO.generateAmount;
            frequencyVelocity = fruitGenerateSO.frequencyVelocity;
            frequencyAddAmount = fruitGenerateSO.frequencyAddAmount;
            amountVelocity = fruitGenerateSO.amountVelocity;
        }

        public void UpdateTimer()
        {
            
            generateTimer += Time.deltaTime * generateFrequency;

            while(generateTimer > 1)
            {
                generateTimer -= 1;
                UpdateTimerEvent.Invoke();

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
        }
        
    }
}
