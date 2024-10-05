using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Parcial2024.Data;
using Parcial2024.Models;

namespace Parcial2024.Controllers
{
    public class RemesasController : Controller
    {
        private readonly ApplicationDbContext _context; // Cambiado a ApplicationDbContext

        public RemesasController(ApplicationDbContext context) // Cambiado a ApplicationDbContext
        {
            _context = context;
        }

        // Acción para mostrar la vista de registro
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        // Acción para registrar una nueva remesa y calcular el monto final
        [HttpPost]
        public IActionResult Registrar(Remesa nuevaRemesa)
        {
            if (ModelState.IsValid)
            {
                // Calcular el monto final basado en el monto enviado y la tasa de cambio
                nuevaRemesa.MontoFinal = nuevaRemesa.MontoEnviado * nuevaRemesa.TasaCambio;

                // Guardar la remesa en la base de datos
                _context.Remesas.Add(nuevaRemesa);
                _context.SaveChanges();

                // Redirigir a la vista de listado
                return RedirectToAction("Listar");
            }

            // Si el modelo no es válido, mostrar de nuevo la vista de registro con los datos ingresados
            return View("Registro", nuevaRemesa);
        }

        // Acción para listar todas las remesas
        public IActionResult Listar()
        {
            var remesas = _context.Remesas.ToList();
            return View(remesas);
        }
    }
}
