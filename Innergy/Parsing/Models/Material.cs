
namespace Innergy.Parsing.Models
{
    public class Material
    {
        public string Id;
        public int Quantity;

        public Material(string id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }
    }
}
