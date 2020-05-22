using Innergy.Input;
using Innergy.Input.Interfaces;
using Innergy.Parsing;
using Innergy.Parsing.Interfaces;

namespace Innergy
{
    class Program
    {
        static void Main()
        {
            IInputReader inputReader = new InputReader();
            IInputParser inputParser = new InputParser();
            var warehouseDataImporter = new WarehouseDataImporter(inputReader, inputParser);
            warehouseDataImporter.Start();
        }
    }
}
