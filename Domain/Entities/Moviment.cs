namespace E_Fix.Domain.Entities
{
    public class Moviment
    {
        public DateTime Data { get; set; }
        public Guid Ide { get; set; }
        public int Sequencia { get; set; }
        public decimal Total_Final { get; set; }
        public bool Selected { get; set; }
    }
}
