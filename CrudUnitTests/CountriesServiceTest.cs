using ServiceContracts;
using ServiceContracts.CountryDTO;
using Services;
using System;
using System.Collections.Generic;

namespace CrudUnitTests
{
    public class CountriesServiceTest
    {
        private readonly ICountryService _countriesService;

        // Constructor
        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

        #region AddCountry
        //When CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arrange
            CountryRequestDTO? request = null;


            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _countriesService.AddCountry(request);
            });
        }

        //When CountryName is null, it should ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            //Arrange
            CountryRequestDTO? request = new CountryRequestDTO() { CountryName = null};


            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesService.AddCountry(request);
            });
        }

        //When the CountryName is duplicated, it should throw ArgumentException

        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            //Arrange
            CountryRequestDTO? request1 = new CountryRequestDTO() { CountryName = "Nigeria"};
            CountryRequestDTO? request2 = new() { CountryName = "Nigeria" };


            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }

        // When you supply proper country name, it should insert (add) the country into the existing
        // list of countries

        [Fact]
        public void AddCountry_CountryAddedIsValid()
        {
            //Arrange
            CountryRequestDTO? request = new CountryRequestDTO() { CountryName = "Nigeria" };

            //Act
            CountryResponseDTO response = _countriesService.AddCountry(request);

            List<CountryResponseDTO> responseList = _countriesService.GetAllCountries();

            //Assert
            Assert.True(response.CountryId != Guid.Empty);
            Assert.Contains(response, responseList);
        }
        #endregion

        #region GetCountries
        [Fact]
        // The list of countries should be empty by default (before adding any countries)
        public void GetAllCountries_EmptyList()
        {
            // Act
            List<CountryResponseDTO> countryResponseDTOs = _countriesService.GetAllCountries();

            // Assert
            Assert.Empty(countryResponseDTOs);
        }

        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            List<CountryRequestDTO> countryRequestDTOs = new List<CountryRequestDTO>
            {
                new CountryRequestDTO() { CountryName = "Nigeria" },
                new CountryRequestDTO() { CountryName = "Monaco" },
                new CountryRequestDTO() { CountryName = "UAE" },
                new CountryRequestDTO() { CountryName = "Kenya" }
            };

            // Act
            List<CountryResponseDTO> countryResponseDTOs = new List<CountryResponseDTO>();

            foreach (CountryRequestDTO countryRequestDTO in countryRequestDTOs) 
            {
                countryResponseDTOs.Add(_countriesService.AddCountry(countryRequestDTO));
            }

            List<CountryResponseDTO> responseDTOs =  _countriesService.GetAllCountries();

            // Read each element from responseDTOs 
            foreach(CountryResponseDTO expectedResponse in countryResponseDTOs)
            {
                Assert.Contains(expectedResponse, responseDTOs);
            }
        }
        #endregion

        #region GetCountryByCountryId
        [Fact]
        //If null is passed as Guid, null should be returned
        public void GetCountryByCountryId_NullCountryId()
        {
            // Arrange 
            Guid? countryId = null;

            // Act
            CountryResponseDTO? country = _countriesService.GetCountryByCountryId(countryId);

            Assert.Null(country);
        }

        [Fact]
        // If a proper CountryId is passed as an argument, a proper CountryResponseDTO
        // object should be returned
        public void GetCountryByCountryId_ValidCountryId()
        {
            CountryRequestDTO countryRequestDTO = new CountryRequestDTO() { CountryName = "Nigeria"};

            CountryResponseDTO createdCountry = _countriesService.AddCountry(countryRequestDTO);

            // Act 

            CountryResponseDTO? fetchedCountry = _countriesService.GetCountryByCountryId(createdCountry.CountryId);

            // Assert 

            Assert.Equal(fetchedCountry, createdCountry);

        }
        #endregion
    }
}
