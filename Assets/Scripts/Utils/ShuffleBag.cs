using System;
using System.Collections.Generic;

namespace WizardGame.Utils
{
    // Testing github
    public class ShuffleBag<T>
    {
        private readonly List<T> allItems;
        private List<T> bag;
        private readonly Random random = new Random();

        public ShuffleBag(IEnumerable<T> items)
        {
            allItems = new List<T>(items);
            bag = new List<T>(allItems);
        }

        public T GetNext()
        {
            // Refill when empty
            if (bag.Count == 0)
            {
                Refill();
            }

            // Pick a random element
            int index = random.Next(bag.Count);
            T item = bag[index];

            // Remove the picked element for this cycle
            bag.RemoveAt(index);

            return item;
        }

        public void Refill()
        {
            // Clear all elements from the bag
            bag.Clear();

            // Add all elements to the bag
            bag.AddRange(allItems);
        }

    }
}
