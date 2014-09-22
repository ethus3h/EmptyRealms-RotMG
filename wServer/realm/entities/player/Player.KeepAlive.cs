using System.Collections.Generic;
using wServer.cliPackets;
using wServer.svrPackets;

namespace wServer.realm.entities
{
    public partial class Player
    {
        long lastPong = -1;
        int? lastTime = null;
        long tickMapping = 0;
        Queue<long> ts = new Queue<long>();

        bool sentPing = false;
        bool KeepAlive(RealmTime time)
        {
            if (lastPong == -1) lastPong = time.tickTimes - 1500;
            if (time.tickTimes - lastPong > 1500 && !sentPing)
            {
                sentPing = true;
                ts.Enqueue(time.tickTimes);
                psr.SendPacket(new PingPacket());
            }
            else if (time.tickTimes - lastPong > 3000)
            {
                ;
                return false;
            }
            return true;
        }
        public void Pong(PongPacket pkt)
        {
            if (lastTime != null && (pkt.Time - lastTime.Value > 3000 || pkt.Time - lastTime.Value < 0))
                ;
            else
                lastTime = pkt.Time;
            tickMapping = ts.Dequeue() - pkt.Time;
            lastPong = pkt.Time + tickMapping;
            sentPing = false;
        }
    }
}
