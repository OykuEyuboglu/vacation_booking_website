using Microsoft.AspNetCore.Http.HttpResults;

namespace BitirmeProjesi1.Data.Entities
{
    public abstract class PaymentBase
    {
        public string ID { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Method { get; set; }  // 'credit_card', 'bank_transfer' or 'paypal'
        public PaymentBase()
        {
            ID = Guid.NewGuid().ToString();
        }
    }
}
