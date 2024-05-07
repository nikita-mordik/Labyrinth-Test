namespace Infrastructure.Services.Identifiers
{
  public interface IIdentifierService : IService
  {
    int Next();
  }
}