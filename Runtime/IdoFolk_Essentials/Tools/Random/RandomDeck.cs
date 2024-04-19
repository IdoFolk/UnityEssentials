using System.Collections.Generic;
using System.Linq;

namespace IdoFolk_Essentials.Tools.Random
{
    public struct RandomDeck<T> : IRandomProvider<T>
    {
        private readonly List<T> _totalItems;
        private List<T> _currentItems;
        
        public RandomDeck(IEnumerable<T> items)
        {
            _totalItems = items.ToList();
            _currentItems = new List<T>(_totalItems);
        }
        public T Provide()
        {
            if (_currentItems.Count == 0) _currentItems = new List<T>(_totalItems);
            var item = _currentItems[UnityEngine.Random.Range(0,_currentItems.Count)];
            _currentItems.Remove(item);
            return item;
        }
        
    }
}