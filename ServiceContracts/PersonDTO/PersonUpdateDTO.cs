using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.PersonDTO
{

    /// <summary>
    /// Acts as a DTO for updating an existing person
    /// </summary>
    public class PersonUpdateDTO
    {
        [Required(ErrorMessage="Person ID cannot be blank")]
        public Guid PersonId { get; set; }

        [Required(ErrorMessage = "PersonName cannot be blank")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Email cannot be blank")]
        [EmailAddress(ErrorMessage = "Email value should be a valid email")]
        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public GenderOptions? Gender { get; set; }

        public Guid? CountryId { get; set; }

        public string? Address { get; set; }

        public bool? ReceiveNewsLetter { get; set; }

        /// <summary>
        /// Converts the current object of the PersonRequestDTO to a new object of the Person type
        /// </summary>
        /// <returns>An object of Person type</returns>
        public Person toPerson()
        {
            return new Person()
            {
                PersonId = PersonId,
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                CountryId = CountryId,
                Address = Address,
                ReceiveNewsLetter = ReceiveNewsLetter,
            };
        }
    }
}
