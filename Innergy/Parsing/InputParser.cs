using Innergy.Parsing.Interfaces;
using Innergy.Parsing.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Innergy.Parsing
{
    public class InputParser: IInputParser
    {
        private List<Warehouse> _warehouses = new List<Warehouse>();

        public List<Warehouse> Parse(string[] inputLines)
        {
            foreach (var line in inputLines)
            {
                if (IsIgnored(line)) { continue; }
                ParseLine(line);
            }
            SortWarehouses();

            return _warehouses;
        }

        private bool IsIgnored(string line)
        {
            return line.StartsWith('#');
        }

        private void ParseLine(string line)
        {
            string[] lineProperties = line.Split(";");
            ValidateLine(lineProperties);

            string materialId = lineProperties[1];
            string[] materialWarehousesProperty = lineProperties[2].Split("|");

            foreach (var materialWarehouseProperty in materialWarehousesProperty)
            {
                string[] warehouseNameAndQuantity = materialWarehouseProperty.Split(",");
                ValidateWarehouseNameAndQuantity(warehouseNameAndQuantity);

                string warehouseName = warehouseNameAndQuantity[0];
                int materialQuantity = ParseMaterialQuantity(warehouseNameAndQuantity[1]);

                Material material = new Material(materialId, materialQuantity);
                AddMaterialToWarehouse(material, warehouseName);
            }
        }

        private void ValidateLine(string[] lineProperties)
        {
            int expectedPropertiesLength = 3;

            if (lineProperties.Length != expectedPropertiesLength)
            {
                throw new ArgumentException(
                    "Invalid input, line must consist of material name, id and warehouse stock separated by semicolons."
                );
            }
        }

        private void ValidateWarehouseNameAndQuantity(string[] warehouseNameAndQuantity)
        {
            int expectedWarehouseNameAndQuantityLength = 2;
            if (warehouseNameAndQuantity.Length != expectedWarehouseNameAndQuantityLength)
            {
                throw new ArgumentException(
                    "Invalid input, warehouse stock must consist of warehouse name and materials quantity " +
                    "separated by vertical bar."
                );
            }
        }

        private int ParseMaterialQuantity(string materialQuantity)
        {
            int quantity;
            if (!Int32.TryParse(materialQuantity, out quantity))
            {
                throw new ArgumentException("Invalid input, materialQuantity must be a number.");
            }

            return quantity;
        }

        private void AddMaterialToWarehouse(Material material, string warehouseName)
        {
            var alreadySavedWarehouse = _warehouses.FirstOrDefault(x => x.Name == warehouseName);
            if (alreadySavedWarehouse == null)
            {
                _warehouses.Add(
                    new Warehouse
                    {
                        Name = warehouseName,
                        Materials = new List<Material>()
                        {
                            material
                        }
                    }
                );
            }
            else
            {
                alreadySavedWarehouse.Materials.Add(material);
            }
        }

        private void SortWarehouses()
        {
            foreach (var warehouse in _warehouses)
            {
                warehouse.Materials = warehouse.Materials.OrderBy(x => x.Id).ToList();
            }

            _warehouses = _warehouses.OrderByDescending(x => x.Materials.Sum(y => y.Quantity)).ThenByDescending(x => x.Name).ToList();
        }
    }
}
