using System;
using Entities;

namespace ServiceContracts.CountryDTO
{
    /// <summary>
    /// DTO Class that is used as a return type for most of CountriesService methods
    /// </summary>
    public class CountryResponseDTO
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }

        // It compares the current object to another object of CountryResponse type and returns true,
        // if both values are the same otherwise returns false
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(CountryResponseDTO)) return false;

            CountryResponseDTO objToCompare = (CountryResponseDTO)obj;

            return this.CountryId == objToCompare.CountryId 
                && this.CountryName == objToCompare.CountryName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class CountryExtensions
    {
        public static CountryResponseDTO ToCountryResponseDTO(this Country country)
        {
            return new CountryResponseDTO
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName
            };
        }
    }
}
