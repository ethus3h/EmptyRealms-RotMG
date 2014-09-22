using System;
using wServer.realm;

namespace wServer.logic.taunt
{
    class RandomTaunt : TauntBase
    {
        public RandomTaunt(double prob, string taunt) { this.prob = prob; this.taunt = taunt; }
        double prob;
        string taunt;

        Random rand = new Random();
        protected override bool TickCore(RealmTime time)
        {
            if (rand.NextDouble() > prob) return false;
            Taunt(taunt, false);
            return true;
        }
    }
}
