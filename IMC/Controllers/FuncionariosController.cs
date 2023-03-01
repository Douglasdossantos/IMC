using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMC.Data;
using IMC.Models;

namespace IMC.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FuncionariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
              return View(await _context.Funcionarios.ToListAsync());
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sexo,Peso,Altura,IMC")] Funcionario funcionario)
        {                   

            if (ModelState.IsValid)
            {
                AjustarFuncionario(funcionario);
                funcionario.Categoria = ValidacaoCategoria((int)funcionario.Sexo, (decimal)funcionario.IMC);

                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sexo,Peso,Altura,IMC")] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    AjustarFuncionario(funcionario);
                    funcionario.Categoria = ValidacaoCategoria((int)funcionario.Sexo, (decimal)funcionario.IMC);

                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funcionarios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Funcionarios'  is null.");
            }
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
          return _context.Funcionarios.Any(e => e.Id == id);
        }

        private void AjustarFuncionario(Funcionario funcionario)
        {
            string peso = funcionario.Peso.ToString().Substring(0, funcionario.Peso.ToString().Length - 1);
            peso += "," + funcionario.Peso.ToString().Substring(funcionario.Peso.ToString().Length - 1, 1);
            string alto = funcionario.Altura.ToString().Substring(0, 1) + "," + funcionario.Altura.ToString().Substring(funcionario.Altura.ToString().Length - 2, 2);
            decimal kg = decimal.Parse(peso);
            decimal altura = decimal.Parse(alto);

            funcionario.IMC = ((float)(kg / (altura * altura)));
        }

        private string ValidacaoCategoria(int sexo, decimal imc)
        {
                string retorno = string.Empty;
            if (sexo == (int)Sexo.Feminino)
            {
                if (imc <= (decimal)19.1)
                {
                    retorno = "Abaixo do peso";
                }
                if (imc is > (decimal)19.1 or <= (decimal)25.8)
                {
                    retorno = "Peso Ideal";
                }
                if (imc is >= (decimal)25.9 or <= (decimal)27.3)
                {
                    retorno = "Pouco acima do peso";
                }
                if (imc is >= (decimal)27.4 or <= (decimal)32.3)
                {
                    retorno = "acima do peso";
                }
                if (imc >= (decimal)32.4)
                {
                    retorno = "Obesidade";
                }

            }
            else if (sexo == (int)Sexo.Masculino)
            {
                if (imc <= (decimal)20.7)
                {
                    retorno = "Abaixo do peso";
                }
                if (imc is > (decimal)20.7 or <= (decimal)26.4)
                {
                    retorno = "Peso Ideal";
                }
                if (imc is >= (decimal)26.5 or <= (decimal)27.8)
                {
                    retorno = "Pouco acima do peso";
                }
                if (imc is >= (decimal)27.9 or <= (decimal)31.1)
                {
                    retorno = "acima do peso";
                }
                if (imc >= (decimal)31.2)
                {
                    retorno = "Obesidade";
                }

            }
            return retorno;
        }
    }
}
