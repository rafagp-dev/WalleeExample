using Microsoft.AspNetCore.Mvc;
using Wallee.Model;

namespace WalleeExample.Controllers;

[ApiController]
[Route("/example-controller")]
public class ExampleController
{
    private readonly IExampleService _exampleService;

    public ExampleController(IExampleService exampleService)
    {
        _exampleService = exampleService;
    }

    [HttpPost("create-costumer")]
    public async Task<IActionResult> CreateCostumer(CustomerCreate customerCreate)
    {
        var customer = await _exampleService.CreateCostumer(customerCreate);
        return new JsonResult(customer);
    }

}