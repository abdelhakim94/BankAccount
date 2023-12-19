using System.Reflection;
using Ardalis.SharedKernel;
using Autofac;
using ExaltIt.BankAccount.Core.AccountAggregate;
using ExaltIt.BankAccount.Infrastructure.Data;
using ExaltIt.BankAccount.Infrastructure.Services;
using ExaltIt.BankAccount.UseCases.Accounts.Get;
using ExaltIt.BankAccount.UseCases.Interfaces;
using MediatR;
using MediatR.Pipeline;
using Module = Autofac.Module;

namespace ExaltIt.BankAccount.Infrastructure;

public class AutofacInfrastructureModule : Module
{
  private readonly List<Assembly> _assemblies = [];

  public AutofacInfrastructureModule(Assembly? callingAssembly = null)
  {
    AddToAssembliesIfNotNull(callingAssembly);
  }

  private void AddToAssembliesIfNotNull(Assembly? assembly)
  {
    if (assembly != null)
    {
      _assemblies.Add(assembly);
    }
  }

  private void LoadAssemblies()
  {
    var coreAssembly = Assembly.GetAssembly(typeof(Account));
    var infrastructureAssembly = Assembly.GetAssembly(typeof(AutofacInfrastructureModule));
    var useCasesAssembly = Assembly.GetAssembly(typeof(GetAccountQuery));

    AddToAssembliesIfNotNull(coreAssembly);
    AddToAssembliesIfNotNull(infrastructureAssembly);
    AddToAssembliesIfNotNull(useCasesAssembly);
  }

  protected override void Load(ContainerBuilder builder)
  {
    LoadAssemblies();
    RegisterEF(builder);
    RegisterMediatR(builder);

    builder.RegisterType<TransactionNotificationService>().As<ITransactionNotificationService>()
     .InstancePerLifetimeScope();
  }

  private static void RegisterEF(ContainerBuilder builder)
  {
    builder.RegisterGeneric(typeof(EfRepository<>))
      .As(typeof(IRepository<>))
      .As(typeof(IReadRepository<>))
      .InstancePerLifetimeScope();
  }

  private void RegisterMediatR(ContainerBuilder builder)
  {
    builder
      .RegisterType<Mediator>()
      .As<IMediator>()
      .InstancePerLifetimeScope();

    builder
      .RegisterGeneric(typeof(LoggingBehavior<,>))
      .As(typeof(IPipelineBehavior<,>))
      .InstancePerLifetimeScope();

    builder
      .RegisterType<MediatRDomainEventDispatcher>()
      .As<IDomainEventDispatcher>()
      .InstancePerLifetimeScope();

    var mediatrOpenTypes = new[]
    {
      typeof(IRequestHandler<,>),
      typeof(IRequestExceptionHandler<,,>),
      typeof(IRequestExceptionAction<,>),
      typeof(INotificationHandler<>),
    };

    foreach (var mediatrOpenType in mediatrOpenTypes)
    {
      builder
        .RegisterAssemblyTypes(_assemblies.ToArray())
        .AsClosedTypesOf(mediatrOpenType)
        .AsImplementedInterfaces();
    }
  }
}
