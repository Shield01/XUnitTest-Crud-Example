using Entities;
using ServiceContracts;
using ServiceContracts.CountryDTO;

namespace Services
{
    public class CountriesService : ICountryService
    {
        private readonly List<Country> _countries;

        public CountriesService()
        {
            _countries = new List<Country>();
        }
        public CountryResponseDTO AddCountry(CountryRequestDTO? countryRequestDTO)
        {
            //Validation: countryRequestDTO cannot be null
            if (countryRequestDTO == null) 
            { 
                throw new ArgumentNullException(nameof(countryRequestDTO));
            }

            //Validation: countryName cannot be null 

            if (countryRequestDTO.CountryName == null)
            {
                throw new ArgumentException(nameof(countryRequestDTO.CountryName));
            }

            // Validation: Country name should not already exist

            if (_countries.Where(country => country.CountryName == countryRequestDTO.CountryName).Count() > 0)
            {
                throw new ArgumentException("Country already exists");
            }
            
            Country country = countryRequestDTO.ToCountry();

            country.CountryId = Guid.NewGuid();

            _countries.Add(country);

            return country.ToCountryResponseDTO();
        }

        public List<CountryResponseDTO> GetAllCountries()
        {

            return _countries.Select(country => country.ToCountryResponseDTO()).ToList();
        }

        public CountryResponseDTO? GetCountryByCountryId(Guid? countryId)
        {
            if (countryId == null) 
            {
                return null;
            }

            Country? fetchedCountry = _countries.FirstOrDefault(country => country.CountryId == countryId);

            if (fetchedCountry != null )
            {
                return fetchedCountry.ToCountryResponseDTO();
            } else
            {
                return null;
            }
                
        }
    }
}
