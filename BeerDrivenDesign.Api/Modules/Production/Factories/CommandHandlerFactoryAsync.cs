using Muflone.Factories;
using Muflone.Messages.Commands;

namespace BeerDrivenDesign.Api.Modules.Production.Factories;

public sealed class CommandHandlerFactoryAsync : ICommandHandlerFactoryAsync
{
    private readonly IServiceProvider _serviceProvider;

    public CommandHandlerFactoryAsync(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICommandHandlerAsync<T> CreateCommandHandlerAsync<T>() where T : class, ICommand =>
        _serviceProvider.GetService<ICommandHandlerAsync<T>>()!;
}