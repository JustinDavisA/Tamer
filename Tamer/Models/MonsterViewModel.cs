using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using System.ComponentModel.DataAnnotations;

namespace Tamer.Models
{
    public class MonsterViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Monster Name")]
        public string MonsterName { get; set; }
        [Required]
        [Display(Name = "Monster Health")]
        public int Health { get; set; }
        [Required]
        [Display(Name = "Attack Name")]
        public string AttackName { get; set; }
        [Required]
        [Display(Name = "Attack Damage")]
        public int AttackDamage { get; set; }
        public List<MonsterViewModel> MonsterList { get; set; }

        public static MonsterViewModel Map(Monster monsters)
        {
            MonsterViewModel beast = new MonsterViewModel();
            beast.Id = monsters.Id;
            beast.MonsterName = monsters.MonsterName;
            beast.Health = monsters.MonsterHealth;
            beast.AttackName = monsters.AttackName;
            beast.AttackDamage = monsters.AttackDamage;
            return beast;
        }
        public static Monster Map(MonsterViewModel monsters)
        {
            Monster beast = new Monster();
            beast.Id = monsters.Id;
            beast.MonsterName = monsters.MonsterName;
            beast.MonsterHealth = monsters.Health;
            beast.AttackName = monsters.AttackName;
            beast.AttackDamage = monsters.AttackDamage; 
            return beast;
        }
        public static List<MonsterViewModel> Map(List<Monster> monster)
        {
            List<MonsterViewModel> beast = new List<MonsterViewModel>();
            foreach (Monster creature in monster)
            {
                beast.Add(Map(creature));
            }
            return beast;
        }
        public static List<Monster> Map(List<MonsterViewModel> monster)
        {
            List<Monster> beast = new List<Monster>();
            foreach (MonsterViewModel creature in monster)
            {
                beast.Add(Map(creature));
            }
            return beast;
        }
    }
}