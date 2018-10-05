using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using ApplicationLogger;
using Tamer.Models;

namespace Tamer.Controllers
{
    public class MonsterController : Controller
    {
        private IMonsterLogic monsterLogic;
        private ILoggerIO log;

        public MonsterController(IMonsterLogic logic, ILoggerIO logger)
        {
            monsterLogic = logic;
            log = logger;
        }

        // GET: Monster
        public ActionResult Index()
        {
            List<MonsterViewModel> monsters = MonsterViewModel.Map(monsterLogic.GetMonsters());
            return View(monsters);
        }

        // GET: Monster/Details/5
        public ActionResult Details(int id)
        {
            return View(MonsterViewModel.Map(monsterLogic.GetMonsterById(id)));
        }

        // GET: Monster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Monster/Create
        [HttpPost]
        public ActionResult Create(MonsterViewModel monster)
        {
            try
            {
                // TODO: Add insert logic here
                monsterLogic.CreateMonster(MonsterViewModel.Map(monster));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Monster/Edit/5
        public ActionResult Edit(int id)
        {
            return View(MonsterViewModel.Map(monsterLogic.GetMonsterById(id)));
        }

        // POST: Monster/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MonsterViewModel monster)
        {
            try
            {
                // TODO: Add update logic here
                monsterLogic.EditMonster(MonsterViewModel.Map(monster));
                return RedirectToAction("Index", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Monster/Delete/5
        public ActionResult Delete(int id)
        {
            return View(MonsterViewModel.Map(monsterLogic.GetMonsterById(id)));
        }

        // POST: Monster/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, MonsterViewModel monster)
        {
            try
            {
                // TODO: Add delete logic here
                monsterLogic.DeleteMonster(MonsterViewModel.Map(monster));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Monster/Compare
        public ActionResult Compare()
        {
            return View(MonsterViewModel.Map(monsterLogic.GetMonsters()));
        }

        // POST: Monster/Compare
        [HttpPost]
        public ActionResult Compare(string monsterDropDown1, string monsterDropDown2)
        {
            Monster monster1 = monsterLogic.GetMonsterById(Convert.ToInt32(monsterDropDown1));
            Monster monster2 = monsterLogic.GetMonsterById(Convert.ToInt32(monsterDropDown2));
            int monsterWinnerId = monsterLogic.GetMonsterValues(monster1, monster2);
            if(monsterWinnerId == 0)
            {
                return RedirectToAction("Draw");
            }
            return RedirectToAction("SimulationResult", new { id = monsterWinnerId });
        }

        // GET: Monster/Simulation Result
        public ActionResult SimulationResult(int id)
        {
            return View(MonsterViewModel.Map(monsterLogic.GetMonsterById(id)));
        }

        //// POST: Monster/Simulation Result
        //[HttpPost]
        //public ActionResult SimulationResult(MonsterViewModel monster)
        //{
        //    return View(monster);
        //}

        // GET: Monster/Draw
        public ActionResult Draw()
        {
            return View();
        }
    }
}
