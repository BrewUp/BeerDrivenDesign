using System.Linq.Expressions;
using LanguageExt;

namespace BeerDrivenDesign.ReadModel.Abstracts;

public interface IFunctionalPersister
{
    Task<Either<Exception, T>> GetByIdAsync<T>(string id) where T : ModelBase;
    Task<Either<Exception, string>> InsertAsync<T>(T dtoToInsert) where T : ModelBase;

    Task<Either<Exception, string>> ReplaceAsync<T>(T dtoToUpdate) where T : ModelBase;
    Task<Either<Exception, string>> UpdateOneAsync<T>(string id, Dictionary<string, object> propertiesToUpdate) where T : ModelBase;

    Task<Either<Exception, string>> DeleteAsync<T>(string id) where T : ModelBase;
    Task DeleteManyAsync<T>(Expression<Func<T, bool>> filter) where T : ModelBase;
    Task<Either<Exception, T[]>> FindAsync<T>(Expression<Func<T, bool>>? filter = null) where T : ModelBase;
    Task<Either<Exception, T[]>> F1ndAsync<T>(Expression<Func<T, bool>>? filter = null) where T : ModelBase;
}