namespace wServer.realm.entities
{
    class Placeholder : StaticObject
    {
        public Placeholder(int life)
            : base(0x070f, life, true, true, false)
        {
            Size = 0;
        }
    }
}
