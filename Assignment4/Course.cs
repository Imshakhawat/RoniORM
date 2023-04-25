using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    public class Course
    {
        public int id { get; set; }
        public string Title { get; set; }
        public double Fees { get; set; }
        public Instructor instructor { get; set; }
        public List<Topic> Topics { get; set; }
        public List<AdmissionTest> Tests { get; set; }
    }

    public class AdmissionTest
    {
        public int id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double TestFees { get; set; }
    }
    public class Topic
    {
        public int  id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Session> Sessions { get; set; }
    }

    public class Session
    {
        public int id { get; set; }
        public int DurationInHour { get; set; }
        public string LearningObjective { get; set; }
    }

    public class Instructor
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address PresentAddress { get; set; }
        public Address PermanentAddress { get; set; }
        public List<Phone> PhoneNumbers { get; set; }
    }

    public class Address
    {
        public int id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class Phone
    {
        public int id { get; set; }
        public string Number { get; set; }
        public string Extension { get; set; }
        public string CountryCode { get; set; }
    }

}
