using System;
using System.IO;
using System.Xml.Linq;

namespace os_lab_1
{
    static class Xml
    {
        private const string Path = "employees.xml";
        public static void Execute()
        {
            GenerateXml();

            var doc = XDocument.Load(Path);
            Console.WriteLine("Generated XML:\n");
            Console.WriteLine(doc + "\n");
            
            Console.WriteLine("Please enter information about a new employee.");
            var newEmployee = new XElement("employee");
            newEmployee.Add(new XElement("id", 4));
            Console.Write("First name: ");
            newEmployee.Add(new XElement("firstName", Console.ReadLine()));
            Console.Write("Last name: ");
            newEmployee.Add(new XElement("lastName", Console.ReadLine()));
            Console.Write("Photo URL: ");
            newEmployee.Add(new XElement("photo", Console.ReadLine()));

            var root = doc.Element("employees");
            root.Add(newEmployee);
            doc.Save(Path);
            
            Console.WriteLine("\nEdited XML:\n");
            Console.WriteLine(doc + "\n");
            
            File.Delete(Path);
        }

        private static void GenerateXml()
        {
            // a sample XML from https://codebeautify.org/xmlviewer#
            
            var doc = new XDocument();

            var employee1 = new XElement("employee");
            employee1.Add(new XElement("id", 1));
            employee1.Add(new XElement("firstName", "Leonardo"));
            employee1.Add(new XElement("lastName", "DiCaprio"));
            employee1.Add(new XElement("photo", "http://1.bp.blogspot.com/-zvS_6Q1IzR8/T5l6qvnRmcI/AAAAAAAABcc/HXO7HDEJKo0/s200/Leonardo+Dicaprio7.jpg"));
            
            var employee2 = new XElement("employee");
            employee2.Add(new XElement("id", 2));
            employee2.Add(new XElement("firstName", "Johnny"));
            employee2.Add(new XElement("lastName", "Depp"));
            employee2.Add(new XElement("photo", "http://4.bp.blogspot.com/_xR71w9-qx9E/SrAz--pu0MI/AAAAAAAAC38/2ZP28rVEFKc/s200/johnny-depp-pirates.jpg"));
            
            var employee3 = new XElement("employee");
            employee3.Add(new XElement("id", 3));
            employee3.Add(new XElement("firstName", "Hritik"));
            employee3.Add(new XElement("lastName", "Roshan"));
            employee3.Add(new XElement("photo", "http://thewallmachine.com/files/1411921557.jpg"));

            var employees = new XElement("employees");
            employees.Add(employee1, employee2, employee3);
            
            doc.Add(employees);
            doc.Save(Path);
        }
    }
}
