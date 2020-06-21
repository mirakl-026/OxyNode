using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;
using OxyNode.Infrastructure.Aditional;

// сервис для манипуляциями с газоанализаторами
namespace OxyNode.Infrastructure.Interfaces
{
    public interface IDeviceService 
    {
        // получить список всех газоанализаторов
        public Task<List<Device>> GetAllDevices();

        // получить все газоанализаторы по фильтру
        public Task<List<Device>> GetAllDevicesFiltered(DeviceFilter filter);

        // получить страницу газоанализаторов
        public Task<List<Device>> GetPageOfDevices(int pageNumber, int pageSize);

        // получить страницу газоанализаторов по фильтру
        public Task<List<Device>> GetPageOfDevicesFiltered(int pageNumber, int pageSize, DeviceFilter filter);

        // получить кол-во газоанализаторов в БД
        public Task<long> GetDevicesCount();

        #region CRUD
        // Create
        public Task CreateDevice(Device dev);

        // Read
        public Task<Device> ReadDevice(string id);

        // update
        public Task UpdateDevice(Device newDev);

        // delete
        public Task DeleteDevice(string id);
        #endregion

        public Task DeleteAllDevices();
    }
}
