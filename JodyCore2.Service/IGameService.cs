﻿using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service
{
    public interface IGameService
    {
        IGameSummaryViewModel Create(int day, int year, Guid homeId, Guid awayid);
        IGameSummaryViewModel Play(Guid gameId);
        IList<IGameSummaryViewModel> GetGames(int year, int firstDay, int lastDay);
    }
}
