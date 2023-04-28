using Assignment4;
using System;
using System.Data.SqlClient;
using System.Text;

Course course = new Course()
{
    id = 1,
    Title = "Programming Fundamentals",
    Fees = 500.00,
    instructor = new Instructor()
    {
        id = 1,
        Name = "John Doe",
        Email = "johndoe@example.com",
        PresentAddress = new Address()
        {
            id = 1,
            Street = "123 Main Street",
            City = "Anytown",
            Country = "USA"
        },
        PermanentAddress = new Address()
        {
            id = 2,
            Street = "456 Oak Street",
            City = "Anytown",
            Country = "USA"
        },
        PhoneNumbers = new List<Phone>()
        {
            new Phone()
            {
                id = 1,
                Number = "123-456-7890",
                Extension = "123",
                CountryCode = "1"
            },
            new Phone()
            {
                id = 2,
                Number = "987-654-3210",
                Extension = "456",
                CountryCode = "1"
            }
        }
    },
    Topics = new List<Topic>()
    {
        new Topic()
        {
            id = 1,
            Title = "Introduction to Programming",
            Description = "This topic covers the basics of programming.",
            Sessions = new List<Session>()
            {
                new Session()
                {
                    id = 1,
                    DurationInHour = 2,
                    LearningObjective = "Understand the basics of programming."
                },
                new Session()
                {
                    id = 2,
                    DurationInHour = 2,
                    LearningObjective = "Write basic programs using a programming language."
                }
            }
        },
        new Topic()
        {
            id = 2,
            Title = "Data Structures and Algorithms",
            Description = "This topic covers data structures and algorithms.",
            Sessions = new List<Session>()
            {
                new Session()
                {
                    id = 3,
                    DurationInHour = 2,
                    LearningObjective = "Understand the basic data structures and algorithms."
                },
                new Session()
                {
                    id = 4,
                    DurationInHour = 2,
                    LearningObjective = "Implement  data structures and algorithms."
                }
            }
        }
    },
    Tests = new List<AdmissionTest>()
    {
        new AdmissionTest()
        {
            id = 1,
            StartDateTime = DateTime.Now.AddDays(7),
            EndDateTime = DateTime.Now.AddDays(8),
            TestFees = 50.00
        },
        new AdmissionTest()
        {
            id = 2,
            StartDateTime = DateTime.Now.AddDays(14),
            EndDateTime = DateTime.Now.AddDays(15),
            TestFees = 50.00
        }
    }
};



//InsertIntoDatabase i1 = new InsertIntoDatabase();
//i1.InsertObjectIntoDb(course, null, null);

//DatabaseManager dm = new DatabaseManager();
//dm.DeleteObject(course, null, null);


//Update x = new Update();

//course.Fees = 10000;



//x.UpdateObjectInDb(course, null, null);





//course.instructor.Name = "Dajjal Jalal";

//x.UpdateObjectInDb(course, null, null);
//string _connection = "Server=DESKTOP-8VMMQPN\\SQLEXPRESS;Database=assignment_4;Trusted_Connection=True;Encrypt=False";


//DatabaseConnector d1 = new DatabaseConnector(_connection);

//DataExtractor s1 = new DataExtractor(d1);

//List<Dictionary<string,object>> result =s1.ExtractAllData(course, null);


//DataPrinter dp1 = new DataPrinter();

//dp1.PrintData(result);

Guid input = new Guid();
Guid guid;

if (Guid.TryParse(input.ToString(), out guid))
{
    Console.WriteLine("Input is a valid GUID: " + guid);
}
else
{
    Console.WriteLine("Input is not a valid GUID");
}