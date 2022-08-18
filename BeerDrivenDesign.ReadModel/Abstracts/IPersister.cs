using System.Linq.Expressions;
using LanguageExt;

namespace BeerDrivenDesign.ReadModel.Abstracts;

public interface IPersister
{
    Task<Either<Exception, T>> GetByIdFuncAsync<T>(string id) where T : ModelBase;

    Task<T> GetByIdAsync<T>(string id) where T : ModelBase;
    Task InsertAsync<T>(T dtoToInsert) where T : ModelBase;
    Task ReplaceAsync<T>(T dtoToUpdate) where T : ModelBase;
    Task UpdateOneAsync<T>(string id, Dictionary<string, object> propertiesToUpdate) where T : ModelBase;

    Task DeleteAsync<T>(string id) where T : ModelBase;
    Task DeleteManyAsync<T>(Expression<Func<T, bool>> filter) where T : ModelBase;
    Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>>? filter = null) where T : ModelBase;
}