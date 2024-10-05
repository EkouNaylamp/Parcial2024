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
            
            var nuevaRemesa = new Remesa();
            return View(nuevaRemesa);
        }

        // Acción para registrar una nueva remesa y calcular el monto final
        [HttpPost]
        public async Task<IActionResult> Registrar(Remesa nuevaRemesa)
        {
            if (ModelState.IsValid)
            {
                if (nuevaRemesa.MonedaEnviada == "USD")
                {
                    var tasaCambioBTC = await _coinGeckoService.GetBtcPriceInUsdAsync();
                    
                    nuevaRemesa.MontoFinal = nuevaRemesa.MontoEnviado / tasaCambioBTC;
                    nuevaRemesa.TasaCambio = tasaCambioBTC; 
                }
                else if (nuevaRemesa.MonedaEnviada == "BTC")
                {
                    var tasaCambioUSD = await _coinGeckoService.GetBtcPriceInUsdAsync(); 
                    
                    nuevaRemesa.MontoFinal = nuevaRemesa.MontoEnviado * tasaCambioUSD; 
                    nuevaRemesa.TasaCambio = tasaCambioUSD; 
                }

                _context.Remesas.Add(nuevaRemesa);
                await _context.SaveChangesAsync();

                return RedirectToAction("Listar");
            }

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
