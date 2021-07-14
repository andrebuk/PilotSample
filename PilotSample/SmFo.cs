﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevMinPilotExt
{
    class SmFo
    {
        public string foName;
        
 
        public string GetFoList()
        {
            Dictionary<string, string> foDict = new Dictionary<string, string>();
            foDict.Add("1 - Центральный ", "Белгородская область\nБрянская область\nВладимирская область\nВоронежская область\nИвановская область\nКалужская область\nКостромская область\nКурская область\nЛипецкая область\nОрловская область\nРязанская область\nСмоленская область\nТамбовская область\nТверская область\nТульская область\nЯрославская область");
            foDict.Add("2 - Северо-Западный ", "Архангельская область\nВологодская область\nКалининградская область\nЛенинградская область\nМурманская область\nНенецкий автономный округ\nНовгородская область\nПсковская область\nРеспублика Карелия\nРеспублика Коми");
            foDict.Add("3 - Южный ", "Астраханская область\nВолгоградская область\nКраснодарский край\nРеспублика Адыгея\nРеспублика Калмыкия\nРостовская область");
            foDict.Add("4 - Северо-Кавказский ", "Кабардино­-Балкарская Республика\nКарачаево-Черкесская Республика\nРеспублика Дагестан\nРеспублика Ингушетия\nРеспублика Северная Осетия – Алания\nСтавропольский край\nЧеченская Республика");
            foDict.Add("5 - Приволжский ", "Кировская область\nНижегородская область\nОренбургская область\nПензенская область\nПермский край\nРеспублика Башкортостан\nРеспублика Марий Эл\nРеспублика Мордовия\nРеспублика Татарстан\nСамарская область\nСаратовская область\nУдмуртская Республика\nУльяновская область\nЧувашская Республика");
            foDict.Add("6 - Уральский ", "Курганская область\nСвердловская область\nТюменская область\nХанты-Мансийский автономный округ - Югра\nЧелябинская область\nЯмало-Ненецкий АО");
            foDict.Add("7 - Сибирский ", "Алтайский край\nИркутская область\nКемеровская область - Кузбасс\nКрасноярский край\nНовосибирская область\nОмская область\nРеспублика Алтай\nРеспублика Тыва\nРеспублика Хакасия\nТомская область");
            foDict.Add("8 - Дальневосточный ", "Амурская область\nЕврейская автономная область\nЗабайкальский край\nКамчатский край\nМагаданская область\nПриморский край\nРеспублика Бурятия\nРеспублика Саха (Якутия)\nСахалинская область\nХабаровский край");
            foDict.Add("9 - Казахстан ", "Весь Казахстан");
            foDict.Add("11 - Армения ", "Вся Армения");
            foDict.Add("10 - Узбекистан ", "Весь Узбекистан");
            foDict.Add("12 - Грузия ", "Вся Грузия");
            bool boolResult = foDict.TryGetValue(foName, out string result);
            if (boolResult)
            {
                return result;
            }
            else
            {
                return "Состав ФО не найден";
            }

        }

    }
}