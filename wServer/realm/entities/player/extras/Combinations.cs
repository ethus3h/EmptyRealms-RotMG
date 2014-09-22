using System;
using System.Collections.Generic;
using System.Linq;

namespace wServer.realm.entities.player
{
    public class Combinations
    {
        public Dictionary<string[], Tuple<string, int>> combos = new Dictionary<string[], Tuple<string, int>>();
        public Dictionary<string[], Tuple<string, int, bool>> advCombos = new Dictionary<string[], Tuple<string, int, bool>>();

        public List<int> SlotList = new List<int>();

        public Tuple<string, int> Combo = new Tuple<string, int>("Short Sword", 0);

        public Combinations()
        {
        }

        public bool SetCombo(string[] items)
        {
            foreach (var i in combos)
            {
                if (i.Key.Length == items.Length)
                {
                    bool pass = true;
                    foreach (var e in items)
                    {
                        if (!i.Key.Contains(e))
                        {
                            pass = false;
                        }
                    }
                    if (pass)
                    {
                        Combo = i.Value;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool SetComboAdv(string[] items, bool forgeAmmy)
        {
            foreach (var i in advCombos)
            {
                if (i.Key.Length == items.Length)
                {
                    bool pass = true;
                    foreach (var e in items)
                    {
                        if (!i.Key.Contains(e))
                        {
                            pass = false;
                        }
                        else
                        {
                            if (i.Value.Item3 && !forgeAmmy)
                                pass = false;
                        }
                    }
                    if (pass)
                    {
                        Combo = new Tuple<string, int>(i.Value.Item1, i.Value.Item2);
                        return true;
                    }
                }
            }
            return false;
        }

        public void AddCombo(string result, int price, params string[] items)
        {
            combos.Add(items, new Tuple<string, int>(result, price));
            advCombos.Add(items, new Tuple<string, int, bool>(result, price, false));
        }

        public void AddAdvCombo(string result, int price, params string[] items)
        {
            advCombos.Add(items, new Tuple<string, int, bool>(result, price, true));
        }
    }
}
