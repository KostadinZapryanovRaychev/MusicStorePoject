using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Models
{
    public class TestModel
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Age { get; set; }

        public List<DateTime> FreeDates { get; set; } = new List<DateTime>()
        {
            new DateTime(2021, 12, 21),
            new DateTime(2021, 12, 22),
            new DateTime(2021, 12, 23),
            new DateTime(2021, 12, 24),
            new DateTime(2021, 12, 25),
            new DateTime(2021, 12, 26),
            new DateTime(2021, 12, 27),
            new DateTime(2021, 12, 28)
        };

        public bool isSame = false;


        //        for (int i = 0; i<; i++)
        //{
        //    string name1 = list1[i].Name;
        //        string date1 = list1[i].Date;
        //        string age1 = list1[i].Age;
        //    for (int j = 0; j<list2.Count; j++)
        //    {
        //        string name2 = list2[j].Name;
        //        string date2 = list2[j].Date;
        //        string age2 = list2[j].Age;
        //        if (name1.Equals(name2) && date1.Equals(date2) && age1.Equals(age2))
        //        {
        //            isSame = true;
        //            break;
        //        }
        //        else
        //        {
        //            isSame = false;
        //        }
        //    }
        //}
    }
}



