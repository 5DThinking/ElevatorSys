using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorSys.Common;

namespace ElevatorSys.BLL
{
    public class PersonService
    {
        public static List<OutsideCall> CreatePersons()
        {
            List<Person> newPersons = new List<Person>();
            int personCount = Methods.RandomValue(1, 9);
            for (int i = 0; i < personCount; i++)
            {
                Person newPerson = new Person();
                newPersons.Add(newPerson);
            }
            ElevatorService.Call(newPersons.ToArray());
            return newPersons.Select(x=>new OutsideCall{ Floor= x.FromFloor, Direction=x.Direction}).Distinct().ToList();
        }

        public static void IntoElevator(Elevator elev,Person[] persons)
        {
            elev.PersonList.AddRange(persons);

        }
    }
}
