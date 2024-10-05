using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Parcial2024.Data;
using Parcial2024.Models;
using Parcial2024.Services; // Incluir el espacio de nombres para CoinGeckoService

namespace Parcial2024.Controllers
{
    public class RemesasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CoinGeckoService _coinGeckoService; // Inyectar CoinGeckoService

        public RemesasController(ApplicationDbContext context, CoinGeckoService coinGeckoService) // Inyectar CoinGeckoService
        {
            _context = context;
            _coinGeckoService = coinGeckoService;
        }

        [HttpGet]
        public IActionResult Registro()
        {
            // Crear una nueva instancia de Remesa y pasarla a la vista
            var nuevaRemesa = new Remesa();
            return View(nuevaRemesa);
        }

        // Acción para registrar una nueva remesa y calcular el monto final
        [HttpPost]
        public async Task<IActionResult> Registrar(Remesa nuevaRemesa)
        {
            if (ModelState.IsValid)
            {
                // Si la moneda enviada es USD, realizar la conversión a BTC
                if (nuevaRemesa.MonedaEnviada == "USD")
                {
                    // Obtener la tasa de cambio actual de USD a BTC usando la API de CoinGecko
                    var tasaCambioBTC = await _coinGeckoService.GetBtcPriceInUsdAsync();
                    
                    // Calcular el monto en BTC con la tasa de cambio actual
                    nuevaRemesa.MontoFinal = nuevaRemesa.MontoEnviado / tasaCambioBTC;
                    nuevaRemesa.TasaCambio = tasaCambioBTC; // Almacenar la tasa de cambio usada
                }
                else if (nuevaRemesa.MonedaEnviada == "BTC")
                {
                    // Obtener la tasa de cambio de BTC a USD usando la API de CoinGecko
                    var tasaCambioUSD = await _coinGeckoService.GetBtcPriceInUsdAsync(); // Obtener BTC en USD
                    
                    // Calcular el monto en USD con la tasa de cambio actual
                    nuevaRemesa.MontoFinal = nuevaRemesa.MontoEnviado * tasaCambioUSD; // Multiplica para convertir a USD
                    nuevaRemesa.TasaCambio = tasaCambioUSD; // Almacenar la tasa de cambio usada
                }

                // Guardar la remesa en la base de datos
                _context.Remesas.Add(nuevaRemesa);
                await _context.SaveChangesAsync();

                // Redirigir a la vista de listado de remesas
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
