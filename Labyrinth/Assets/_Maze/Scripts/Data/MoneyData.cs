using System;

namespace FreedLOW._Maze.Scripts.Data
{
    [Serializable]
    public class MoneyData
    {
        public int Money { get; private set; }
        public Action<int> OnMoneyAmountChanged;

        public MoneyData(int money)
        {
            Money = money;
        }

        public void ChangeMoneyCount(int amount)
        {
            Money += amount;
            OnMoneyAmountChanged?.Invoke(Money);
        }
    }
}