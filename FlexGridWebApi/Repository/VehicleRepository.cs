using FlexGridWebApi.Models;
using System;
using System.Collections.Generic;

namespace FlexGridWebApi.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        /// <summary>
        /// This is not the right approach for production quality code. 
        /// This is for demo purposes only.
        /// </summary>
        private List<VehicleInfo> vehicles = new List<VehicleInfo>();

        public VehicleRepository()
        {
            // This is used to populate initial data.
            // NOTE: This is not the right approach for production quality code.
            PopulateInitialData();
        }

        public IEnumerable<VehicleInfo> GetAll()
        {
            return vehicles;
        }

        public IEnumerable<VehicleInfo> GetVehiclesByYear(int year)
        {
            return vehicles.FindAll(p => p.Year == year);
        }

        public IEnumerable<VehicleInfo> GetVehiclesByMake(string make)
        {
            return vehicles.FindAll(p => p.Make == make);
        }

        public VehicleInfo Get(Guid Id)
        {
            return vehicles.Find(p => p.Id == Id);
        }

        public IEnumerable<VehicleColumn> GetVehicleColumns()
        {
            var cols = new List<VehicleColumn>()
            {
                new VehicleColumn {Name="Id", Text="Id", IsReadOnly = true},
                new VehicleColumn {Name="Make", Text="Make", IsReadOnly = false},
                new VehicleColumn {Name="Model", Text="Model", IsReadOnly = false},
                new VehicleColumn {Name="Year", Text="Year", IsReadOnly = false},
                new VehicleColumn {Name="Trim", Text="Trim", IsReadOnly = false},
            };

            return cols;
        }

        public void Remove(Guid Id)
        {
            vehicles.RemoveAll(p => p.Id == Id);
        }

        public bool Update(VehicleInfo item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            int index = vehicles.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }

            // This form of update is to remove and add again.
            vehicles.RemoveAt(index);
            vehicles.Add(item);

            return true;
        }

        public VehicleInfo Add(VehicleInfo item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            // A new GUID is assigned every time a new item is added.
            item.Id = System.Guid.NewGuid();
            vehicles.Add(item);
            return item;
        }

        private void PopulateInitialData()
        {
            Add(new VehicleInfo { Make = "Infiniti", Model = "FX 35", Year = 2009, Trim = "Crossover" });
            Add(new VehicleInfo { Make = "Toyota", Model = "RAV44", Year = 2005, Trim = "Sport" });
            Add(new VehicleInfo { Make = "Audi", Model = "A5", Year = 2010, Trim = "Convertible" });
        }
    }
}