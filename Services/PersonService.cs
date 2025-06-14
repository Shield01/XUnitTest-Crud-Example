using Entities;
using ServiceContracts;
using ServiceContracts.Enums;
using ServiceContracts.PersonDTO;
using Services.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class PersonService : IPersonsService
    {
        private readonly List<Person> _people;
        private readonly ICountryService _countryService;

        public PersonService()
        {
            _people = new List<Person>();
            _countryService = new CountriesService();
        }

        private PersonResponseDTO ConvertPersonToPersonResponseDTO(Person person)
        {
            PersonResponseDTO personResponseDTO = person.ToPersonResponseDTO();

            personResponseDTO.CountryName = _countryService.GetCountryByCountryId(person.CountryId)?.CountryName;

            return personResponseDTO;
        }

        public PersonResponseDTO AddPerson(PersonRequestDTO? personRequestDTO)
        {
            // Check if PersonRequestDTO is null

            ArgumentNullException.ThrowIfNull(personRequestDTO);

            // Model Validations

            ValidationHelper.ModelValidation(personRequestDTO);

            // Convert personRequestDTO to Person type
            Person person = personRequestDTO.toPerson();

            // Generate PersonId
            person.PersonId = Guid.NewGuid();

            // Add person object to the datastore

            _people.Add(person);

            // Convert Person object to PersonResponseDTO type

            return ConvertPersonToPersonResponseDTO(person);
        }

        public List<PersonResponseDTO> GetAllPersons()
        {
            return _people.Select(person => person.ToPersonResponseDTO()).ToList();
        }

        public PersonResponseDTO? GetPersonByPersonId(Guid personId)
        {
            // Check if personId is null

            ArgumentNullException.ThrowIfNull(personId);

            Person? person = _people.FirstOrDefault(person => person.PersonId == personId);

            if(person == null)
            {
                return null;
            }

            return person.ToPersonResponseDTO();
        }

        public List<PersonResponseDTO> GetFilteredPersons(string? searchBy, string? searchString)
        {
            List<PersonResponseDTO> allPersons = GetAllPersons();

            List<PersonResponseDTO> matchingPersons = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString)) {
                return matchingPersons;
            }

            switch (searchBy) 
            {
                case nameof(Person.PersonName):
                    matchingPersons = allPersons.Where(temp => 
                    (!string.IsNullOrEmpty(temp.PersonName)?
                    temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Email):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Email) ?
                    temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.DateOfBirth):
                    matchingPersons = allPersons.Where(temp =>
                    (temp.DateOfBirth != null) ?
                    temp.DateOfBirth.Value.ToString("dd MMMM yyyy")
                    .Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                case nameof(Person.Gender):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Gender) ?
                    temp.Gender.StartsWith(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.CountryId):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.CountryName) ?
                    temp.CountryName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Address):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Address) ?
                    temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                default: return matchingPersons;
            }

            return matchingPersons;
        }

        public List<PersonResponseDTO> GetSortedPersons(List<PersonResponseDTO> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
            {
                return allPersons;
            }

            List<PersonResponseDTO> sortedPersonsToReturn =
                (sortBy, sortOrder) switch
                {
                    (nameof(PersonResponseDTO.PersonName), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                    (nameof(PersonResponseDTO.PersonName), SortOrderOptions.DESC)
                    => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                    (nameof(PersonResponseDTO.Email), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                    (nameof(PersonResponseDTO.Email), SortOrderOptions.DESC)
                    => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                    (nameof(PersonResponseDTO.DateOfBirth), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),

                    (nameof(PersonResponseDTO.DateOfBirth), SortOrderOptions.DESC)
                    => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),

                    (nameof(PersonResponseDTO.Age), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.Age).ToList(),

                    (nameof(PersonResponseDTO.Age), SortOrderOptions.DESC)
                    => allPersons.OrderByDescending(temp => temp.Age).ToList(),

                    (nameof(PersonResponseDTO.Gender), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                    (nameof(PersonResponseDTO.Gender), SortOrderOptions.DESC)
                    => allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                    (nameof(PersonResponseDTO.CountryName), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.CountryName, StringComparer.OrdinalIgnoreCase).ToList(),

                    (nameof(PersonResponseDTO.CountryName), SortOrderOptions.DESC)
                    => allPersons.OrderByDescending(temp => temp.CountryName, StringComparer.OrdinalIgnoreCase).ToList(),

                    (nameof(PersonResponseDTO.Address), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                    (nameof(PersonResponseDTO.Address), SortOrderOptions.DESC)
                    => allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                    (nameof(PersonResponseDTO.ReceiveNewsLetter), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.ReceiveNewsLetter).ToList(),

                    (nameof(PersonResponseDTO.ReceiveNewsLetter), SortOrderOptions.DESC)
                    => allPersons.OrderByDescending(temp => temp.ReceiveNewsLetter).ToList(),

                    _ => allPersons
                };
            return sortedPersonsToReturn;
        }

        public PersonResponseDTO UpdatePerson(PersonUpdateDTO? personUpdateDTO)
        {
            if (personUpdateDTO == null) 
            { 
                throw new ArgumentNullException(nameof(Person));
            }

            // Validation
            ValidationHelper.ModelValidation(personUpdateDTO);

            // Get the person object to update
            Person? personToUpdate = _people.FirstOrDefault(temp => temp.PersonId == personUpdateDTO.PersonId);

            if (personToUpdate == null) 
            {
                throw new ArgumentException("Given person id does not exist");
            }

            // Update all details 
            personToUpdate.PersonName = personUpdateDTO.PersonName;
            personToUpdate.Email = personUpdateDTO.Email;
            personToUpdate.DateOfBirth = personUpdateDTO.DateOfBirth;
            personToUpdate.Gender = personUpdateDTO.Gender.ToString();
            personToUpdate.CountryId = personUpdateDTO.CountryId;
            personToUpdate.Address = personUpdateDTO.Address;
            personToUpdate.ReceiveNewsLetter = personUpdateDTO.ReceiveNewsLetter;

            return personToUpdate.ToPersonResponseDTO();
        }

        public bool DeletePerson(Guid? personId)
        {
            if (personId == null) 
            { 
                throw new ArgumentNullException(nameof(personId));
            }

            Person? person = _people.FirstOrDefault(temp => temp.PersonId == personId);

            if (person == null) return false;

            _people.RemoveAll(temp => temp.PersonId == personId);

            return true;
        }
    }
}
