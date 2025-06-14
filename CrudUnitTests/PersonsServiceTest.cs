using ServiceContracts;
using ServiceContracts.CountryDTO;
using ServiceContracts.Enums;
using ServiceContracts.PersonDTO;
using Services;
using System;
using Xunit.Abstractions;

namespace CrudUnitTests
{
    public class PersonsServiceTest
    {
        private readonly IPersonsService _personsService;
        private readonly ICountryService _countryService;
        private readonly ITestOutputHelper _testOutputHelper;

        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personsService = new PersonService();
            _countryService = new CountriesService();
            _testOutputHelper = testOutputHelper;
        }

        #region AddPerson
        [Fact]
        // When a null value is supplied as a PersonRequestDTO, it should throw an ArgumentNullException
        public void AddPerson_NullPerson()
        {
            // Arrange
            PersonRequestDTO? personRequestDTO = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => 
            {
                _personsService.AddPerson(personRequestDTO);
            });
        }

        [Fact]
        // When a person's name is null, it should throw an ArgumentException
        public void AddPerson_PersonNameIsNull()
        {
            PersonRequestDTO? personRequestDTO = new PersonRequestDTO() { PersonName = null };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.AddPerson(personRequestDTO);
            });
        }

        [Fact]
        // When a proper person's details is supplied, the addition should be carried out successfully,
        // and it should return an object of the PersonResponseDTO, which includes the Person's generated id
        public void AddPerson_ProperPersonDetails()
        {
            // Arrange
            PersonRequestDTO? personRequestDTO = new PersonRequestDTO()
            {
                PersonName = "King Hussein",
                Email = "kingHusseins@email.com",
                Address = "A dummy address",
                CountryId = new Guid(),
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            // Act
            PersonResponseDTO createdPerson = _personsService.AddPerson(personRequestDTO);
            List<PersonResponseDTO> personsList = _personsService.GetAllPersons();

            // Assert
            Assert.True(createdPerson.PersonId != Guid.Empty);
            Assert.Contains(createdPerson, personsList);
        }
        #endregion

        #region GetPersonByPersonId
        [Fact]
        // If null is passed as the person id, it should return null
        public void GetPersonByPersonId_NullPersonId()
        {
            // Arrange 
            Guid guid = Guid.Empty;

            // Act
            PersonResponseDTO? fetchedPerson = _personsService.GetPersonByPersonId(guid);

            // Assert 
            Assert.Null(fetchedPerson);
        }

        [Fact]
        // If a valid person id is supplied. an appropriate person object should be returned
        public void GetPersonByPersonId_ValidPersonId()
        {
            // Arrange
            CountryRequestDTO countryToCreate = new CountryRequestDTO() 
            {
                CountryName = "Canada"
            };

            CountryResponseDTO createdCountry = _countryService.AddCountry(countryToCreate);

            // Act
            PersonRequestDTO personToCreate = new PersonRequestDTO()
            {
                PersonName = "King Hussein",
                Email = "kinghussein@email.com",
                Address = "Some dummy address",
                CountryId = createdCountry.CountryId,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                Gender = GenderOptions.Male,
                ReceiveNewsLetter = true
            };

            PersonResponseDTO createdPerson = _personsService.AddPerson(personToCreate);

            PersonResponseDTO? fetchedPerson = _personsService.GetPersonByPersonId(createdPerson.PersonId);

            // Assert
            Assert.Equal(createdPerson, fetchedPerson);
        }
        #endregion

        #region GetAllPersons
        [Fact]
        // The GetAllPersons method should return an empty list by default
        public void GetAllPersons_EmptyList()
        {
            // Act 
            List<PersonResponseDTO> fetchedPersons = _personsService.GetAllPersons();

            Assert.Empty(fetchedPersons);
        }

        [Fact]
        // All persons that have been added to the list/database should be returned when GetAllPersons is called
        public void GetAllPersons_AddFewPersons()
        {
            // Arrange
            CountryRequestDTO country1 = new CountryRequestDTO() { CountryName = "Nigeria" };
            CountryRequestDTO country2 = new CountryRequestDTO() { CountryName = "Monaco" };
            CountryRequestDTO country3 = new CountryRequestDTO() { CountryName = "UAE" };
            CountryRequestDTO country4 = new CountryRequestDTO() { CountryName = "Kenya" };

            CountryResponseDTO createdCountry1 = _countryService.AddCountry(country1);
            CountryResponseDTO createdCountry2 = _countryService.AddCountry(country2);
            CountryResponseDTO createdCountry3 = _countryService.AddCountry(country3);
            CountryResponseDTO createdCountry4 = _countryService.AddCountry(country4);

            PersonRequestDTO? person1 = new PersonRequestDTO()
            {
                PersonName = "King Hussein1",
                Email = "kingHusseins@email1.com",
                Address = "A dummy address",
                CountryId = createdCountry1.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            PersonRequestDTO? person2 = new PersonRequestDTO()
            {
                PersonName = "King Hussein2",
                Email = "kingHusseins@email2.com",
                Address = "A dummy address",
                CountryId = createdCountry2.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            PersonRequestDTO? person3 = new PersonRequestDTO()
            {
                PersonName = "King Hussein3",
                Email = "kingHusseins@email3.com",
                Address = "A dummy address",
                CountryId = createdCountry3.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            PersonRequestDTO? person4 = new PersonRequestDTO()
            {
                PersonName = "King Hussein4",
                Email = "kingHusseins@email4.com",
                Address = "A dummy address",
                CountryId = createdCountry4.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            List<PersonRequestDTO> personsToCreate = new List<PersonRequestDTO>()
            {
                person1,
                person2,
                person3,
                person4
            };

            List<PersonResponseDTO> createdPeople = new List<PersonResponseDTO>();

            foreach(PersonRequestDTO person in personsToCreate)
            {
                PersonResponseDTO createdPerson =  _personsService.AddPerson(person);

                createdPeople.Add(createdPerson);
            }

            // Print created people
            _testOutputHelper.WriteLine("Expected list:");
            foreach (PersonResponseDTO createdPerson in createdPeople)
            {
                _testOutputHelper.WriteLine(createdPerson.ToString());
            }

            // Act

            List<PersonResponseDTO> fetchedPersons = _personsService.GetAllPersons();

            // Print fetchedPersons
            _testOutputHelper.WriteLine("Actual list:");

            foreach(PersonResponseDTO person in fetchedPersons)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            // Assert
            foreach (PersonResponseDTO createdPerson in createdPeople) 
            {
                Assert.Contains(createdPerson, fetchedPersons);
            }

        }
        #endregion

        #region GetFilteredPersons
        // If the search text is empty and search by is "PersonName", it should return all persons
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            // Arrange
            CountryRequestDTO country1 = new CountryRequestDTO() { CountryName = "Nigeria" };
            CountryRequestDTO country2 = new CountryRequestDTO() { CountryName = "Monaco" };
            CountryRequestDTO country3 = new CountryRequestDTO() { CountryName = "UAE" };
            CountryRequestDTO country4 = new CountryRequestDTO() { CountryName = "Kenya" };

            CountryResponseDTO createdCountry1 = _countryService.AddCountry(country1);
            CountryResponseDTO createdCountry2 = _countryService.AddCountry(country2);
            CountryResponseDTO createdCountry3 = _countryService.AddCountry(country3);
            CountryResponseDTO createdCountry4 = _countryService.AddCountry(country4);

            PersonRequestDTO? person1 = new PersonRequestDTO()
            {
                PersonName = "King Hussein1",
                Email = "kingHusseins@email1.com",
                Address = "A dummy address",
                CountryId = createdCountry1.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            PersonRequestDTO? person2 = new PersonRequestDTO()
            {
                PersonName = "King Hussein2",
                Email = "kingHusseins@email2.com",
                Address = "A dummy address",
                CountryId = createdCountry2.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            PersonRequestDTO? person3 = new PersonRequestDTO()
            {
                PersonName = "King Hussein3",
                Email = "kingHusseins@email3.com",
                Address = "A dummy address",
                CountryId = createdCountry3.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            PersonRequestDTO? person4 = new PersonRequestDTO()
            {
                PersonName = "King Hussein4",
                Email = "kingHusseins@email4.com",
                Address = "A dummy address",
                CountryId = createdCountry4.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            List<PersonRequestDTO> personsToCreate = new List<PersonRequestDTO>()
            {
                person1,
                person2,
                person3,
                person4
            };

            List<PersonResponseDTO> createdPeople = new List<PersonResponseDTO>();

            foreach (PersonRequestDTO person in personsToCreate)
            {
                PersonResponseDTO createdPerson = _personsService.AddPerson(person);

                createdPeople.Add(createdPerson);
            }

            // Print created people
            _testOutputHelper.WriteLine("Expected list:");
            foreach (PersonResponseDTO createdPerson in createdPeople)
            {
                _testOutputHelper.WriteLine(createdPerson.ToString());
            }

            // Act

            List<PersonResponseDTO> fetchedPersons = _personsService.GetFilteredPersons(nameof(PersonResponseDTO.PersonName), "");

            // Print fetchedPersons
            _testOutputHelper.WriteLine("Actual list:");

            foreach (PersonResponseDTO person in fetchedPersons)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            // Assert
            foreach (PersonResponseDTO createdPerson in createdPeople)
            {
                Assert.Contains(createdPerson, fetchedPersons);
            }

        }

        // First we will add few persons, and then we will search based on the person name with some search string
        // And it should return the matching persons
        [Fact]
        public void GetFilteredPersons_()
        {
            // Arrange
            CountryRequestDTO country1 = new CountryRequestDTO() { CountryName = "Nigeria" };
            CountryRequestDTO country2 = new CountryRequestDTO() { CountryName = "Monaco" };
            CountryRequestDTO country3 = new CountryRequestDTO() { CountryName = "UAE" };
            CountryRequestDTO country4 = new CountryRequestDTO() { CountryName = "Kenya" };

            CountryResponseDTO createdCountry1 = _countryService.AddCountry(country1);
            CountryResponseDTO createdCountry2 = _countryService.AddCountry(country2);
            CountryResponseDTO createdCountry3 = _countryService.AddCountry(country3);
            CountryResponseDTO createdCountry4 = _countryService.AddCountry(country4);

            PersonRequestDTO? person1 = new PersonRequestDTO()
            {
                PersonName = "KING Hussein",
                Email = "kingHusseins@email1.com",
                Address = "A dummy address",
                CountryId = createdCountry1.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            PersonRequestDTO? person2 = new PersonRequestDTO()
            {
                PersonName = "Prince Wahab",
                Email = "kingHusseins@email2.com",
                Address = "A dummy address",
                CountryId = createdCountry2.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            PersonRequestDTO? person3 = new PersonRequestDTO()
            {
                PersonName = "Duke Malik",
                Email = "kingHusseins@email3.com",
                Address = "A dummy address",
                CountryId = createdCountry3.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            PersonRequestDTO? person4 = new PersonRequestDTO()
            {
                PersonName = "Duke Lanre",
                Email = "kingHusseins@email4.com",
                Address = "A dummy address",
                CountryId = createdCountry4.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            List<PersonRequestDTO> personsToCreate = new List<PersonRequestDTO>()
            {
                person1,
                person2,
                person3,
                person4
            };

            List<PersonResponseDTO> createdPeople = new List<PersonResponseDTO>();

            foreach (PersonRequestDTO person in personsToCreate)
            {
                PersonResponseDTO createdPerson = _personsService.AddPerson(person);

                createdPeople.Add(createdPerson);
            }

            // Print created people
            _testOutputHelper.WriteLine("Expected list:");
            foreach (PersonResponseDTO createdPerson in createdPeople)
            {
                _testOutputHelper.WriteLine(createdPerson.ToString());
            }

            // Act

            List<PersonResponseDTO> fetchedPersons = _personsService.GetFilteredPersons(nameof(PersonResponseDTO.PersonName), "in");

            // Print fetchedPersons
            _testOutputHelper.WriteLine("Actual list:");

            foreach (PersonResponseDTO person in fetchedPersons)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            // Assert
            foreach (PersonResponseDTO createdPerson in createdPeople)
            {
                if (createdPerson.PersonName != null)
                {
                    if (createdPerson.PersonName.Contains("in", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(createdPerson, fetchedPersons);
                    }
                }
            }
        }
        #endregion

        #region GetSortedPersons
        [Fact]
        // When we sort based on PersonName in DESC, it should return persons list in descending on PersonName
        public void GetSortedPersons_()
        {
            // Arrange
            CountryRequestDTO country1 = new CountryRequestDTO() { CountryName = "Nigeria" };
            CountryRequestDTO country2 = new CountryRequestDTO() { CountryName = "Monaco" };
            CountryRequestDTO country3 = new CountryRequestDTO() { CountryName = "UAE" };
            CountryRequestDTO country4 = new CountryRequestDTO() { CountryName = "Kenya" };

            CountryResponseDTO createdCountry1 = _countryService.AddCountry(country1);
            CountryResponseDTO createdCountry2 = _countryService.AddCountry(country2);
            CountryResponseDTO createdCountry3 = _countryService.AddCountry(country3);
            CountryResponseDTO createdCountry4 = _countryService.AddCountry(country4);

            PersonRequestDTO? person1 = new PersonRequestDTO()
            {
                PersonName = "KING Hussein",
                Email = "kingHusseins@email1.com",
                Address = "A dummy address",
                CountryId = createdCountry1.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            PersonRequestDTO? person2 = new PersonRequestDTO()
            {
                PersonName = "Prince Wahab",
                Email = "kingHusseins@email2.com",
                Address = "A dummy address",
                CountryId = createdCountry2.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            PersonRequestDTO? person3 = new PersonRequestDTO()
            {
                PersonName = "Duke Malik",
                Email = "kingHusseins@email3.com",
                Address = "A dummy address",
                CountryId = createdCountry3.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            PersonRequestDTO? person4 = new PersonRequestDTO()
            {
                PersonName = "Duke Lanre",
                Email = "kingHusseins@email4.com",
                Address = "A dummy address",
                CountryId = createdCountry4.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            List<PersonRequestDTO> personsToCreate = new List<PersonRequestDTO>()
            {
                person1,
                person2,
                person3,
                person4
            };

            List<PersonResponseDTO> createdPeople = new List<PersonResponseDTO>();

            foreach (PersonRequestDTO person in personsToCreate)
            {
                PersonResponseDTO createdPerson = _personsService.AddPerson(person);

                createdPeople.Add(createdPerson);
            }

            // Print created people
            _testOutputHelper.WriteLine("Expected list:");
            foreach (PersonResponseDTO createdPerson in createdPeople)
            {
                _testOutputHelper.WriteLine(createdPerson.ToString());
            }

            // Act
            List<PersonResponseDTO> allPersons = _personsService.GetAllPersons();

            List<PersonResponseDTO> sortedPersons = _personsService.GetSortedPersons(
                allPersons, 
                nameof(PersonResponseDTO.PersonName), 
                SortOrderOptions.DESC
            );

            // Print fetchedPersons
            _testOutputHelper.WriteLine("Actual list:");

            foreach (PersonResponseDTO person in sortedPersons)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            createdPeople = createdPeople.OrderByDescending(temp => temp.PersonName).ToList();


            // Assert
            for (int i = 0; i < createdPeople.Count; i++) 
            {
                Assert.Equal(createdPeople[i], sortedPersons[i]);
            }
        }
        #endregion

        #region UpdatePerson
        // When we supply null as PersonUpdateDTO, it should throw ArgumentNullException
        [Fact]
        public void Update_Person_NullPerson()
        {
            // Arrange
            PersonUpdateDTO? personUpdateDTO = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                _personsService.UpdatePerson(personUpdateDTO);
            });

        }

        // When we supply an invalid person id, it should throw ArgumentException
        [Fact]
        public void Update_Person_InValidPersonId()
        {
            // Arrange
            PersonUpdateDTO? personUpdateDTO = new PersonUpdateDTO() { PersonId = Guid.NewGuid() };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _personsService.UpdatePerson(personUpdateDTO);
            });

        }

        // When the person name is null, it should throw argument exception
        [Fact]
        public void Update_Person_NullPersonName()
        {
            // Arrange
            CountryRequestDTO countryRequestDTO = new CountryRequestDTO() { CountryName = "Nigeria" };

            CountryResponseDTO countryResponseDTO =  _countryService.AddCountry(countryRequestDTO);

            PersonRequestDTO personRequestDTO = new PersonRequestDTO()
            {
                PersonName = "King",
                Email = "kingsemail@email.com",
                Gender = GenderOptions.Male,
                CountryId = countryResponseDTO.CountryId
            };

            PersonResponseDTO createdPerson = _personsService.AddPerson(personRequestDTO);


            PersonUpdateDTO? personUpdateDTO = createdPerson.ToPersonUpdateDTO();

            personUpdateDTO.PersonName = null;

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _personsService.UpdatePerson(personUpdateDTO);
            });

        }

        // When valid details are supplied, the update should run successfully 
        [Fact]
        public void Update_Person_Valid()
        {
            // Arrange
            CountryRequestDTO countryRequestDTO = new CountryRequestDTO() { CountryName = "Nigeria" };

            CountryResponseDTO countryResponseDTO = _countryService.AddCountry(countryRequestDTO);

            PersonRequestDTO personRequestDTO = new PersonRequestDTO()
            {
                PersonName = "King Hussein",
                Email = "kingHusseins@email.com",
                Address = "A dummy address",
                CountryId = countryResponseDTO.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1860-01-01"),
                ReceiveNewsLetter = true
            };

            PersonResponseDTO createdPerson = _personsService.AddPerson(personRequestDTO);


            PersonUpdateDTO? personUpdateDTO = createdPerson.ToPersonUpdateDTO();

            personUpdateDTO.PersonName = "Oba";
            personUpdateDTO.Email = "oluepe@royalty.com";

            //Act
            PersonResponseDTO personObjectAfterUpdate = _personsService.UpdatePerson(personUpdateDTO);

            PersonResponseDTO fetchedPerson = _personsService.GetPersonByPersonId(personObjectAfterUpdate.PersonId);

            // Assert
            Assert.Equal(personObjectAfterUpdate, fetchedPerson);

        }
        #endregion

        #region DeletePerson
        // If you supply a valid person id, true should be returned 
        [Fact]
        public void DeletePerson_ValidPersonId()
        {
            // Arrange
            CountryRequestDTO countryRequestDTO = new CountryRequestDTO() { CountryName = "Nigeria" };

            CountryResponseDTO countryResponseDTO = _countryService.AddCountry(countryRequestDTO);

            PersonRequestDTO personRequestDTO = new PersonRequestDTO()
            {
                PersonName = "King",
                Email = "kingsemail@email.com",
                Gender = GenderOptions.Male,
                CountryId = countryResponseDTO.CountryId
            };

            PersonResponseDTO createdPerson = _personsService.AddPerson(personRequestDTO);

            // Act

            bool isDeleted = _personsService.DeletePerson(createdPerson.PersonId);

            // Assert  
            Assert.True(isDeleted);
        }


        // If you supply an invalid person id, false should be returned 
        [Fact]
        public void DeletePerson_InValidPersonId()
        {
            // Act
            bool isDeleted = _personsService.DeletePerson(Guid.NewGuid());

            // Assert  
            Assert.False(isDeleted);
        }
        #endregion
    }
}