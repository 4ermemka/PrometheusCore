using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Shared.Constants
{
    public static class PlayerEffects
    {
        private static List<BuffJsonModel> ExistingBuffs;
        private static List<BuffJsonModel> ExistingDebuffs;
        private static List<InjuryJsonModel> ExistingInjuries;
        public static void Configigurate()
        { 
            var buffsText = Resources.Load<TextAsset>("Files\\Buffs").text;
            var debuffsText = Resources.Load<TextAsset>("Files\\Debuffs").text;
            var injuriesText = Resources.Load<TextAsset>("Files\\Injury").text;
            ExistingBuffs = JsonConvert.DeserializeObject<List<BuffJsonModel>>(buffsText);
            ExistingDebuffs = JsonConvert.DeserializeObject<List<BuffJsonModel>>(debuffsText);
            ExistingInjuries = JsonConvert.DeserializeObject<List<InjuryJsonModel>>(injuriesText);
        }

        public static Buff GetBuff(string buffName)
        {
            var buffModel = ExistingBuffs.FirstOrDefault(x=>x.Name == buffName);
            if (buffModel != null)
            {
                return new Buff(buffModel.Name, buffModel.Special, buffModel.Duration);
            }
            else
            { 
                return null;
            }
        }

        public static Buff GetDebuff(string debuffName)
        {
            var debuffModel = ExistingDebuffs.FirstOrDefault(x => x.Name == debuffName);
            if (debuffModel != null)
            {
                return new Buff(debuffModel.Name, debuffModel.Special, debuffModel.Duration);
            }
            else
            {
                return null;
            }
        }

        public static Injury GetInjury(string name)
        {
            var injuryModel = ExistingInjuries.FirstOrDefault(x => x.Name == name);
            if (injuryModel != null)
            {
                var injury = new Injury(injuryModel.Name, injuryModel.Special, injuryModel.Sources, injuryModel.Duration, injuryModel.BodyParts.Random());
                return injury;
            }
            else
            {
                return null; 
            }
        }

        public static Injury GetInjury(InjurySource source)
        {
            List<InjuryJsonModel> injuryModels = ExistingInjuries.Where(x => x.Sources.Contains(source)).ToList();
            if (injuryModels.Count() != 0)
            {
                var injuryModel = injuryModels.Random();
                var injury = new Injury(injuryModel.Name, injuryModel.Special, injuryModel.Sources, injuryModel.Duration, injuryModel.BodyParts.Random());
                return injury;
            }
            else
            {
                return null;
            }
        }
    }
}
