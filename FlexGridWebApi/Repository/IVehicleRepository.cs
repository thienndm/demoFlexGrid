using FlexGridWebApi.Models;
using System;
using System.Collections.Generic;

namespace FlexGridWebApi.Repository
{
    interface IVehicleRepository
    {
        IEnumerable<VehicleInfo> GetAll();
        IEnumerable<VehicleInfo> GetVehiclesByYear(int year);
        IEnumerable<VehicleInfo> GetVehiclesByMake(string make);
        VehicleInfo Get(Guid Id);
        void Remove(Guid Id);
        bool Update(VehicleInfo item);
        VehicleInfo Add(VehicleInfo item);
        IEnumerable<VehicleColumn> GetVehicleColumns();
    }
}