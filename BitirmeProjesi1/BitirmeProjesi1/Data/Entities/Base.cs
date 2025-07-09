namespace BitirmeProjesi1.Data.Entities
{

    // en genel sınıf
    public abstract class Base
    {
        public string ID { get; init; }
        public DateTime CreatedAt { get; init; }
        public Base()
        {
            ID = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
        }
    }
}
