using AutoMapper;
using DataAccessLayer;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomerController : ControllerBase
{
    ICustomerService customerService;
    IMapper mapper;
    IValidator<CreateCustomerDTO> createCustomerDTOValidator;

    public CustomerController(
        ICustomerService customerService,
        IMapper mapper,
        IValidator<CreateCustomerDTO> createCustomerDTOValidator)
    {
        this.customerService = customerService;
        this.mapper = mapper;
        this.createCustomerDTOValidator = createCustomerDTOValidator;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var customers = customerService.GetAllCustomers();
        return Ok(customers);
    }

    [HttpGet("{id:int}")]
    public IActionResult Get([FromRoute] int id)
    {
        var customer = customerService.GetAllCustomers()
                                      .FirstOrDefault(x => x.Id == id);

        if (customer == null)
            return NotFound();

        var customerDTO = mapper.Map<CustomerDTO>(customer);

        return Ok(customerDTO);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateCustomerDTO createCustomerDTO)
    {
        var validationResult = createCustomerDTOValidator.Validate(createCustomerDTO);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(createCustomerDTO);
    }
}