using ServiceContracts.Enums;
using ServiceContracts.PersonDTO;
using System;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person entity
    /// </summary>
    /// <param name="personRequestDTO"></param>
    /// <returns></returns>
    public interface IPersonsService
    {
        /// <summary>
        /// Adds a new person into the existing List of persons
        /// </summary>
        /// <param name="personRequestDTO">The object of the person to be added</param>
        /// <returns>The details of the person which was added, alongside the person's id</returns>
        PersonResponseDTO AddPerson(PersonRequestDTO? personRequestDTO);

        /// <summary>
        /// Fetches all existing persons from the List/database
        /// </summary>
        /// <returns>Returns a list of objects of type PersonResponseDTO</returns>
        List<PersonResponseDTO> GetAllPersons();

        /// <summary>
        /// Returns the person object base on the given person id
        /// </summary>
        /// <param name="personId"></param>
        /// <returns>The Matching person object</returns>
        PersonResponseDTO? GetPersonByPersonId(Guid personId);

        /// <summary>
        /// Returns all person objects that matches with the given search field and search string
        /// </summary>
        /// <param name="searchBy">Property to check</param>
        /// <param name="searchString">String to search for in the property above</param>
        /// <returns>Returns all mathcing persons based on the given search field and search string</returns>
        List<PersonResponseDTO> GetFilteredPersons(string? searchBy, string? searchString);

        /// <summary>
        /// Returns sorted list of Persons
        /// </summary>
        /// <param name="allPersons">Represents the list of persons to be sorted</param>
        /// <param name="sortBy">The name of the property, based on which the list should be sorted</param>
        /// <param name="sortOrder">ASC or DESC</param>
        /// <returns>Returns a sorted list of Person as a List of PersonResponseDTO</returns>
        List<PersonResponseDTO> GetSortedPersons(List<PersonResponseDTO> allPersons, string sortBy, SortOrderOptions sortOrder);

        /// <summary>
        /// Updates the specified person details based on the given person ID
        /// </summary>
        /// <param name="personUpdateDTO">Person details to update, 
        /// including PersonId which would be used to find the person object to be updated</param>
        /// <returns>Returns the updated Person object in the form of PersonResponseDTO</returns>
        PersonResponseDTO UpdatePerson(PersonUpdateDTO? personUpdateDTO);

        /// <summary>
        /// Deleates a person based on the given person id
        /// </summary>
        /// <param name="personId">PersonId of the person to be deleted</param>
        /// <returns>Returns true if the deletion is successful, otherwise it returns false</returns>
        bool DeletePerson(Guid? personId);
    }
}
