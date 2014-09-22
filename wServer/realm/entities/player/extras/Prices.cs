using System;
using System.Collections.Generic;

namespace wServer.realm.entities.player
{
    public class Prices
    {
        public Dictionary<short, int> prices = new Dictionary<short, int>();

        public List<int> SellSlots = new List<int>();

        public Prices()
        {
        }

        public bool HasPrices(Player p)
        {
            bool ret = false;
            List<int> removeSlots = new List<int>();
            foreach (var i in SellSlots)
                if (p.Inventory[i] != null && prices.ContainsKey(p.Inventory[i].ObjectType))
                    ret = true;
                else
                    removeSlots.Add(i);
            foreach (var i in removeSlots)
                SellSlots.Remove(i);
            return ret;
        }

        public int GetPrices(Player p)
        {
            int price = 0;
            foreach (var i in SellSlots)
                if (p.Inventory[i] != null && prices.ContainsKey(p.Inventory[i].ObjectType))
                    price += prices[p.Inventory[i].ObjectType];
            return price;
        }

        public void AddPrice(int price, params string[] items)
        {
            foreach (var i in items)
                try { prices.Add(XmlDatas.IdToType[i], price); }
                catch { Console.Out.WriteLine("Can't find item: " + i); }
        }
    }
}
