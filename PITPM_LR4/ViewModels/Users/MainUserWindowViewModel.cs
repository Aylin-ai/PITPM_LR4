using PITPM_LR4.Models;
using PITPM_LR4.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITPM_LR4.ViewModels.Users
{
    internal class MainUserWindowViewModel : ViewModel
    {

        #region Данные о товарах
        static Context db = new Context();
        public List<Good> Goods { get; set; } = db.Goods.ToList();

        #endregion

    }
}
