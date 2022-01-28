using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GridSystem
{
    public class FruitManager : MonoBehaviour
    {
        public List<Fruit> fruits;

        public int currentFruitsNumber;

        private GridFactory gridFactory;
        private FruitGenerate fruitGenerate;

        //public int initialFruitsNumber;

        public void Init(int initialFruitsNumber)
        {
            for (int i = 0; i < initialFruitsNumber; i++)
            {
                fruits.Add(null);
            }
        }

        public void RemoveFruit(Fruit fruit)
        {
            int index = fruits.IndexOf(fruit);
            fruits[index] = null;
            currentFruitsNumber--;
        }

        public void GenerateFruit()
        {
            int index = fruits.FindIndex(c => c == null);
            if (index < 0) return;
            Fruit fruit = gridFactory.RandomGenerateFruit();
            fruits[index] = fruit;

            currentFruitsNumber++;
        }

        public void ClearAndGenerateFruits(int num)
        {
            Clear();
            for(int i = 0; i < num; i++)
            {
                GenerateFruit();
            }
        }

        public bool IsEmpty()
        {
            return currentFruitsNumber <= 0;
        }

        public void Clear()
        {
            fruits.ForEach(c => { 
                if (c != null) 
                    c.Disappear();
            });

            for (int i = 0; i < fruits.Count; i++)
                fruits[i] = null;
            
            currentFruitsNumber = 0;
        }

        private void Awake()
        {
            gridFactory = GetComponent<GridFactory>();
            fruitGenerate = GetComponent<FruitGenerate>();
            fruitGenerate.GenerateFruitEvent.AddListener(() => ClearAndGenerateFruits(fruitGenerate.generateAmount));
            GameManager.instance.GameOver.AddListener(() => Clear());
            GameManager.instance.GameReset.AddListener(() => Clear());

            //Init(fruitGenerate.generateAmount);
        }

        private void Update()
        {
            if (IsEmpty())
                fruitGenerate.GenerateFruitEvent.Invoke();
        }


    }
}
