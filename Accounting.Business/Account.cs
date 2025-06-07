using System;
using System.Linq;
using Accounting.DataLayer.Context;
using Accounting.ViewModels.Accounting;

namespace Accounting.Business
{
    public class Account
    {
        public static ReportViewModel ReportFormMain()
        {
            ReportViewModel rp = new ReportViewModel();
            using (UnitOfWork db = new UnitOfWork())
            {
                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
                DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30);

                var recive = db.AccountingRepository.Get(a => a.TypeID == 1 && a.DateTitle >= startDate && a.DateTitle <= endDate).Select(a => a.Amount).ToList();
                var pay = db.AccountingRepository.Get(a => a.TypeID == 2 && a.DateTitle >= startDate && a.DateTitle <= endDate).Select(a => a.Amount).ToList();


                rp.Recciv = recive.Sum();
                rp.Pay = pay.Sum();
                rp.AccountingBalamce = (recive.Sum() - pay.Sum());

            }
            return rp;
        }

    }
}
