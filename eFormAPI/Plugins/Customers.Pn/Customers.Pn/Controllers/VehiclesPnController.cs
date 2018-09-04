using System;
using System.Diagnostics;
using Customers.Pn.Helpers;
using Customers.Pn.Infrastructure.Data;
using Customers.Pn.Infrastructure.Data.Entities;
using Customers.Pn.Infrastructure.Models;
using NLog;

namespace Customers.Pn.Controllers
{
    public class VehiclesPnController : ApiController
    {
        private readonly Logger _logger;
        private readonly VehiclesPnDbContext _dbContext;

        public VehiclesPnController()
        {
            _dbContext = VehiclesPnDbContext.Create();
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("api/vehicles-pn")]
        public OperationDataResult<VehiclesPnModel> GetAllVehicles(VehiclesPnRequestModel pnRequestModel)
        {
            try
            {
                var vehiclesPnModel = new VehiclesPnModel();
                var vehiclesQuery = _dbContext.Vehicles.AsQueryable();
                if (!string.IsNullOrEmpty(pnRequestModel.SortColumnName))
                {
                    if (pnRequestModel.IsSortDsc)
                    {
                        vehiclesQuery = vehiclesQuery.OrderByDescending(pnRequestModel.SortColumnName);
                    }
                    else
                    {
                        vehiclesQuery = vehiclesQuery.OrderBy(pnRequestModel.SortColumnName);
                    }
                }
                vehiclesPnModel.Total = vehiclesQuery.Count();
                vehiclesQuery = vehiclesQuery.Skip(pnRequestModel.Offset).Take(pnRequestModel.PageSize);
                var vehicles = vehiclesQuery.ToList();
                vehicles.ForEach(vehicle =>
                {
                    vehiclesPnModel.Vehicles.Add(new VehiclePnModel()
                    {
                        VinNumber = vehicle.VinNumber,
                        ContractEndDate = vehicle.ContractEndDate,
                        ContractStartDate = vehicle.ContractStartDate,
                        CustomerName = vehicle.CustomerName,
                        RegistrationDate = vehicle.RegistrationDate,
                        Brand = vehicle.Brand,
                        ContactNumber = vehicle.ContactNumber,
                        ModelName = vehicle.ModelName,
                        Id = vehicle.Id,
                    });
                });
                return new OperationDataResult<VehiclesPnModel>(true, vehiclesPnModel);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationDataResult<VehiclesPnModel>(true,
                    VehiclePnLocaleHelper.GetString("ErrorObtainingVehiclesInfo"));
            }
        }

        [HttpPost]
        [Route("api/vehicles-pn/create-vehicle")]
        public OperationResult CreateVehicle(VehiclePnModel vehiclePnCreateModel)
        {
            try
            {
                var vehiclePn = new VehiclePn
                {
                    VinNumber = vehiclePnCreateModel.VinNumber,
                    Brand = vehiclePnCreateModel.Brand,
                    ContactNumber = vehiclePnCreateModel.ContactNumber,
                    ContractEndDate = vehiclePnCreateModel.ContractEndDate,
                    ContractStartDate = vehiclePnCreateModel.ContractStartDate,
                    CustomerName = vehiclePnCreateModel.CustomerName,
                    ModelName = vehiclePnCreateModel.ModelName,
                    RegistrationDate = vehiclePnCreateModel.RegistrationDate,
                };
                _dbContext.Vehicles.Add(vehiclePn);
                _dbContext.SaveChanges();
                return new OperationResult(true,
                    VehiclePnLocaleHelper.GetString("VehicleCreated", vehiclePnCreateModel.Brand,
                        vehiclePnCreateModel.ModelName));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationResult(true, VehiclePnLocaleHelper.GetString("ErrorWhileCreatingVehicle"));
            }
        }

        [HttpPost]
        [Route("api/vehicles-pn/update-vehicle")]
        public OperationResult UpdateVehicle(VehiclePnModel vehiclePnUpdateModel)
        {
            try
            {
                var vehicle = _dbContext.Vehicles.FirstOrDefault(x => x.Id == vehiclePnUpdateModel.Id);
                if (vehicle == null)
                {
                    return new OperationResult(true, "Vehicle not found");
                }
                vehicle.VinNumber = vehiclePnUpdateModel.VinNumber;
                vehicle.Brand = vehiclePnUpdateModel.Brand;
                vehicle.ContactNumber = vehiclePnUpdateModel.ContactNumber;
                vehicle.ContractEndDate = vehiclePnUpdateModel.ContractEndDate;
                vehicle.ContractStartDate = vehiclePnUpdateModel.ContractStartDate;
                vehicle.CustomerName = vehiclePnUpdateModel.CustomerName;
                vehicle.ModelName = vehiclePnUpdateModel.ModelName;
                vehicle.RegistrationDate = vehiclePnUpdateModel.RegistrationDate;
                _dbContext.SaveChanges();
                return new OperationDataResult<VehiclesPnModel>(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationDataResult<VehiclesPnModel>(true, "ErrorWhileUpdatingVehicleInfo");
            }
        }
    }
}