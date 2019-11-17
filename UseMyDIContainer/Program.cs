using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDIContainer;

namespace UseMyDIContainer
{
    class Program
    {        
        public interface ICat
        {
            int GetCountPaws();
        }
        public class Cat: ICat
        {
            public int GetCountPaws()
            {
                return 4;
            }
        }
        public interface IDog
        {
            string GetBreedName();
        }
        public class Dog : IDog
        {
            private string _breedName = "Breed";
            public string GetBreedName()
            {
                return _breedName;
            }
        }

        public static string Display(IDog dog)
        {
            return dog.GetBreedName();
        }

        public static string Display(ICat cat)
        {
            return cat.GetCountPaws().ToString();
        }


        static void Main(string[] args)
        {
            IoC.Register<IDog, Dog>();
            IoC.Register(typeof(ICat), typeof(Cat));

            Console.WriteLine(Display(IoC.Resolve<IDog>()));

            Console.WriteLine(Display((Cat)IoC.Resolve(typeof(ICat))));
            Console.ReadLine();
        }
    }
}
