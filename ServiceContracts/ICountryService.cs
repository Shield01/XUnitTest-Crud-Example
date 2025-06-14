using ServiceContracts.CountryDTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating country entity
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryRequestDTO">Country object to be added</param>
        /// <returns>Returns the same country object as CountryResponseDTO,
        /// after adding the country object to the database/list of countries </returns>
        CountryResponseDTO AddCountry(CountryRequestDTO? countryRequestDTO);

        /// <summary>
        /// Returns all countries from the list/Database
        /// </summary>
        /// <returns>All countries on the list/Database as a List of CountryResponseDTO</returns>
        List<CountryResponseDTO> GetAllCountries();

        /// <summary>
        /// Returns a country object based on a given country Id
        /// </summary>
        /// <param name="countryId">Country ID (Guid) to search for</param>
        /// <returns>Matching country as a CountryResponseDTO object</returns>
        CountryResponseDTO? GetCountryByCountryId(Guid? countryId);
    }
}
