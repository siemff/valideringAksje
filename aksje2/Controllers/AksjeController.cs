using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aksje2.DAL;
using aksje2.Model;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace aksje2.Controllers
{
    [Route("[controller]/[action]")]
    public class AksjeController : ControllerBase
    {
        private readonly IAksjeRepository db;

        private ILogger<AksjeController> _log;

        public AksjeController(IAksjeRepository aksjeDb, ILogger<AksjeController> log)
        {
            db = aksjeDb;
            _log = log;
        }

        public async Task<ActionResult<Aksje>> hentAksjer()
        {
            List<Aksje> allAksjer = await db.hentAksjer();
            return Ok(allAksjer);
        }

        public async Task<ActionResult> hentEn(int id)
        {
            Aksje enAksje = await db.hentEn(id);
            if (enAksje == null)
            {
                _log.LogInformation("Fant ikke akjsen!");
                return NotFound("Fant ikke aksjen!");
            }
            return Ok("Aksje funnet!");
        }

        public async Task<ActionResult> kjopAksje(Salg innSalg)
        {
            bool returOK = await db.kjopAksje(innSalg);
            if (!returOK)
            {
                _log.LogInformation("Kjøp av aksje ble ikke gjennomført!");
                return BadRequest("Kjøp av aksje ble ikke gjennomført!");
            }
            return Ok("Aksje kjøp gjennomført");
        }

        public async Task<ActionResult> hentSaldo(int id)
        {
            double saldoHent = await db.hentSaldo(id);
            if (saldoHent == null)
            {
                _log.LogInformation("Fant ikke saldo!");
                return NotFound("Fant ikke saldo!");
            }
            return Ok("Saldo hentet!");
        }

        public async Task<ActionResult> hentPortefolje(int id)
        {
            List<Kjop> enPortefolije = await db.hentPortefolje(id);
            if(enPortefolije == null)
            {
                _log.LogInformation("Fant ikke portefolije!");
                return NotFound("Fant ikke portefolije!");
            }
            return Ok("Portefolije funnet!");
        }

        public async Task<ActionResult> selg(Selg innSelg)
        {
            bool returOk = await db.selg(innSelg);
            if (!returOk)
            {
                _log.LogInformation("Salg av aksjen ble ikke gjennomført!");
                return NotFound("Salg av aksjen ble ikke gjennomført!");
            }
            return Ok("Aksje salg gjennomført");
        }
        public async Task<ActionResult> Register(Bruker innBruker)
        {
            if (ModelState.IsValid)
            {
                bool returOK = await db.Register(innBruker);
                if (!returOK)
                {
                    _log.LogInformation("Personen kunne ikke registeres!");
                    return BadRequest("Personen kunne ikke registeres");
                }
                return Ok("Person lagret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await db.LoggInn(bruker);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker" + bruker.Brukernavn);
                    return Ok(false);
                }
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }


    }
}