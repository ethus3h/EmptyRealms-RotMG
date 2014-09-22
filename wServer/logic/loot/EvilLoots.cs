using System;

namespace wServer.logic.loot
{
    class EvilLoot : ILoot
    {
        public Item GetLoot(Random rand)
        {
            return XmlDatas.ItemDescs[0x0a22];
        }
    }
}
