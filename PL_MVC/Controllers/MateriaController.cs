using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class MateriaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Materia.GetAllLinQ();
            
            ML.Materia materia = new ML.Materia();

            if (result.Correct)
            {
                materia.Materias = result.Objects;
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;  //mandar datos de Controller hacia la vista
            }
            
            return View(materia);
        }


        //HttpGet -> Mostrar la vista
        //HttpPost -> Guardar los datos { Add, Update }


        [HttpGet] //Mostrar el formulario
        public ActionResult Form(int? IdMateria)
        {
            ML.Materia materia = new ML.Materia();
            if (IdMateria == null) //ADD
            {
                materia.Semestre = new ML.Semestre();
            }
            else //UPDATE
            {
                ML.Result result = BL.Materia.GetById((byte)IdMateria);
                if (result.Correct){
                    materia = (ML.Materia)result.Object;
                    materia.Semestre = new ML.Semestre();//No agregar
                }
            }
            return View(materia);
        }


        [HttpPost] //Recibir la información del formulario para Add/Update
        public ActionResult Form(ML.Materia materia)
        {
            if(materia.IdMateria == 0) //ADD
            {
               ML.Result result =  BL.Materia.AddLinQ(materia);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Materia agregada exitosamente";
                }
                else
                {
                    ViewBag.Mensaje = "Materia no se agrego exitosamente " + result.ErrorMessage;
                }
            }
            else //UPDATE
            {
                BL.Materia.UpdateLinQ(materia);
            }
            return View("Modal");

        }



    }
}