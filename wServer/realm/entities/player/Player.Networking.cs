using System;
using System.Collections.Generic;

namespace wServer.realm.entities
{
    partial class Player
    {
        Queue<Tuple<Packet, Predicate<Player>>> pendingPackets = new Queue<Tuple<Packet, Predicate<Player>>>();

        void BroadcastSync(Packet packet)
        {
            BroadcastSync(packet, _ => true);
        }
        void BroadcastSync(Packet packet, Predicate<Player> cond)
        {
            pendingPackets.Enqueue(Tuple.Create(packet, cond));
        }
        void BroadcastSync(IEnumerable<Packet> packets)
        {
            foreach (var i in packets)
                BroadcastSync(i, _ => true);
        }
        void BroadcastSync(IEnumerable<Packet> packets, Predicate<Player> cond)
        {
            foreach (var i in packets)
                BroadcastSync(i, cond);
        }
    }
}
