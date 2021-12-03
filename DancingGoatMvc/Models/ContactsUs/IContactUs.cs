using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DancingGoat.Models.ContactsUs
{
    public interface IContactUs
    {
        string Name { get; }


        string Phone { get; }


        string Email { get; }


        string ZIP { get; }


        string Street { get; }


        string City { get; }


        string Country { get; }
    }
}