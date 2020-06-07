using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;
using OxyNode.Infrastructure.Aditional;

// сервис для манипуляциями с газоанализаторами
namespace OxyNode.Infrastructure.Interfaces
{
    public interface IGasAnalyzerService 
    {
        // получить список всех газоанализаторов
        public Task<List<GasAnalyzer>> GetAllGasAnalyzers();

        // получить все газоанализаторы по фильтру
        public Task<List<GasAnalyzer>> GetAllGasAnalyzersFiltered(GasAnalyzerFilter filter);

        // получить страницу газоанализаторов
        public Task<List<GasAnalyzer>> GetPageOfGasAnalyzers();

        // получить страницу газоанализаторов по фильтру
        public Task<List<GasAnalyzer>> GetPageOfGasAnalyzersFiltered(GasAnalyzerFilter filter);


        #region CRUD
        // Create
        public Task CreateGasAnalyzer(GasAnalyzer ga);

        // Read
        public Task<GasAnalyzer> ReadGasAnalyzer(string id);

        // update
        public Task UpdateGasAnalyzer(GasAnalyzer new_id);

        // delete
        public Task DeleteGasAnalyzer(string id);
        #endregion
    }
}
