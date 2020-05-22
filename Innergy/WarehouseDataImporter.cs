using Innergy.Input.Interfaces;
using Innergy.Parsing.Interfaces;
using System;
using System.Linq;

namespace Innergy
{
    public class WarehouseDataImporter
    {
        private IInputReader _inputReader;
        private IInputParser _inputParser;

        public WarehouseDataImporter(IInputReader inputReader, IInputParser inputParser)
        {
            _inputReader = inputReader;
            _inputParser = inputParser;
        }

        public void Start()
        {
            string[] inputLines = _inputReader.Read();
            var warehouses = _inputParser.Parse(inputLines);

            foreach (var warehouse in warehouses)
            {
                Console.WriteLine($"{warehouse.Name} (total {warehouse.Materials.Sum(x => x.Quantity)})");

                foreach (var material in warehouse.Materials)
                {
                    Console.WriteLine($"{material.Id}: {material.Quantity}");
                }
                Console.WriteLine();
            }
        }
    }
}
