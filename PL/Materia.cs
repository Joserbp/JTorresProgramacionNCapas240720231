using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class Materia
    {
        public static void GetAll()
        {
            ML.Result result =  BL.Materia.GetAll();

            if(result.Correct)
            {
                foreach (ML.Materia materia in result.Objects)
                {
                    Console.WriteLine(materia.IdMateria);
                    Console.WriteLine(materia.Nombre);
                    Console.WriteLine(materia.Creditos);
                    Console.WriteLine(materia.Costo);
                }
            }
            else
            {
                Console.WriteLine("No se encontraron registros en la tabla Usuario. Detalle: " + result.ErrorMessage);
            }
        }

        public static void GetById()
        {
            ML.Materia materia = new ML.Materia();

            Console.WriteLine("Ingrese el Id de la materia que desea obtener");
            materia.IdMateria = byte.Parse(Console.ReadLine());

            ML.Result result = BL.Materia.GetById(materia.IdMateria);

            if (result.Correct)
            {
                materia = (ML.Materia)result.Object;
                Console.WriteLine(materia.IdMateria);
                Console.WriteLine(materia.Nombre);
                Console.WriteLine(materia.Creditos);
                Console.WriteLine(materia.Costo);
            }
            else
            {
                Console.WriteLine("No se encontraron registros en la tabla Usuario. Detalle: " + result.ErrorMessage);
            }




            // Acceder a objects y mostrar en consola 
        }
        public static void Add()
        {
            ML.Materia materia = new ML.Materia();


            Console.WriteLine("Ingrese el nombre de la materia");
            materia.Nombre = Console.ReadLine();

            Console.WriteLine("Ingreso los créditos de la materia");
            materia.Creditos = byte.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el costo de la materia");
            materia.Costo = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el costo de la materia");
            materia.Semestre = new ML.Semestre(); //Inicializar
            materia.Semestre.IdSemestre = byte.Parse(Console.ReadLine());


            ML.Result result = BL.Materia.AddLinQ(materia);

            if (result.Correct)
            {
                Console.WriteLine("La materia se inserto correctamente");
            }else
            {
                Console.WriteLine("Ocurrio un error al insertar la materia" + result.ErrorMessage);
                Console.WriteLine(result.Ex);
            }
        }

        public static void Update()
        {

        }

        public static void Delete()
        {


            ML.Materia materia = new ML.Materia();
            materia.IdMateria = 1;
            materia.Nombre = "Química";


            materia = new ML.Materia();

            Console.WriteLine(materia.IdMateria);
            Console.WriteLine(materia.Nombre);

        }
    }
}
