using Microsoft.AspNetCore.Mvc;
using Student.Web.Api.Data;
using Student.Web.Api.Dto;
using Student.Web.Api.Models;

namespace Student.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private ILogger<StudentsController> _logger;
        private readonly IPupilRepository _pupilRepository;

        // Constructor injection (Dependency Injection)
        public StudentsController(ILogger<StudentsController> logger,
            IPupilRepository pupilRepository
        )
        {
            _logger = logger;
            _pupilRepository = pupilRepository;
        }

        // HTTP GET method for retrieving a list of pupils
        [HttpGet()]
        public async Task<IActionResult> GetList()
        {
            // Retrieving data from the repository (No explicit creational design here)
            var pupils = await _pupilRepository.GetAllAsync();
            _logger.LogInformation("Getting all list");
            return Ok(pupils);
        }

        // HTTP POST method for creating a new pupil
        [HttpPost()]
        public async Task<IActionResult> Post(PupilDto input)
        {
            // Creational design: Creating a new Pupil instance
            var newPupil = new Pupil(input.StudentId);
            newPupil.LastName = input.LastName;
            newPupil.FirsName = input.FirsName;
            newPupil.MiddleName = input.MiddleName;

            // Adding the new Pupil to the repository
            _pupilRepository.Add(newPupil);

            // Saving changes in the repository (No explicit creational design here)
            if (await _pupilRepository.SaveAllChangesAsync())
            {
                return Ok(input);
            }

            return BadRequest("May Error");
        }

        // HTTP PUT method for updating an existing pupil
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(PupilDto input)
        {
            // Updating an existing Pupil instance
            var pupil = await _pupilRepository.GetById(input.StudentId);
            pupil.LastName = input.LastName;
            pupil.FirsName = input.FirsName;
            pupil.MiddleName = input.MiddleName;

            // Saving changes in the repository (No explicit creational design here)
            if (await _pupilRepository.SaveAllChangesAsync())
            {
                return Ok("Updated Na!");
            }

            return BadRequest("May Error");
        }

        // HTTP DELETE method for deleting an existing pupil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            // Retrieving an existing Pupil instance by ID
            var pupil = await _pupilRepository.GetById(id);

            if (pupil != null)
            {
                // Deleting the Pupil from the repository
                _pupilRepository.Delete(pupil);

                // Saving changes in the repository (No explicit creational design here)
                if (await _pupilRepository.SaveAllChangesAsync())
                {
                    return Ok("Finis Na!");
                }
            }

            return BadRequest("May Error");
        }
    }
}
/*1. Structural Design:
Structural design focuses on organizing your code in a way that provides clarity, modularity, and maintainability.
Here are key aspects to look for:

a. Project Structure:
Look at how your project is organized. Are there clear directories or packages for controllers, models, services, middleware, 
and other components?
A well-structured project separates concerns, making it easier to navigate and maintain.

b. Middleware:
Identify middleware components in your application. 
Middleware handles structural concerns that are applied across multiple routes or controllers, such as authentication, 
logging, error handling, etc.

c. Routing:
Evaluate your routing structure. How are incoming HTTP requests mapped to the corresponding controllers or handlers?
Consistent and clear routing contributes to a well-organized API.

d. Controllers:
Examine your controllers. Each controller should have a well-defined responsibility,
handling specific HTTP methods and related actions.
Controllers often interact with services, models, and repositories.

e. Models:
Models represent the data structures of your application. Identify how models are defined and used in your API.
Models may handle the interaction with databases or other data storage.



2. Creational Design:
Creational design deals with object creation and instantiation. 
Look for patterns and principles that guide the creation of objects in your application:

a. Dependency Injection (DI):
Identify dependencies injected into your classes, typically through constructor injection.
This promotes loose coupling and testability.

b. Factory Patterns:
Look for the use of factory patterns to encapsulate object creation.
Factories centralize the creation logic, providing flexibility and maintainability.

c. Service Layer:
Check for the presence of a service layer responsible for business logic.
This layer may handle the creation and management of objects needed for specific functionalities.

d. Singletons:
Identify instances where a single instance of a class is ensured throughout the application.
Be cautious with singletons as they introduce global state.

e. Data Transfer Objects (DTOs):
Look for the use of Data Transfer Objects (DTOs) to encapsulate data sent over the network.
DTOs separate the internal representation of data from what is exposed via the API.*/
