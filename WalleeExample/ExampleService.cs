using Wallee.Model;
using Wallee.Service;

namespace WalleeExample;

public interface IExampleService
{
    Task<Customer> CreateCostumer(CustomerCreate customer);
}

public class ExampleService : IExampleService
{
    private readonly ICustomerService _customerService;
    private readonly IConfiguration _cfg;

    public ExampleService(ICustomerService customerService, IConfiguration cfg)
    {
        _customerService = customerService;
        _cfg = cfg;
    }
    
    public Task<Customer> CreateCostumer(CustomerCreate customer)
    {
        var res = _customerService.Create(_cfg.GetValue<int>("Walle:SpaceId"), customer);
        return Task.FromResult(res);
    }
}