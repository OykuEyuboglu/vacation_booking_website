using Microsoft.AspNetCore.Http.HttpResults;

namespace BitirmeProjesi1.Data.Entities
{
    //Baseden sonraki genel sınıf
    public abstract class Entity : Base
    {
        public DateTime UpdatedAt { get; set; }
        public Entity()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}
