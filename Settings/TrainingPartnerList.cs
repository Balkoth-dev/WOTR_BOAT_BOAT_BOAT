using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOTR_BOAT_BOAT_BOAT.Settings
{
    public static class TrainingPartnerList
    {
        public static readonly List<string> trainingpartnersLocalizedStringList = new List<string>()
            {
                "2c0fd840d87c3b4418ae958c9a813fe0",
                "16d3e924302e06341914555b7d0c2039",
                "11f98b11ab989ed4aa407f778e0262dc",
                "eeb29b8b182f23e4a9a34c5520ae8571",
                "00a10a2129fc7de45889a7f10e06ea92",
                "b80f9014b742ea04ba27f897177bafc4",
                "b3ff82b3a3e578342a5edc1880f1480c",
                "dcb1e8beb63096a45a813623b9410139",
                "5440e7be8185f2d4eafbb89f5925feab",
                "bd08faf0a5d8a9d4d97613b95c1f1cee",
                "667f0ff46d87bca498cb8f410057be03",
                "ec9ff26ca80461348891cdd268a58753",
                "5cc2717c960068645a1b1f78436a7b34",
                "cd13c2f54d222234582aca645cde044d",
                "4932555b9d789964eb41183412c8b339",
                "3ab5715f9010a09408fbd307c7731c4c",
                "981b08bddac11ca41b3698d3f6059629",
                "04867a7006ab04140a4dd45d79d2ce1b",
                "60a35995607d493c85a8118dd9da993b",
                "d054e06bcb0242b0b3925d888a192cdd",
                "6c786c202513e8a458ad0e3b8324291c",
                "947c90850e25a094fbdebdcb80ae6434",
                "afb75a3f377db55458f6298c0bc30704",
                "3a4a9124784a49d299fbc770b41b127a",
                "95a3fd789c077c741955f8aad271686e",
                "515c094f4e757794fa12ce0d6d7ee3d4",
                "600c0aadc5e44075b3b1e1a623de392c",
                "9cbdad39cf0e7614da9453c7bed90210",
                "c35017033bd4491fb3fa880d49211c0b",
                "09a02fa57e1844a4b6df546954bd1a75",
                "c33d66055f934b66b60e3a4d61757baa",
                "a1b06220efb4f0545a870ce52fadd678",
                "0938fe1091ef439db8a28179d643250a",
                "f77027145d3b474c8317189b626c2345"
            };

        public static List<LocalizedString> localizedStrings()
        {
            var list = new List<LocalizedString>();
            foreach(var v in trainingpartnersLocalizedStringList)
            {
                list.Add(BlueprintTool.Get<BlueprintUnit>(v).LocalizedName.String);
            }
            return list;
        }
    }
}
