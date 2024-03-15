using Microsoft.AspNetCore.Mvc;
using PracticaMvcMichelyPinto.Models;
using PracticaMvcMichelyPinto.Repositories;
using System.Drawing;

namespace PracticaMvcMichelyPinto.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatilla repo;
        public ZapatillasController(RepositoryZapatilla repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Zapatillas()
        {
            List<Zapatilla> zapatillas = await this.repo.GetZapatillasAsync();
            return View(zapatillas);
        }
        public async Task<IActionResult> Details(int idproducto)
        {
            Zapatilla zap = await this.repo.FindZapatillaAsync(idproducto);
            return View(zap);
        }
        public async Task<IActionResult> _PartialPaginacion(int? posicion,int idproducto)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
           
           
                ModelPaginacion model = await
                    this.repo.GetImagen(posicion.Value,idproducto);
                ViewData["REGISTROS"] = model.NumeroRegistros;
                ViewData["IDPRODUCTO"] = idproducto;
                ViewData["POSICION"] = posicion;
                int siguiente = posicion.Value + 1;
                //DEBEMOS COMPROBAR QUE NO PASAMOS DEL NUMERO DE REGISTROS
                if (siguiente > model.NumeroRegistros)
                {
                    //EFECTO OPTICO
                    siguiente = model.NumeroRegistros;
                }
                int anterior = posicion.Value - 1;
                if (anterior < 1)
                {
                    anterior = 1;
                }
                ViewData["ULTIMO"] = model.NumeroRegistros;
                ViewData["SIGUIENTE"] = siguiente;
                ViewData["ANTERIOR"] = anterior;
                ViewData["POSICION"] = posicion;
                return PartialView("_PartialPaginacion", model.Imagenes);
           
        }
        public async Task<IActionResult> Insertar()
        {

            var zapatillas = await this.repo.GetZapatillasAsync();
            ViewData["ZAPATILLAS"] = zapatillas;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insertar(List<string> imagen)
        {

            //this.repo.InsertarImagen(imagen);
            return View();
        }

    }
}

