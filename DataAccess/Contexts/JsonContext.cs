using Core.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class JsonContext<T> where T : BaseEntity
    {
        private readonly IConfigurationRoot _config;
        private readonly string _connectionString;
        private List<T> _entities;
        private List<T> _entitiesToAdd = new List<T>();
        private List<T> _entitiesToRemove = new List<T>();

        public JsonContext(IConfigurationRoot config)
        {
            _config = config;
            _connectionString = _config.
                 GetConnectionString(typeof(T).Name.ToString());
            _entities = GetData();
        }

        public List<T> GetData()
        {
            using (StreamReader reader = new StreamReader(_connectionString))
            {
                var json = reader.ReadToEnd();
                var obj = JsonConvert.DeserializeObject<IEnumerable<T>>(json);
                _entities = obj as List<T>;
                return _entities;
            }
        }

        public void InsertEntity(T entity)
        {
            int lastId = 0;

            if (_entities == null)
            {
                _entities = new List<T>();
            }
            else
            {
                lastId = _entities.Last().Id + 1;
            }

            entity.Id = lastId + 1;
            _entitiesToAdd.Add(entity);
        }

        public void DeleteEntity(T entity)
        {
            _entitiesToRemove.Add(entity);
        }

        public async Task<bool> UpdateEntity(int idOfEntityToUpdate, T updatedEntity)
        {
            var entityToUpdate = _entities.FirstOrDefault(e => e.Id == idOfEntityToUpdate);

            entityToUpdate = updatedEntity;
            entityToUpdate.Id = idOfEntityToUpdate;
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            AddAllEntitites();
            RemoveAllEntities();
            return await PushAllDataToFileAsync();
        }

        private void AddAllEntitites()
        {
            foreach (var entity in _entitiesToAdd)
            {
                _entities.Add(entity);
            }

            _entitiesToAdd.Clear();
        }

        private void RemoveAllEntities()
        {
            foreach (var entity in _entitiesToRemove)
            {
                _entities.Remove(entity);
            }

            _entitiesToRemove.Clear();
        }

        private async Task<bool> PushAllDataToFileAsync()
        {
            using (StreamWriter writer = new StreamWriter(_connectionString, false))
            {
                var json = JsonConvert.SerializeObject(_entities,Formatting.Indented);
                await writer.WriteLineAsync(json);
                return true;
            }
        }
    }
}
