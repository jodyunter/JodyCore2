using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.ConsoleApp.Views
{
    public class StandingsView
    {
        public static string GetView(IStandingsViewModel model)
        {
            var result = StandingsRecordView.HeaderString();

            model.Records.OrderBy(r => r.Rank).ToList().ForEach(r =>
            {
                result += "\n" + StandingsRecordView.RecordView(r);
            });

            return result;
        }
    }
}
