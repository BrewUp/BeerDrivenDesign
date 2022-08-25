﻿using System.Linq.Expressions;
using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.ReadModel.Abstracts;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using LanguageExt;
using static LanguageExt.Prelude;

namespace BeerDrivenDesign.ReadModel.MongoDb.Repositories;

public sealed class Persister : IPersister
{
    private readonly IMongoDatabase _mongoDatabase;
    private readonly ILogger _logger;

    public Persister(IMongoDatabase mongoDatabase, ILoggerFactory loggerFactory)
    {
        _mongoDatabase = mongoDatabase;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task<Either<Exception, T>> GetByIdFuncAsync<T>(string id) where T : ModelBase
    {
        try
        {
            var collection = _mongoDatabase.GetCollection<T>(GetCollectionName<T>());

            var filter = Builders<T>.Filter.Eq(c => c.Id, id);
            return await collection.CountDocumentsAsync(filter) > 0
                ? Right((await collection.FindAsync(filter)).First())
                : Left(new Exception($"No document found in {typeof(T).Name} with Id {id}"));
        }
        catch (Exception ex)
        {
            _logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            return Left(ex);
        }
    }

    public async Task<T> GetByIdAsync<T>(string id) where T : ModelBase
    {
        try
        {
            var collection = _mongoDatabase.GetCollection<T>(GetCollectionName<T>()).AsQueryable();

            var results = await Task.Run(() => collection.Where(t => t.Id.Equals(id) && !t.IsDeleted));
            return await results.AnyAsync()
                ? results.First()
                : ConstructEntity<T>();
        }
        catch (Exception ex)
        {
            _logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task InsertAsync<T>(T dtoToInsert) where T : ModelBase
    {
        try
        {
            var collection = _mongoDatabase.GetCollection<T>(GetCollectionName<T>());
            await collection.InsertOneAsync(dtoToInsert);
        }
        catch (Exception ex)
        {
            _logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task ReplaceAsync<T>(T dtoToUpdate) where T : ModelBase
    {
        try
        {
            var collection = _mongoDatabase.GetCollection<T>(GetCollectionName<T>());
            await collection.ReplaceOneAsync(x => x.Id == dtoToUpdate.Id, dtoToUpdate);
        }
        catch (Exception ex)
        {
            _logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task UpdateOneAsync<T>(string id, Dictionary<string, object> propertiesToUpdate) where T : ModelBase
    {
        try
        {
            var collection = _mongoDatabase.GetCollection<T>(GetCollectionName<T>());

            var updateDefination = propertiesToUpdate
                .Select(dataField => Builders<T>.Update.Set(dataField.Key, dataField.Value)).ToList();
            var combinedUpdate = Builders<T>.Update.Combine(updateDefination);

            var updateResult = await collection.UpdateOneAsync(
                Builders<T>.Filter.Eq("_id", id),
                combinedUpdate);

            if (!updateResult.IsAcknowledged)
                throw new Exception($"Failed to Update {typeof(T).Name}");
        }
        catch (Exception ex)
        {
            _logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task DeleteAsync<T>(string id) where T : ModelBase
    {
        try
        {
            var collection = _mongoDatabase.GetCollection<T>(GetCollectionName<T>());
            var filter = Builders<T>.Filter.Eq("_id", id);
            await collection.FindOneAndDeleteAsync(filter);
        }
        catch (Exception ex)
        {
            _logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task DeleteManyAsync<T>(Expression<Func<T, bool>> filter) where T : ModelBase
    {
        try
        {
            var collection = _mongoDatabase.GetCollection<T>(GetCollectionName<T>());
            await collection.DeleteManyAsync(filter);
        }
        catch (Exception ex)
        {
            _logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> filter = null) where T : ModelBase
    {
        try
        {
            var collection = _mongoDatabase.GetCollection<T>(GetCollectionName<T>()).AsQueryable();

            return await Task.Run(() => filter != null
                ? collection.Where(filter).Where(c => !c.IsDeleted)
                : collection);
        }
        catch (Exception ex)
        {
            _logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    private static string GetCollectionName<T>() where T : ModelBase => typeof(T).Name;

    public static T ConstructEntity<T>() where T : ModelBase
    {
        return (T)Activator.CreateInstance(typeof(T), true);
    }
}